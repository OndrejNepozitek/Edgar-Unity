using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor.EditorNodes
{
    public class ConnectionNode
    {
        public ConnectionBase Connection { get; }

        public RoomNode From { get; }

        public RoomNode To { get; }

        public ConnectionNode(ConnectionBase connection, RoomNode from, RoomNode to)
        {
            Connection = connection;
            From = from;
            To = to;
        }

        public void Draw(float zoom, Vector2 panOffset)
        {
            Handles.DrawLine(From.GetRect(zoom, panOffset).center, To.GetRect(zoom, panOffset).center);

            var style = new GUIStyle(Selection.activeObject == Connection ? LevelGraphEditorStyles.ConnectionHandleActive : LevelGraphEditorStyles.ConnectionHandle);
            GUI.Box(GetHandleRect(zoom, panOffset), string.Empty, style);
        }

        public Rect GetHandleRect(float zoom, Vector2 panOffset)
        {
            var width = zoom * 12;

            var handleCenter = Vector2.Lerp(From.GetRect(zoom, panOffset).center, To.GetRect(zoom, panOffset).center, 0.5f);
            var rect = new Rect(handleCenter.x - width / 2.0f, handleCenter.y - width / 2.0f, width, width);

            return rect;
        }
    }
}