using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor.EditorNodes
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
            var rect = GetRect(zoom, gridOffset);

            var style = new GUIStyle(Selection.activeObject == Room ? LevelGraphEditorStyles.RoomNodeActive : LevelGraphEditorStyles.RoomNode);
            style.fontSize = (int) (style.fontSize * zoom);

            GUI.Box(rect, Room.GetDisplayName(), style);
        }
    }
}