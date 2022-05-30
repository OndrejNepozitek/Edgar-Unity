using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Edgar.Unity.Editor
{
    public partial class LevelGraphEditor
    {
        private readonly List<string> supportedCommands = new List<string>()
        {
            "SoftDelete", "Duplicate",
        };

        /// <summary>
        /// This field is used to track if there was a drag between MouseDown and MouseUp events.
        /// </summary>
        private Vector2 mouseDownPosition;

        /// <summary>
        /// Handles are the controls of the editor - adding/deleting rooms/connections, etc.
        /// </summary>
        private void HandleControls()
        {
            var e = Event.current;

            // Skip window controls if an object picker is currently shown
            if (EditorGUIUtility.GetObjectPickerControlID() != 0)
            {
                return;
            }

            switch (e.type)
            {
                case EventType.MouseDown:
                {
                    isDoubleClick = e.clickCount == 2;
                    mouseDownPosition = e.mousePosition;

                    if (CurrentState == State.Idle)
                    {
                        var currentHoverRoomControl = GetHoverRoomControl(e.mousePosition);
                        var hoverConnectionControl = GetHoverConnectionControl(e.mousePosition);

                        if (currentHoverRoomControl != null)
                        {
                            // Hold room on left click
                            if (e.button == 0 && !e.control)
                            {
                                hoverRoomControl = currentHoverRoomControl;
                                CurrentState = State.HoldRoom;
                            }
                            // Start making a connection on left click with control
                            else if (e.button == 0 && e.control)
                            {
                                CurrentState = State.CreateConnection;
                                connectionStartControl = currentHoverRoomControl;
                            }
                        }
                        else if (hoverConnectionControl != null)
                        {
                            /* empty */
                        }
                        // Hold grid on right click on empty space
                        else if (e.button == 1 || e.button == 2)
                        {
                            CurrentState = State.HoldGrid;
                        }
                    }

                    break;
                }

                case EventType.MouseUp:
                {
                    var currentHoverRoomControl = GetHoverRoomControl(e.mousePosition);
                    var currentHoverConnectionControl = GetHoverConnectionControl(e.mousePosition);

                    var mouseDownDistance = Vector2.Distance(mouseDownPosition, e.mousePosition);
                    var doubleClickToConfigure = EdgarSettings.instance.General.DoubleClickToConfigureRoom;

                    if (currentHoverRoomControl != null)
                    {
                        // Configure room on double click
                        if (e.button == 0 && !e.control && ((doubleClickToConfigure && isDoubleClick) || (!doubleClickToConfigure && mouseDownDistance <= 2)))
                        {
                            SelectObject(currentHoverRoomControl.Room, e.shift);

                            GUI.changed = true;
                            CurrentState = State.Idle;
                        }
                        // Show room context menu on right click
                        else if (e.button == 1)
                        {
                            ShowRoomContextMenu(currentHoverRoomControl);
                            GUI.changed = true;
                        }
                        // Create a connection if hovering a room
                        else if (CurrentState == State.CreateConnection)
                        {
                            CreateConnection(connectionStartControl, currentHoverRoomControl);
                            GUI.changed = true;
                        }
                    }
                    else if (currentHoverConnectionControl != null)
                    {
                        // Configure connection on double click
                        if (e.button == 0 && ((doubleClickToConfigure && isDoubleClick) || (!doubleClickToConfigure && mouseDownDistance <= 2)))
                        {
                            SelectObject(currentHoverConnectionControl.Connection);
                            GUI.changed = true;
                        }
                        // Show connection context menu on right click
                        else if (e.button == 1)
                        {
                            ShowConnectionContextMenu(currentHoverConnectionControl);
                            GUI.changed = true;
                        }
                    }
                    // Change GUI if didn't find anything to connect to, otherwise there would be a line rendered pointing to nothing
                    else if (CurrentState == State.CreateConnection)
                    {
                        GUI.changed = true;
                    }
                    // Create room on double click
                    else if (e.button == 0 && isDoubleClick)
                    {
                        CreateRoom(e.mousePosition);
                        GUI.changed = true;
                    }

                    CurrentState = State.Idle;

                    break;
                }

                case EventType.MouseDrag:
                {
                    // Drag grid
                    if (CurrentState == State.HoldGrid || CurrentState == State.DragGrid)
                    {
                        CurrentState = State.DragGrid;
                        panOffset += e.delta / zoom;

                        GUI.changed = true;
                    }

                    // Drag room
                    if (CurrentState == State.HoldRoom || CurrentState == State.DragRoom)
                    {
                        if (CurrentState == State.HoldRoom)
                        {
                            drag = Vector2.zero;
                            originalDragRoomPosition = hoverRoomControl.Room.Position;
                        }

                        CurrentState = State.DragRoom;
                        drag += e.delta;

                        HandleDragRoomControl(e);
                        GUI.changed = true;
                    }

                    if (CurrentState == State.CreateConnection)
                    {
                        GUI.changed = true;
                    }

                    break;
                }

                case EventType.ScrollWheel:
                {
                    // Scroll
                    if (CurrentState == State.Idle)
                    {
                        HandleZoom(e);
                        GUI.changed = true;
                    }

                    break;
                }
            }

            if (e.type == EventType.ExecuteCommand || e.type == EventType.ValidateCommand)
            {
                if (e.type == EventType.ValidateCommand && supportedCommands.Contains(e.commandName))
                {
                    e.Use();
                }

                if (e.type is EventType.ExecuteCommand)
                {
                    switch (e.commandName)
                    {
                        case "SoftDelete":
                            DeleteSelectedRooms();
                            break;

                        case "Duplicate":
                            DuplicateSelectedRooms();
                            break;
                    }
                }
            }

            //if (e.type != EventType.Layout && e.type != EventType.Repaint)
            //{
            //    Debug.Log(e);
            //}

            if (GUI.changed)
            {
                Repaint();
            }
        }

        /// <summary>
        /// Create a room at a given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private RoomBase CreateRoom(Vector2 position)
        {
            var type = FindType(LevelGraph.RoomType);
            var roomType = type != null ? LevelGraph.RoomType : typeof(Room).FullName;
            var room = (RoomBase) CreateInstance(roomType);

            // Add room to the level graph
            LevelGraph.Rooms.Add(room);
            AssetDatabase.AddObjectToAsset(room, LevelGraph);

            var roomControl = CreateRoomControl(room);

            // We have to compute a normalized position because the mouse position is not affected by zoom and pan offset
            var normalizedPosition = WorldToGridPosition(position);

            // Subtract the center of the room control rect because we want the room to appear centered on the cursor
            normalizedPosition -= roomControl.GetRect(Vector2.zero, 1).center;

            // Snap to grid if enabled
            if (snapToGrid)
            {
                normalizedPosition = GetSnappedToGridPosition(normalizedPosition);
            }

            room.Position = normalizedPosition;

            // Select the room in the inspector after creating
            SelectObject(room);

            SetDirtyInternal();

            return room;
        }

        private static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }

        /// <summary>
        /// Creates a room control for a given room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private RoomControl CreateRoomControl(RoomBase room)
        {
            RoomControl roomControl;

            if (roomTypeToControlType.TryGetValue(room.GetType(), out var customControl))
            {
                roomControl = (RoomControl) Activator.CreateInstance(customControl);
            }
            else
            {
                roomControl = new RoomControl();
            }

            roomControl.Initialize(room);
            roomControls.Add(roomControl);

            return roomControl;
        }

        /// <summary>
        /// Creates a connection between the two given rooms.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ConnectionBase CreateConnection(RoomControl from, RoomControl to)
        {
            // Do not create the connection if the from room is same as the to room
            if (from.Room == to.Room)
            {
                return null;
            }

            // Do not create the connection if it already exists
            if (connectionControls.Any(x => (x.From == from && x.To == to) || (x.To == from && x.From == to)))
            {
                return null;
            }

            var type = FindType(LevelGraph.RoomType);
            var connectionType = type != null ? LevelGraph.ConnectionType : typeof(Connection).FullName;
            var connection = (ConnectionBase) CreateInstance(connectionType);

            connection.From = from.Room;
            connection.To = to.Room;

            LevelGraph.Connections.Add(connection);
            AssetDatabase.AddObjectToAsset(connection, LevelGraph);

            CreateConnectionControl(connection);

            SetDirtyInternal();

            return connection;
        }

        /// <summary>
        /// Create a connection control for a given connection.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public ConnectionControl CreateConnectionControl(ConnectionBase connection)
        {
            var from = roomControls.Single(x => x.Room == connection.From);
            var to = roomControls.Single(x => x.Room == connection.To);

            ConnectionControl connectionControl;
            if (connectionTypeToControlType.TryGetValue(connection.GetType(), out var customControl))
            {
                connectionControl = (ConnectionControl) Activator.CreateInstance(customControl);
            }
            else
            {
                connectionControl = new ConnectionControl();
            }

            connectionControl.Initialize(connection, from, to);
            connectionControls.Add(connectionControl);

            return connectionControl;
        }

        /// <summary>
        /// Handle room being dragged.
        /// </summary>
        /// <param name="e"></param>
        private void HandleDragRoomControl(Event e)
        {
            var control = hoverRoomControl;
            var dragOffset = drag / zoom;
            var newPosition = originalDragRoomPosition + dragOffset;

            // Snap to grid if permanently enabled or if holding shift
            if (snapToGrid || e.shift)
            {
                newPosition = GetSnappedToGridPosition(newPosition);
            }

            control.Room.Position = newPosition;

            SetDirtyInternal();
        }

        /// <summary>
        /// Handle zoom.
        /// </summary>
        /// <param name="e"></param>
        private void HandleZoom(Event e)
        {
            var oldZoom = zoom;

            if (e.delta.y > 0)
            {
                zoom -= zoom * 0.1f;
            }
            else
            {
                zoom += zoom * 0.1f;
            }

            // Zoom value must be in the interval [0.1,5]
            zoom = Math.Max(0.1f, zoom);
            zoom = Math.Min(5, zoom);

            // If zoom center is the current mouse position, zoom will focus on that position
            // An alternative is to use position.size / 2 in which case the center of the window will be the same after zooming
            var zoomCenter = e.mousePosition;

            // This equation makes sure that zoom center is the focus of the zoom
            panOffset += -(zoom * (zoomCenter - panOffset * oldZoom) - zoomCenter * oldZoom) / (zoom * oldZoom) - panOffset;
        }

        /// <summary>
        /// Gets the room that is under the mouse cursor.
        /// Returns null if there is no such room.
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        private RoomControl GetHoverRoomControl(Vector2 mousePosition)
        {
            foreach (var roomControl in roomControls)
            {
                if (roomControl.GetRect(panOffset, zoom).Contains(mousePosition))
                {
                    return roomControl;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the connection that is under the mouse cursor.
        /// Returns null if there is no such connection.
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        private ConnectionControl GetHoverConnectionControl(Vector2 mousePosition)
        {
            foreach (var connectionControl in connectionControls)
            {
                if (connectionControl.GetHandleRect(panOffset, zoom).Contains(mousePosition))
                {
                    return connectionControl;
                }
            }

            return null;
        }

        /// <summary>
        /// Shows the connection context menu.
        /// </summary>
        private void ShowConnectionContextMenu(ConnectionControl connectionControl)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Delete connection"), false, () => DeleteConnection(connectionControl));
            genericMenu.ShowAsContext();
        }

        /// <summary>
        /// Deletes a given connection.
        /// </summary>
        private void DeleteConnection(ConnectionControl connectionControl)
        {
            LevelGraph.Connections.Remove(connectionControl.Connection);
            DestroyImmediate(connectionControl.Connection, true);
            connectionControls.Remove(connectionControl);

            SetDirtyInternal();
        }

        /// <summary>
        /// Shows the room context menu.
        /// </summary>
        /// <param name="roomControl"></param>
        private void ShowRoomContextMenu(RoomControl roomControl)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Configure room"), false, () => SelectObject(roomControl.Room));
            genericMenu.AddItem(new GUIContent("Delete room"), false, () => DeleteRoom(roomControl));
            genericMenu.ShowAsContext();
        }

        /// <summary>
        /// Deletes a given room.
        /// </summary>
        /// <param name="roomControl"></param>
        private void DeleteRoom(RoomControl roomControl)
        {
            LevelGraph.Rooms.Remove(roomControl.Room);
            DestroyImmediate(roomControl.Room, true);
            roomControls.Remove(roomControl);

            var connectionsToRemove = new List<ConnectionControl>();
            foreach (var connectionControl in connectionControls)
            {
                if (connectionControl.From == roomControl || connectionControl.To == roomControl)
                {
                    connectionsToRemove.Add(connectionControl);
                }
            }

            foreach (var connectionControl in connectionsToRemove)
            {
                DeleteConnection(connectionControl);
            }

            SetDirtyInternal();
        }

        private void DeleteSelectedRooms()
        {
            foreach (var room in Selection.objects.OfType<RoomBase>())
            {
                var roomControl = roomControls.FirstOrDefault(x => x.Room == room);

                if (roomControl == null)
                {
                    continue;
                }

                DeleteRoom(roomControl);
            }

            GUI.changed = true;
        }

        private void DuplicateSelectedRooms()
        {
            foreach (var room in Selection.objects.OfType<RoomBase>())
            {
                var roomControl = roomControls.FirstOrDefault(x => x.Room == room);

                if (roomControl == null)
                {
                    continue;
                }

                var duplicatedRoom = Instantiate(room);
                duplicatedRoom.Position += 2 * new Vector2(gridSize, -gridSize);

                CreateRoomControl(duplicatedRoom);

                // Add room to the level graph
                LevelGraph.Rooms.Add(duplicatedRoom);
                AssetDatabase.AddObjectToAsset(duplicatedRoom, LevelGraph);

                SetDirtyInternal();
            }

            GUI.changed = true;
        }

        private static void SelectObject(UnityEngine.Object o, bool expandSelection = false)
        {
            ProjectBrowserLocker.Lock();

            if (!expandSelection)
            {
                Selection.activeObject = o;
            }
            else
            {
                Selection.objects = new List<UnityEngine.Object>(Selection.objects) {o}.ToArray();
            }
        }

        /// <summary>
        /// Setups custom controls for rooms and connection using reflection and attributes.
        /// </summary>
        private void SetupCustomControls()
        {
            //var sw = new Stopwatch();
            //sw.Start();

            roomTypeToControlType = new Dictionary<Type, Type>();
            foreach (var tuple in GetTypesWithAttribute<CustomRoomControlAttribute>())
            {
                var controlType = tuple.Item1;
                var attribute = tuple.Item2;

                if (!typeof(RoomControl).IsAssignableFrom(controlType))
                {
                    Debug.LogWarning($"Class '{controlType}' does not inherit from '{nameof(RoomControl)}'!");
                    continue;
                }

                //Debug.Log($"{attribute.RoomType}, {controlType}");
                roomTypeToControlType[attribute.RoomType] = controlType;
            }

            connectionTypeToControlType = new Dictionary<Type, Type>();
            foreach (var tuple in GetTypesWithAttribute<CustomConnectionControlAttribute>())
            {
                var controlType = tuple.Item1;
                var attribute = tuple.Item2;

                if (!typeof(ConnectionControl).IsAssignableFrom(controlType))
                {
                    Debug.LogWarning($"Class '{controlType}' does not inherit from '{nameof(ConnectionControl)}'!");
                    continue;
                }

                //Debug.Log($"{attribute.ConnectionType}, {controlType}");
                connectionTypeToControlType[attribute.ConnectionType] = controlType;
            }

            //sw.Stop();
            //Debug.Log($"{sw.ElapsedMilliseconds}");
        }

        /// <summary>
        /// Finds types that contain a custom attribute.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        private IEnumerable<Tuple<Type, TAttribute>> GetTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            var definedIn = typeof(TAttribute).Assembly.GetName().Name;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly == typeof(TAttribute).Assembly || assembly.GetReferencedAssemblies().Any(a => a.Name == definedIn))
                {
                    Type[] types;

                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException)
                    {
                        continue;
                    }

                    foreach (var type in types)
                    {
                        var attribute = type.GetCustomAttribute<TAttribute>();

                        if (attribute != null)
                        {
                            yield return Tuple.Create(type, attribute);
                        }
                    }
                }
            }
        }
    }
}