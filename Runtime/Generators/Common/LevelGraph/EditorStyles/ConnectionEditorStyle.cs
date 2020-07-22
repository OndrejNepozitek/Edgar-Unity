using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph.EditorStyles
{
    /// <summary>
    /// Editor style for connection nodes in the level graph editor.
    /// </summary>
    public class ConnectionEditorStyle
    {
        /// <summary>
        /// Background color of the connection handle.
        /// </summary>
        public Color HandleBackgroundColor { get; set; } = new Color(0.3f, 0.3f, 0.3f, 0.9f);

        /// <summary>
        /// Color of the line that connects two room nodes.
        /// </summary>
        public Color LineColor { get; set; } = Color.white;
    }
}
