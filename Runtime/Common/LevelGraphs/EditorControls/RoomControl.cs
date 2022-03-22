using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for displaying rooms in the level graph editor window.
    /// </summary>
    public class RoomControl
    {
        /// <summary>
        /// The room that this control represents/displays.
        /// </summary>
        public RoomBase Room { get; private set; }

        /// <summary>
        /// Default width of the room control.
        /// </summary>
        public static readonly float DefaultWidth = 80;

        /// <summary>
        /// Default height of the room control.
        /// </summary>
        public static readonly float DefaultHeight = 32;

        public void Initialize(RoomBase room)
        {
            Room = room;
        }

        /// <summary>
        /// Returns the Rect that will be used for determining if a user interacted with the control.
        /// </summary>
        /// <param name="gridOffset">Offset of the level graph editor window.</param>
        /// <param name="zoom">Zoom of the level graph editor window.</param>
        /// <returns></returns>
        public virtual Rect GetRect(Vector2 gridOffset, float zoom)
        {
            var width = DefaultWidth * zoom;
            var height = DefaultHeight * zoom;

            return new Rect((Room.Position.x + gridOffset.x) * zoom, (Room.Position.y + gridOffset.y) * zoom, width, height);
        }

        /// <summary>
        /// Draws the room control.
        /// </summary>
        /// <remarks>
        /// It is advised to use the <see cref="GetRect"/> method for the base position and shape of the control.
        /// </remarks>
        /// <param name="gridOffset">Offset of the level graph editor window.</param>
        /// <param name="zoom">Zoom of the level graph editor window.</param>
        public virtual void Draw(Vector2 gridOffset, float zoom)
        {
            var style = Room.GetEditorStyle(IsSelected());

            var rect = GetRect(gridOffset, zoom);

            var rectStyle = new GUIStyle(LevelGraphEditorStyles.RoomControl);
            rectStyle.fontSize = (int) (rectStyle.fontSize * zoom);
            rectStyle.normal.textColor = style.TextColor;

            var oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = style.BackgroundColor;
            GUI.Box(rect, Room.GetDisplayName(), rectStyle);
            GUI.backgroundColor = oldBackgroundColor;
        }

        /// <summary>
        /// Checks if the room is currently selected in the editor.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsSelected()
        {
            #if UNITY_EDITOR
            return Selection.objects.Contains(Room);
            #else
            return false;
            #endif
        }
    }
}