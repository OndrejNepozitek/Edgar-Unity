using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public static class ProUtils
    {
        /// <summary>
        /// Takes a screenshot of a given camera.
        /// </summary>
        /// <param name="camera">Camera that is used to take the screenshot.</param>
        /// <param name="orthographicSize">Target orthographic size of the camera that should fit the whole generated level.</param>
        /// <param name="width">Width of the resulting screenshot.</param>
        /// <param name="height">Height of the resulting screenshot.</param>
        /// <returns></returns>
        public static Texture2D TakeScreenshot(Camera camera, float orthographicSize, int width = 500, int height = 500)
        {
            // Save original orthographic size and set the new one
            var originalOrthographicSize = camera.orthographicSize;
            camera.orthographicSize = orthographicSize;

            // Prepare the screenshot texture
            var rect = new Rect(0, 0, width, height);
            var renderTexture = new RenderTexture(width, height, 24);
            var screenShot = new Texture2D(width, height, TextureFormat.RGBA32, false);

            // Render the texture
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            // Cleanup
            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.DestroyImmediate(renderTexture);
            camera.orthographicSize = originalOrthographicSize;

            return screenShot;
        }

        public static List<Type> FindDerivedTypes(Type baseType)
        {
            return baseType
                .Assembly
                .GetTypes()
                .Where(baseType.IsAssignableFrom)
                .ToList();
        }
    }
}