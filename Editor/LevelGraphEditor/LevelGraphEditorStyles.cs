using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor
{
    public class LevelGraphEditorStyles
    {
        private static readonly LevelGraphEditorStyles currentStyles = new LevelGraphEditorStyles();

        public static GUIStyle RoomNode => currentStyles.roomNode;
        private GUIStyle roomNode;

        public static GUIStyle RoomNodeActive => currentStyles.roomNodeActive;
        private GUIStyle roomNodeActive;

        public static GUIStyle ConnectionHandle => currentStyles.connectionHandle;
        private GUIStyle connectionHandle;

        public static GUIStyle ConnectionHandleActive => currentStyles.connectionHandleActive;
        private GUIStyle connectionHandleActive;

        public LevelGraphEditorStyles()
        {
            InitStyles();
        }

        private void InitStyles()
        {
            roomNode = new GUIStyle();
            roomNode.normal.background = MakeTexture(1, 1, new Color(0.2f, 0.2f, 0.2f, 0.85f));
            roomNode.normal.textColor = Color.white;
            roomNode.fontSize = 12;
            roomNode.alignment = TextAnchor.MiddleCenter;

            roomNodeActive = new GUIStyle();
            roomNodeActive.normal.background = MakeTexture(1, 1, new Color(0.2f, 0.2f, 0.2f, 0.85f));
            roomNodeActive.normal.textColor = Color.red;
            roomNodeActive.fontSize = 12;
            roomNodeActive.alignment = TextAnchor.MiddleCenter;

            connectionHandle = new GUIStyle();
            connectionHandle.normal.background = MakeTexture(1, 1, new Color(0.3f, 0.3f, 0.3f, 0.9f));
            connectionHandle.normal.textColor = Color.white;
            connectionHandle.fontSize = 12;
            connectionHandle.alignment = TextAnchor.MiddleCenter;

            connectionHandleActive = new GUIStyle();
            connectionHandleActive.normal.background = MakeTexture(1, 1, new Color(0.8f, 0f, 0f, 0.8f));
            connectionHandleActive.normal.textColor = Color.white;
            connectionHandleActive.fontSize = 12;
            connectionHandleActive.alignment = TextAnchor.MiddleCenter;
        }

        private Texture2D MakeTexture(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}