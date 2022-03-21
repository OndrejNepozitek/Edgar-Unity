using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Edgar.Unity.Editor
{
    /// <summary>
    /// Base class for displaying connections in the level graph editor window.
    /// </summary>
    public class ConnectionControl
    {
        /// <summary>
        /// The connection that this control represents/displays.
        /// </summary>
        public ConnectionBase Connection { get; private set; }

        /// <summary>
        /// Control of the From room.
        /// </summary>
        public RoomControl From { get; private set; }

        /// <summary>
        /// Control of the To room.
        /// </summary>
        public RoomControl To { get; private set; }

        /// <summary>
        /// Default width of the connection handle.
        /// </summary>
        public static readonly float DefaultHandleWidth = 12;

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public void Initialize(ConnectionBase connection, RoomControl from, RoomControl to)
        {
            Connection = connection;
            From = from;
            To = to;
        }

        /// <summary>
        /// Returns the Rect that will be used for determining if a user interacted with the handle of the connection.
        /// </summary>
        /// <param name="gridOffset">Offset of the level graph editor window.</param>
        /// <param name="zoom">Zoom of the level graph editor window.</param>
        /// <returns></returns>
        public virtual Rect GetHandleRect(Vector2 gridOffset, float zoom)
        {
            var width = DefaultHandleWidth * zoom;

            var handleCenter = Vector2.Lerp(From.GetRect(gridOffset, zoom).center, To.GetRect(gridOffset, zoom).center, 0.5f);
            var rect = new Rect(handleCenter.x - width / 2.0f, handleCenter.y - width / 2.0f, width, width);

            return rect;
        }

        /// <summary>
        /// Draws the room control.
        /// </summary>
        /// <remarks>
        /// It is advised to use the <see cref="GetHandleRect"/> method for the base position and shape of the connection handle.
        /// </remarks>
        /// <param name="gridOffset">Offset of the level graph editor window.</param>
        /// <param name="zoom">Zoom of the level graph editor window.</param>
        public virtual void Draw(Vector2 gridOffset, float zoom)
        {
            var style = Connection.GetEditorStyle(IsSelected());

            // Draw a line between the From and To rooms
            var oldColor = Handles.color;
            Handles.color = style.LineColor;
            Handles.DrawLine(From.GetRect(gridOffset, zoom).center, To.GetRect(gridOffset, zoom).center);
            Handles.color = oldColor;

            // Draw the connection handle
            var oldBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = style.HandleBackgroundColor;
            var rectStyle = new GUIStyle(LevelGraphEditorStyles.ConnectionHandle);
            GUI.Box(GetHandleRect(gridOffset, zoom), string.Empty, rectStyle);
            GUI.backgroundColor = oldBackgroundColor;
        }

        /// <summary>
        /// Checks if the connection is currently selected in the editor.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsSelected()
        {
            return Selection.objects.Contains(Connection);
        }
    }
}