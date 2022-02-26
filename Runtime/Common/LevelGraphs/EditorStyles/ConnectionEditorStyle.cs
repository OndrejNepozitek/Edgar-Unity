using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Editor style for connection nodes in the level graph editor.
    /// </summary>
    public class ConnectionEditorStyle
    {
        #if UNITY_2019_1_OR_NEWER
        private static readonly Color DefaultBackgroundColor = new Color(0.4f, 0.4f, 0.4f, 0.9f);
        #else
        private static readonly Color DefaultBackgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.9f);
        #endif

        /// <summary>
        /// Background color of the connection handle.
        /// </summary>
        public Color HandleBackgroundColor { get; set; } = DefaultBackgroundColor;

        /// <summary>
        /// Color of the line that connects two room nodes.
        /// </summary>
        public Color LineColor { get; set; } = Color.white;
    }
}