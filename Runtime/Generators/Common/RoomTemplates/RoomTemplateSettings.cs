using System;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    /// <summary>
    /// Component that is attached to each room template game objects and contains basic settings.
    /// </summary>
    public class RoomTemplateSettings : MonoBehaviour
    {
        public RoomTemplateRepeatMode RepeatMode = RoomTemplateRepeatMode.AllowRepeat;

        public PolygonGrid2D GetOutline()
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