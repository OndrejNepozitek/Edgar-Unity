using System;
using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor.EditorNodes;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using UnityEditor;
using UnityEngine;
using ConnectionNode = ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor.EditorNodes.ConnectionNode;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor
{
    public partial class LevelGraphEditor : EditorWindow
    {
        public LevelGraph LevelGraph { get; private set; }

        private List<RoomNode> roomNodes = new List<RoomNode>();
        
        private List<ConnectionNode> connectionNodes = new List<ConnectionNode>();

        public State CurrentState;

        private bool snapToGrid;

        private Vector2 panOffset;

        private Vector2 drag;

        private float zoom;

        private RoomNode hoverRoomNode;

        private Vector2 originalDragRoomPosition;

        private int currentPickerWindow;

        private RoomNode connectionStartNode;

        private bool isDoubleClick;

        private int gridSize = 16;

        public void OnEnable()
        {
            if (LevelGraph != null)
            {
                Initialize(LevelGraph);
            }
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
        }

        /// <summary>
        /// Initialize the window with a given level graph.
        /// </summary>
        /// <param name="levelGraph"></param>
        public void Initialize(LevelGraph levelGraph)
        {
            LevelGraph = levelGraph; 
            CurrentState = State.Idle;
            zoom = LevelGraph.EditorData.Zoom;
            panOffset = LevelGraph.EditorData.PanOffset;
            snapToGrid = EditorPrefs.GetBool(EditorConstants.SnapToGridEditorPrefsKey, false);
            connectionStartNode = null;

            // Initialize room nodes
            roomNodes = new List<RoomNode>();
            foreach (var room in LevelGraph.Rooms)
            {
                if (room != null)
                {
                    CreateRoomNode(room);
                }
                else
                {
                    Debug.LogError($"There is a null room in the level graph {levelGraph.name}. This should not happen.");
                }
            }

            // Initialize connection nodes
            connectionNodes = new List<ConnectionNode>();
            foreach (var connection in LevelGraph.Connections)
            {
                if (connection != null)
                {
                    CreateConnectionNode(connection);
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
                GUILayout.Label($"Selected graph: {LevelGraph.name}"); 
            }
            else
            {
                GUILayout.Label($"No graph selected");
            }

            snapToGrid = GUILayout.Toggle(snapToGrid, "Snap to grid", GUILayout.Width(120));

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

        private void DrawRooms()
        {
            foreach (var roomNode in roomNodes)
            {
                roomNode.Draw(zoom, panOffset);
            }
        }

        private void DrawConnections()
        {
            foreach (var connectionNode in connectionNodes)
            {
                connectionNode.Draw(zoom, panOffset);
            }
        }

        private void DrawCreatingConnection(Event e)
        {
            if (CurrentState == State.CreateConnection)
            {
                Handles.DrawLine(connectionStartNode.GetRect(zoom, panOffset).center, e.mousePosition);
            }
        }

        private void SaveData()
        {
            EditorPrefs.SetBool(EditorConstants.SnapToGridEditorPrefsKey, snapToGrid);

            if (LevelGraph != null)
            {
                var setDirty = LevelGraph.EditorData.PanOffset != panOffset || LevelGraph.EditorData.Zoom != zoom;

                LevelGraph.EditorData.PanOffset = panOffset;
                LevelGraph.EditorData.Zoom = zoom;

                if (setDirty)
                {
                    EditorUtility.SetDirty(LevelGraph);
                }
            }
        }

        private void OnDestroy()
        {
            SaveData();
        }

        private void OnLostFocus()
        {
            SaveData();
        }

        private void ClearWindow()
        {
            LevelGraph = null;
            connectionNodes.Clear();
            roomNodes.Clear();
        }

        public enum State
        {
            Idle, HoldGrid, DragGrid, HoldRoom, DragRoom, CreateConnection
        }
    }
}