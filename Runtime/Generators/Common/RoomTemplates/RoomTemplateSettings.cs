using System;
using GeneralAlgorithms.DataStructures.Polygons;
using MapGeneration.Core.MapDescriptions.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates
{
    /// <summary>
    /// Component that is attached to each room template game objects and contains basic settings.
    /// </summary>
    public class RoomTemplateSettings : MonoBehaviour
    {
        public RepeatMode RepeatMode = RepeatMode.AllowRepeat;

        public bool IsOutlineValid()
        {
            return GetOutline() != null;
        }

        public GridPolygon GetOutline()
        {
            try
            {
                var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(gameObject);

                return polygon;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public void AddOutlineOverride()
        {
            if (HasOutlineOverride())
            {
                return;
            }

            var tilemapsRoot = RoomTemplateUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = new GameObject(GeneratorConstants.OutlineOverrideLayerName);
            outlineOverride.transform.parent = tilemapsRoot.transform;
            outlineOverride.AddComponent<Tilemap>();
            outlineOverride.AddComponent<TilemapRenderer>();
            outlineOverride.AddComponent<OutlineOverride>();
            outlineOverride.GetComponent<TilemapRenderer>().sortingOrder = 1000;
        }

        public void RemoveOutlineOverride()
        {
            if (!HasOutlineOverride())
            {
                return;
            }

            var tilemapsRoot = RoomTemplateUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideLayerName).gameObject;
            PostProcessUtils.Destroy(outlineOverride);
        }

        public bool HasOutlineOverride()
        {
            var tilemapsRoot = RoomTemplateUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideLayerName);

            return outlineOverride != null;
        }
    }
}