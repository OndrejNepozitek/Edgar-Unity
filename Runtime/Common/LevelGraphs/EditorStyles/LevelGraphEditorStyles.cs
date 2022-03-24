using UnityEngine;

namespace Edgar.Unity
{
    public class LevelGraphEditorStyles
    {
        private static readonly LevelGraphEditorStyles currentStyles = new LevelGraphEditorStyles();

        public static GUIStyle RoomControl => currentStyles.roomControl;
        private GUIStyle roomControl;

        public static GUIStyle ConnectionHandle => currentStyles.connectionHandle;
        private GUIStyle connectionHandle;

        public LevelGraphEditorStyles()
        {
            InitStyles();
        }

        private void InitStyles()
        {
            roomControl = new GUIStyle();
            roomControl.normal.background = Texture2D.whiteTexture;
            roomControl.normal.textColor = Color.white;
            roomControl.fontSize = 12;
            roomControl.alignment = TextAnchor.MiddleCenter;

            connectionHandle = new GUIStyle();
            connectionHandle.normal.background = Texture2D.whiteTexture;
            connectionHandle.normal.textColor = Color.white;
            connectionHandle.fontSize = 12;
            connectionHandle.alignment = TextAnchor.MiddleCenter;
        }
    }
}