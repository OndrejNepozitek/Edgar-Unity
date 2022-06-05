using System.Collections.Generic;
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
        /// Gets all the corridor room templates that are available for the connection.
        /// </summary>
        /// <remarks>
        /// This method is used in the default implementation of the input setup task.
        /// If null or an empty list is returned, the input setup will use the default room template from the level graph.
        /// </remarks>
        public virtual List<GameObject> GetRoomTemplates()
        {
            return null;
        }

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