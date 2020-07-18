using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph.EditorStyles;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    public abstract class ConnectionBase : ScriptableObject
    {
        [HideInInspector]
        public RoomBase From;

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