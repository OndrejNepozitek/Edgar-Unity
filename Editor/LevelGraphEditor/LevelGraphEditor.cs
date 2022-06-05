using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Edgar.Unity.Editor
{
    public partial class LevelGraphEditor : EditorWindow
    {
        public LevelGraph LevelGraph { get; private set; }

        private List<RoomControl> roomControls = new List<RoomControl>();

        private List<ConnectionControl> connectionControls = new List<ConnectionControl>();

        public State CurrentState;

        private bool snapToGrid => EdgarSettings.instance.General.SnapLevelGraphToGrid;

        private Vector2 panOffset;

        private Vector2 drag;

        private float zoom;

        private RoomControl hoverRoomControl;

        private Vector2 originalDragRoomPosition;

        private int currentPickerWindow;

        private RoomControl connectionStartControl;

        private bool isDoubleClick;

        private int gridSize = 16;

        private bool lastIsDirty;

        private Dictionary<Type, Type> roomTypeToControlType;
        private Dictionary<Type, Type> connectionTypeToControlType;

        public void OnEnable()
        {
            Selection.selectionChanged += ProjectBrowserLocker.Unlock;
            SetupCustomControls();

            if (LevelGraph != null)
            {
                Initialize(LevelGraph);
            }
        }

        public void OnDisable()
        {
            Selection.selectionChanged -= ProjectBrowserLocker.Unlock;
        }

        public void OnGUI()
        {
            if (LevelGraph == null)
            {
                ClearWindow();
            }

            var e = Event.current;

            HandleControls();

            DrawGrid(gridSize, 0.2f, Color.gray);
            DrawGrid(gridSize * 5, 0.4f, Color.gray);

            // Connections must be drawn before rooms because they must be behind rooms
            DrawConnections();
            DrawCreatingConnection(e);
            DrawRooms();

            DrawMenuBar();

            lastIsDirty = IsDirty();
        }

        protected void Update()
        {
            var isDirtyChanged = lastIsDirty && !IsDirty();

            if (LevelGraph.HasChanges || isDirtyChanged)
            {
                lastIsDirty = IsDirty();
                LevelGraph.HasChanges = false;
                Repaint();
            }
        }

        /// <summary>
        /// Initialize the window with a given level graph.
        /// </summary>
        /// <param name="levelGraph"></param>
        public void Initialize(LevelGraph levelGraph)
        {
            if (roomTypeToControlType == null || connectionTypeToControlType == null)
            {
                SetupCustomControls();
            }

            LevelGraph = levelGraph;
            CurrentState = State.Idle;
            zoom = LevelGraph.EditorData.Zoom;
            panOffset = LevelGraph.EditorData.PanOffset;
            connectionStartControl = null;

            // Initialize room controls
            roomControls = new List<RoomControl>();
            foreach (var room in LevelGraph.Rooms)
            {
                if (room != null)
                {
                    CreateRoomControl(room);
                }
                else
                {
                    Debug.LogError($"There is a null room in the level graph {levelGraph.name}. This should not happen.");
                }
            }

            // Initialize connection controls
            connectionControls = new List<ConnectionControl>();
            foreach (var connection in LevelGraph.Connections)
            {
                if (connection != null)
                {
                    CreateConnectionControl(connection);
                }
                else
                {
                    Debug.LogError($"There is a null connection in the level graph {levelGraph.name}. This should not happen.");
                }
            }
        }

        /// <summary>
        /// Transform a given position so that it is snapped to the grid.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 GetSnappedToGridPosition(Vector2 position)
        {
            return Round(position, gridSize);
        }

        /// <summary>
        /// Rounds a given number to the given nearest number.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="nearest"></param>
        /// <returns></returns>
        private int Round(float i, int nearest)
        {
            return (int) Math.Round(i / (double) nearest) * nearest;
        }

        /// <summary>
        /// Rounds a given vector to the given nearest number.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="nearest"></param>
        /// <returns></returns>
        private Vector2 Round(Vector2 position, int nearest)
        {
            return new Vector2(Round(position.x, nearest), Round(position.y, nearest));
        }

        /// <summary>
        /// Transforms a given world (mouse) position to a local grid position, taking into account pan offset and zoom.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 WorldToGridPosition(Vector2 position)
        {
            return (position - panOffset * zoom) / zoom;
        }

        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="gridSpacing"></param>
        /// <param name="gridOpacity"></param>
        /// <param name="gridColor"></param>
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            gridSpacing = gridSpacing * zoom;

            var widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            var heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            var originalHandleColor = Handles.color;
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            var newOffset = new Vector3((zoom * panOffset.x) % gridSpacing, (zoom * panOffset.y) % gridSpacing, 0);

            // Draw vertical lines
            for (var i = 0; i < widthDivs + 1; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height + gridSpacing, 0f) + newOffset);
            }

            // Draw horizontal lines
            for (var j = 0; j < heightDivs + 1; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width + gridSpacing, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = originalHandleColor;
            Handles.EndGUI();
        }

        /// <summary>
        /// Draws the menu bar.
        /// </summary>
        private void DrawMenuBar()
        {
            var menuBar = new Rect(0, 0, position.width, 20);

            GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();

            if (LevelGraph != null)
            {
                GUILayout.Label($"Selected graph: {LevelGraph.name} {(lastIsDirty ? "*" : "")}");
            }
            else
            {
                GUILayout.Label($"No graph selected");
            }

            var snapToGridNewValue = GUILayout.Toggle(snapToGrid, "Snap to grid", GUILayout.Width(120));
            if (snapToGridNewValue != snapToGrid)
            {
                EdgarSettings.instance.General.SnapLevelGraphToGrid = snapToGridNewValue;
                EdgarSettings.instance.Save();
            }

            if (GUILayout.Button(new GUIContent("Select in inspector"), EditorStyles.toolbarButton, GUILayout.Width(150)))
            {
                if (LevelGraph != null)
                {
                    Selection.activeObject = LevelGraph;
                }
            }

            if (GUILayout.Button(new GUIContent("Select level graph"), EditorStyles.toolbarButton, GUILayout.Width(150)))
            {
                // Create a window picker control ID
                currentPickerWindow = GUIUtility.GetControlID(FocusType.Passive) + 100;

                // Use the ID you just created
                EditorGUIUtility.ShowObjectPicker<LevelGraph>(null, false, string.Empty, currentPickerWindow);
            }

            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == currentPickerWindow)
            {
                currentPickerWindow = -1;
                var levelGraph = EditorGUIUtility.GetObjectPickerObject() as LevelGraph;

                if (levelGraph != null)
                {
                    Initialize(levelGraph);
                }
                else
                {
                    ClearWindow();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        #if UNITY_2019_1_OR_NEWER
        private bool IsDirty()
        {
            if (LevelGraph == null)
            {
                return false;
            }

            return EditorUtility.IsDirty(LevelGraph);
        }
        #else
        private MethodInfo isDirtyMethod;
        private bool IsDirty()
        {
            if (LevelGraph == null)
            {
                return false;
            }

            if (isDirtyMethod == null)
            {
                var type = typeof(EditorUtility);
                isDirtyMethod = type.GetMethod("IsDirty", BindingFlags.NonPublic | BindingFlags.Static, null, CallingConventions.Any, new[] {typeof(UnityEngine.Object)}, null);
            }

            if (isDirtyMethod != null)
            {
                var isDirty = (bool) isDirtyMethod.Invoke(this, new object[] {LevelGraph});
                return isDirty;
            }

            return false;
        }
        #endif

        private void DrawRooms()
        {
            foreach (var roomControl in roomControls)
            {
                roomControl.Draw(panOffset, zoom);
            }
        }

        private void DrawConnections()
        {
            foreach (var connectionControl in connectionControls)
            {
                connectionControl.Draw(panOffset, zoom, false);
            }
        }

        private void DrawCreatingConnection(Event e)
        {
            if (CurrentState == State.CreateConnection)
            {
                var color = Handles.color;
                Handles.color = Color.white;
                Handles.DrawLine(connectionStartControl.GetRect(panOffset, zoom).center, e.mousePosition);
                Handles.color = color;
            }
        }

        private void SetDirtyInternal()
        {
            EditorUtility.SetDirty(LevelGraph);
            SaveData();
        }

        private void SaveData(bool setDirty = false)
        {
            if (LevelGraph != null)
            {
                var hasChanges = LevelGraph.EditorData.PanOffset != panOffset || LevelGraph.EditorData.Zoom != zoom;

                LevelGraph.EditorData.PanOffset = panOffset;
                LevelGraph.EditorData.Zoom = zoom;

                // Should the metadata make the level graph dirty?
                if (setDirty && hasChanges)
                {
                    EditorUtility.SetDirty(LevelGraph);
                }
            }
        }

        private void OnDestroy()
        {
            SaveData(true);
        }

        private void OnLostFocus()
        {
            SaveData(true);
        }

        private void ClearWindow()
        {
            LevelGraph = null;
            connectionControls.Clear();
            roomControls.Clear();
        }

        public enum State
        {
            Idle,
            HoldGrid,
            DragGrid,
            HoldRoom,
            DragRoom,
            CreateConnection
        }
    }
}