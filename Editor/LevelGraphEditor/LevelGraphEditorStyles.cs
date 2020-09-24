using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class LevelGraphEditorStyles
    {
        private static readonly LevelGraphEditorStyles currentStyles = new LevelGraphEditorStyles();

        public static GUIStyle RoomNode => currentStyles.roomNode;
        private GUIStyle roomNode;

        public static GUIStyle ConnectionHandle => currentStyles.connectionHandle;
        private GUIStyle connectionHandle;

        public LevelGraphEditorStyles()
        {
            InitStyles();
        }

        private void InitStyles()
        {
            roomNode = new GUIStyle();
            roomNode.normal.background = Texture2D.whiteTexture;
            roomNode.normal.textColor = Color.white;
            roomNode.fontSize = 12;
            roomNode.alignment = TextAnchor.MiddleCenter;

            connectionHandle = new GUIStyle();
            connectionHandle.normal.background = Texture2D.whiteTexture;
            connectionHandle.normal.textColor = Color.white;
            connectionHandle.fontSize = 12;
            connectionHandle.alignment = TextAnchor.MiddleCenter;
        }
    }
}