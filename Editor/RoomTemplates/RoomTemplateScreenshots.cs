using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Editor
{
    public class RoomTemplateScreenshots
    {
        private static Vector3 ComputeCenter(GameObject gameObject)
        {
            var tilemaps = gameObject.GetComponentsInChildren<Tilemap>().ToList();
            return PostProcessUtilsGrid2D.GetTilemapsCenter(tilemaps, true);
        }

        #if OndrejNepozitekEdgar
        [MenuItem("Edgar debug/Room template screenshot with center %g")]
        public static void TakeScreenshotWithCenter()
        {
            TakeScreenshot(true);
        }

        [MenuItem("Edgar debug/Room template screenshot without center %h")]
        public static void TakeScreenshotWithoutCenter()
        {
            TakeScreenshot(false);
        }

        public static void TakeScreenshot(bool center)
        {
            SceneView sw = SceneView.lastActiveSceneView;
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            var gameObject = prefabStage.prefabContentsRoot;

            var name = gameObject.name;
            name = name.Replace(" ", "_");
            name = name.ToLower();

            var position = sw.position;

            if (center)
            {
                sw.LookAtDirect(ComputeCenter(gameObject), Quaternion.identity);
                SceneView.RepaintAll();
            }

            // Get screen position and sizes
            var vec2Position = position.position + new Vector2(2, 60);
            var sizeX = position.width;
            var sizeY = position.height - 42;

            // Take Screenshot at given position sizes
            var colors = InternalEditorUtility.ReadScreenPixel(vec2Position, (int) sizeX, (int) sizeY);

            // write result Color[] data into a temporal Texture2D
            var result = new Texture2D((int) sizeX, (int) sizeY);
            result.SetPixels(colors);

            byte[] pngData = result.EncodeToPNG();
            var path =
                "C:\\Users\\ondra\\Projects\\JavaScript\\Edgar-Unity\\docs\\assets\\2d\\examples\\example2\\";

            FileStream file = File.Create($"{path}{name}.png");

            if (!file.CanWrite)
            {
                Debug.LogError("Unable to capture editor screenshot, Failed to open file for writing");
            }

            file.Write(pngData, 0, pngData.Length);

            file.Close();

            Debug.Log($"Room template screenshot taken - {name}.png");
        }
        #endif
    }
}