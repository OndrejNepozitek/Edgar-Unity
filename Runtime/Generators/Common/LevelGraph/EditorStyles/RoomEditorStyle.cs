using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph.EditorStyles
{
    /// <summary>
    /// Editor style for room nodes in the level graph editor.
    /// </summary>
    public class RoomEditorStyle
    {
        /// <summary>
        /// Background color of the room node.
        /// </summary>
        public Color BackgroundColor { get; set; } = new Color(0.2f, 0.2f, 0.2f, 0.85f);

        /// <summary>
        /// Text color of the room node.
        /// </summary>
        public Color TextColor { get; set; } = Color.white;
    }
}