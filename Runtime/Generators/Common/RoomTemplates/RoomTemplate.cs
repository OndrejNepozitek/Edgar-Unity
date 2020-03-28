using System;
using GeneralAlgorithms.DataStructures.Polygons;
using MapGeneration.Interfaces.Core.MapDescriptions;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates
{
    public class RoomTemplate : MonoBehaviour
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
            var outlineOverride = new GameObject(GeneratorConstants.OutlineOverrideName);
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
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideName).gameObject;
            PostProcessUtils.Destroy(outlineOverride);
        }

        public bool HasOutlineOverride()
        {
            var tilemapsRoot = RoomTemplateUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideName);

            return outlineOverride != null;
        }
    }
}