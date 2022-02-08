using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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

            switch (e.type)
            {
                case EventType.MouseDown:
                {
                    isDoubleClick = e.clickCount == 2;
                    mouseDownPosition = e.mousePosition;

                    if (CurrentState == State.Idle)
                    {
                        var currentHoverRoomNode = GetHoverRoomNode(e.mousePosition);
                        var currentHoverConnectionNode = GetHoverConnectionNode(e.mousePosition);

                        if (currentHoverRoomNode != null)
                        {
                            // Hold room on left click
                            if (e.button == 0 && !e.control)
                            {
                                hoverRoomNode = currentHoverRoomNode;
                                CurrentState = State.HoldRoom;
                            }
                            // Start making a connection on left click with control
                            else if (e.button == 0 && e.control)
                            {
                                CurrentState = State.CreateConnection;
                                connectionStartNode = currentHoverRoomNode;
                            }
                        }
                        else if (currentHoverConnectionNode != null)
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
                    var currentHoverRoomNode = GetHoverRoomNode(e.mousePosition);
                    var currentHoverConnectionNode = GetHoverConnectionNode(e.mousePosition);

                    if (currentHoverRoomNode != null)
                    {
                        var mouseDownDistance = Vector2.Distance(mouseDownPosition, e.mousePosition);

                        // Configure room on double click
                        if (e.button == 0 && !e.control && ( /*mouseDownDistance <= 2 ||*/ isDoubleClick))
                        {
                            SelectObject(currentHoverRoomNode.Room, e.shift);

                            GUI.changed = true;
                            CurrentState = State.Idle;
                        }
                        // Show room context menu on right click
                        else if (e.button == 1)
                        {
                            ShowRoomContextMenu(currentHoverRoomNode);
                            GUI.changed = true;
                        }
                        // Create a connection if hovering node
                        else if (CurrentState == State.CreateConnection)
                        {
                            CreateConnection(connectionStartNode, currentHoverRoomNode);
                            GUI.changed = true;
                        }
                    }
                    else if (currentHoverConnectionNode != null)
                    {
                        // Configure connection on double click
                        if (e.button == 0 && isDoubleClick)
                        {
                            SelectObject(currentHoverConnectionNode.Connection);
                            GUI.changed = true;
                        }
                        // Show connection context menu on right click
                        else if (e.button == 1)
                        {
                            ShowConnectionContextMenu(currentHoverConnectionNode);
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
                            originalDragRoomPosition = hoverRoomNode.Room.Position;
                        }

                        CurrentState = State.DragRoom;
                        drag += e.delta;

                        HandleDragRoomNode(e);
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

            var roomNode = CreateRoomNode(room);

            // We have to compute a normalized position because the mouse position is not affected by zoom and pan offset
            var normalizedPosition = WorldToGridPosition(position);

            // Subtract the center of the room node rect because we want the room to appear centered on the cursor
            normalizedPosition -= roomNode.GetRect(1, Vector2.zero).center;

            // Snap to grid if enabled
            if (snapToGrid)
            {
                normalizedPosition = GetSnappedToGridPosition(normalizedPosition);
            }

            room.Position = normalizedPosition;

            // Select the room in the inspector after creating
            SelectObject(room);

            EditorUtility.SetDirty(LevelGraph);

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
        /// Creates a room node from a given room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private RoomNode CreateRoomNode(RoomBase room)
        {
            var roomNode = new RoomNode(room);
            roomNodes.Add(roomNode);

            return roomNode;
        }

        /// <summary>
        /// Creates a connection between the two given room nodes.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ConnectionBase CreateConnection(RoomNode from, RoomNode to)
        {
            // Do not create the connection if the from room is same as the to room
            if (from.Room == to.Room)
            {
                return null;
            }

            // Do not create the connection if it already exists
            if (connectionNodes.Any(x => (x.From == from && x.To == to) || (x.To == from && x.From == to)))
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

            CreateConnectionNode(connection);

            EditorUtility.SetDirty(LevelGraph);

            return connection;
        }

        /// <summary>
        /// Create a connection node from a given connection.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public ConnectionNode CreateConnectionNode(ConnectionBase connection)
        {
            var from = roomNodes.Single(x => x.Room == connection.From);
            var to = roomNodes.Single(x => x.Room == connection.To);

            var connectionNode = new ConnectionNode(connection, from, to);
            connectionNodes.Add(connectionNode);

            return connectionNode;
        }

        /// <summary>
        /// Handle room being dragged.
        /// </summary>
        /// <param name="e"></param>
        private void HandleDragRoomNode(Event e)
        {
            var node = hoverRoomNode;
            var dragOffset = drag / zoom;
            var newPosition = originalDragRoomPosition + dragOffset;

            // Snap to grid if permanently enabled or if holding shift
            if (snapToGrid || e.shift)
            {
                newPosition = GetSnappedToGridPosition(newPosition);
            }

            node.Room.Position = newPosition;

            EditorUtility.SetDirty(LevelGraph);
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
        private RoomNode GetHoverRoomNode(Vector2 mousePosition)
        {
            foreach (var roomNode in roomNodes)
            {
                if (roomNode.GetRect(zoom, panOffset).Contains(mousePosition))
                {
                    return roomNode;
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
        private ConnectionNode GetHoverConnectionNode(Vector2 mousePosition)
        {
            foreach (var connectionNode in connectionNodes)
            {
                if (connectionNode.GetHandleRect(zoom, panOffset).Contains(mousePosition))
                {
                    return connectionNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Shows the connection context menu.
        /// </summary>
        /// <param name="connectionNode"></param>
        private void ShowConnectionContextMenu(ConnectionNode connectionNode)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Delete connection"), false, () => DeleteConnectionNode(connectionNode));
            genericMenu.ShowAsContext();
        }

        /// <summary>
        /// Deletes a given connection node.
        /// </summary>
        /// <param name="connectionNode"></param>
        private void DeleteConnectionNode(ConnectionNode connectionNode)
        {
            LevelGraph.Connections.Remove(connectionNode.Connection);
            DestroyImmediate(connectionNode.Connection, true);
            connectionNodes.Remove(connectionNode);

            EditorUtility.SetDirty(LevelGraph);
        }

        /// <summary>
        /// Shows the room context menu.
        /// </summary>
        /// <param name="roomNode"></param>
        private void ShowRoomContextMenu(RoomNode roomNode)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Configure room"), false, () => SelectObject(roomNode.Room));
            genericMenu.AddItem(new GUIContent("Delete room"), false, () => DeleteRoomNode(roomNode));
            genericMenu.ShowAsContext();
        }

        /// <summary>
        /// Deletes a given room node.
        /// </summary>
        /// <param name="roomNode"></param>
        private void DeleteRoomNode(RoomNode roomNode)
        {
            LevelGraph.Rooms.Remove(roomNode.Room);
            DestroyImmediate(roomNode.Room, true);
            roomNodes.Remove(roomNode);

            var connectionsToRemove = new List<ConnectionNode>();
            foreach (var connectionNode in connectionNodes)
            {
                if (connectionNode.From == roomNode || connectionNode.To == roomNode)
                {
                    connectionsToRemove.Add(connectionNode);
                }
            }

            foreach (var connectionNode in connectionsToRemove)
            {
                DeleteConnectionNode(connectionNode);
            }

            EditorUtility.SetDirty(LevelGraph);
        }

        private void DeleteSelectedRooms()
        {
            foreach (var room in Selection.objects.OfType<RoomBase>())
            {
                var roomNode = roomNodes.FirstOrDefault(x => x.Room == room);

                if (roomNode == null)
                {
                    continue;
                }

                DeleteRoomNode(roomNode);
            }

            GUI.changed = true;
        }

        private void DuplicateSelectedRooms()
        {
            foreach (var room in Selection.objects.OfType<RoomBase>())
            {
                var roomNode = roomNodes.FirstOrDefault(x => x.Room == room);

                if (roomNode == null)
                {
                    continue;
                }

                var duplicatedRoom = Instantiate(room);
                duplicatedRoom.Position += 2 * new Vector2(gridSize, -gridSize);

                var duplicatedRoomNode = CreateRoomNode(duplicatedRoom);

                // Add room to the level graph
                LevelGraph.Rooms.Add(duplicatedRoom);
                AssetDatabase.AddObjectToAsset(duplicatedRoom, LevelGraph);

                EditorUtility.SetDirty(LevelGraph);
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
    }
}