using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
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

        // TODO: move somewhere else
        public static List<Tilemap> GetRoomTemplateTilemaps(GameObject roomTemplate)
        {
            var tilemapsHolder = roomTemplate.transform.Find(GeneratorConstants.TilemapsRootName)?.gameObject ?? roomTemplate;
            var tilemaps = new List<Tilemap>();

            foreach (var childTransform in tilemapsHolder.transform.Cast<Transform>())
            {
                var tilemap = childTransform.gameObject.GetComponent<Tilemap>();

                if (tilemap != null)
                {
                    tilemaps.Add(tilemap);
                }
            }

            // TODO: return tilemaps or GameObjects?
            return tilemaps;
        }

        // TODO: move somewhere else
        public static void InitializeSharedTilemaps(GeneratedLevel level, ITilemapLayersHandler tilemapLayersHandler)
        {
            // Initialize GameObject that will hold tilemaps
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.transform.parent = level.RootGameObject.transform;

            // Create individual tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        // TODO: move somewhere else
        public static void CopyTilesToSharedTilemaps(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetAllRoomInstances().OrderBy(x => x.IsCorridor))
            {
                CopyTilesToSharedTilemaps(level, roomInstance);
            }
        }

        // TODO: move somewhere else
        public static void CopyTilesToSharedTilemaps(GeneratedLevel level, RoomInstance roomInstance)
        {
            var tilemapsRoot = level.RootGameObject.transform.Find(GeneratorConstants.TilemapsRootName).gameObject;
            var destinationTilemaps = RoomTemplateUtils.GetTilemaps(tilemapsRoot);
            var sourceTilemaps = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);

            PostProcessUtils.CopyTiles(sourceTilemaps, destinationTilemaps, roomInstance.Position);
        }
    }
}