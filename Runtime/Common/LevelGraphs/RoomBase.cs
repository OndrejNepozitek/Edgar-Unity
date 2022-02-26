using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for room in a level graph.
    /// </summary>
    public abstract class RoomBase : ScriptableObject
    {
        /// <summary>
        /// Position of the room in the graph editor.
        /// </summary>
        /// <remarks>
        /// This value is not used by the dungeon generator.
        /// </remarks>
        [HideInInspector]
        public Vector2 Position;

        /// <summary>
        /// Gets all the room templates that are available for the room.
        /// </summary>
        /// <remarks>
        /// This method is used in the default implementation of the input setup task.
        /// If null or an empty list is returned, the input setup will use the default room template from the level graph.
        /// </remarks>
        public abstract List<GameObject> GetRoomTemplates();

        /// <summary>
        /// Gets the display name of the room that is display in the graph editor.
        /// </summary>
        public abstract string GetDisplayName();

        public override string ToString()
        {
            return GetDisplayName();
        }

        /// <summary>
        /// Gets the style for the level graph editor.
        /// Override this to change how are room nodes displayed in the editor.
        /// </summary>
        public virtual RoomEditorStyle GetEditorStyle(bool isFocused)
        {
            if (isFocused)
            {
                return new RoomEditorStyle()
                {
                    TextColor = Color.red
                };
            }
            else
            {
                return new RoomEditorStyle();
            }
        }

        protected virtual void OnValidate()
        {
            LevelGraph.HasChanges = true;
        }
    }
}