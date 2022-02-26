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
    [AddComponentMenu("Edgar/Grid2D/Room Template Settings (Grid2D)")]
    public class RoomTemplateSettingsGrid2D : MonoBehaviour
    {
        public RoomTemplateRepeatMode RepeatMode = RoomTemplateRepeatMode.AllowRepeat;

        public PolygonGrid2D GetOutline()
        {
            try
            {
                var polygon = RoomTemplateLoaderGrid2D.GetPolygonFromRoomTemplate(gameObject);

                return polygon;
            }
            catch (InvalidOutlineException)
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

            var tilemapsRoot = RoomTemplateUtilsGrid2D.GetTilemapsRoot(gameObject);
            var outlineOverride = new GameObject(GeneratorConstantsGrid2D.OutlineOverrideLayerName);
            outlineOverride.transform.parent = tilemapsRoot.transform;
            outlineOverride.AddComponent<Tilemap>();
            outlineOverride.AddComponent<TilemapRenderer>();
            outlineOverride.AddComponent<OutlineOverrideGrid2D>();
            outlineOverride.GetComponent<TilemapRenderer>().sortingOrder = 1000;
        }

        public void RemoveOutlineOverride()
        {
            if (!HasOutlineOverride())
            {
                return;
            }

            var tilemapsRoot = RoomTemplateUtilsGrid2D.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstantsGrid2D.OutlineOverrideLayerName).gameObject;
            PostProcessUtilsGrid2D.Destroy(outlineOverride);
        }

        public bool HasOutlineOverride()
        {
            var tilemapsRoot = RoomTemplateUtilsGrid2D.GetTilemapsRoot(gameObject);
            var outlineOverride = tilemapsRoot.transform.Find(GeneratorConstantsGrid2D.OutlineOverrideLayerName);

            return outlineOverride != null;
        }
    }
}