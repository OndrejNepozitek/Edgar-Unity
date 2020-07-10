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
            var style = Connection.GetEditorStyle(Selection.activeObject == Connection);

            var oldColor = Handles.color;
            Handles.color = style.LineColor;
            Handles.DrawLine(From.GetRect(zoom, panOffset).center, To.GetRect(zoom, panOffset).center);
            Handles.color = oldColor;

            var oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = style.HandleBackgroundColor;
            var rectStyle = new GUIStyle(LevelGraphEditorStyles.ConnectionHandle);
            GUI.Box(GetHandleRect(zoom, panOffset), string.Empty, rectStyle);
            GUI.backgroundColor = oldBackgroundColor;
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