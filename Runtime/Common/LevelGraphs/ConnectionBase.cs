using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Represents a connection between two rooms in a level graph.
    /// </summary>
    /// <remarks>
    /// We usually do not care about the direction, i.e. it does not matter which room is From and which is To.
    /// </remarks>
    public abstract class ConnectionBase : ScriptableObject
    {
        /// <summary>
        /// Room from which this connection leads.
        /// </summary>
        [HideInInspector]
        public RoomBase From;

        /// <summary>
        /// Room to which this connection leads.
        /// </summary>
        [HideInInspector]
        public RoomBase To;

        /// <summary>
        /// Gets the style for the level graph editor.
        /// Override this to change how are connection nodes displayed in the editor.
        /// </summary>
        public virtual ConnectionEditorStyle GetEditorStyle(bool isFocused)
        {
            if (isFocused)
            {
                return new ConnectionEditorStyle()
                {
                    HandleBackgroundColor = new Color(0.8f, 0, 0, 0.8f),
                };
            }
            else
            {
                return new ConnectionEditorStyle();
            }
        }
    }
}