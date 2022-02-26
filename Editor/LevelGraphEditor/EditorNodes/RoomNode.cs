using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class RoomNode
    {
        public RoomBase Room { get; }

        public RoomNode(RoomBase room)
        {
            Room = room;
        }

        public Rect GetRect(float zoom, Vector2 gridOffset)
        {
            var width = 80 * zoom;
            var height = 32 * zoom;

            return new Rect((Room.Position.x + gridOffset.x) * zoom, (Room.Position.y + gridOffset.y) * zoom, width, height);
        }

        public void Draw(float zoom, Vector2 gridOffset)
        {
            var style = Room.GetEditorStyle(Selection.objects.Contains(Room));

            var rect = GetRect(zoom, gridOffset);

            var rectStyle = new GUIStyle(LevelGraphEditorStyles.RoomNode);
            rectStyle.fontSize = (int) (rectStyle.fontSize * zoom);
            rectStyle.normal.textColor = style.TextColor;

            var oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = style.BackgroundColor;
            GUI.Box(rect, Room.GetDisplayName(), rectStyle);
            GUI.backgroundColor = oldBackgroundColor;
        }
    }
}