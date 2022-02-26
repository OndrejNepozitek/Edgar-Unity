using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Editor style for room nodes in the level graph editor.
    /// </summary>
    public class RoomEditorStyle
    {
        #if UNITY_2019_1_OR_NEWER
        private static readonly Color DefaultBackgroundColor = new Color(0.4f, 0.4f, 0.4f, 0.9f);
        #else
        private static readonly Color DefaultBackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.85f);
        #endif

        /// <summary>
        /// Background color of the room node.
        /// </summary>
        public Color BackgroundColor { get; set; } = DefaultBackgroundColor;

        /// <summary>
        /// Text color of the room node.
        /// </summary>
        public Color TextColor { get; set; } = Color.white;
    }
}