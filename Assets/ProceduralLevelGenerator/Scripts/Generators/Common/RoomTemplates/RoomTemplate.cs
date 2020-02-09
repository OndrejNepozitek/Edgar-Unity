using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using GeneralAlgorithms.DataStructures.Polygons;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates
{
    [ExecuteInEditMode]
    public class RoomTemplate : MonoBehaviour
    {
        public bool IsOutlineValid()
        {
            return GetOutline() != null;
        }

        public GridPolygon GetOutline()
        {
            try
            {
                var roomShapesLoader = new RoomShapesLoader();
                var tilemaps = PostProcessUtils.GetTilemaps(gameObject);
                var polygon = roomShapesLoader.GetPolygonFromTilemaps(tilemaps);

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

            var tilemapsRoot = PostProcessUtils.GetTilemapsRoot(gameObject);

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

            var tilemapsRoot = PostProcessUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideName).gameObject;
            PostProcessUtils.Destroy(outlineOverride);
        }

        public bool HasOutlineOverride()
        {
            var tilemapsRoot = PostProcessUtils.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstants.OutlineOverrideName);

            return outlineOverride != null;
        }
    }
}