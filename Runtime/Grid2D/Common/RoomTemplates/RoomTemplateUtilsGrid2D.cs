using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    public static class RoomTemplateUtilsGrid2D
    {
        /// <summary>
        /// Gets the GameObject that is the parent to all the tilemaps.
        /// </summary>
        /// <remarks>
        /// If there is no child named GeneratorConstants.TilemapsRootName, the room template GameObject
        /// itself is returned to provide backwards compatibility.
        /// </remarks>
        /// <param name="roomTemplate">GameObject representing the room template.</param>
        /// <returns></returns>
        public static GameObject GetTilemapsRoot(GameObject roomTemplate)
        {
            return roomTemplate.transform.Find(GeneratorConstantsGrid2D.TilemapsRootName)?.gameObject ?? roomTemplate;
        }

        /// <summary>
        /// Gets all the tilemap layers from the room template GameObject.
        /// </summary>
        /// <remarks>
        /// Only tilemaps that are direct children of the tilemap root GameObject are returned.
        /// That means that if there is a tilemap that is not a direct child of the tilemaps root
        /// (e.g. it is part of a prefab that should be instantiated alongside the room template),
        /// it is not returned here and is not used to compute room template outlines.
        /// </remarks>
        /// <param name="roomTemplate">GameObject representing the room template.</param>
        /// <param name="includeInactive">Whether inactive tilemaps should be returned.</param>
        /// <returns></returns>
        public static List<Tilemap> GetTilemaps(GameObject roomTemplate, bool includeInactive = true)
        {
            var tilemapsHolder = GetTilemapsRoot(roomTemplate);
            var tilemaps = new List<Tilemap>();

            foreach (var childTransform in tilemapsHolder.transform.Cast<Transform>())
            {
                var tilemap = childTransform.gameObject.GetComponent<Tilemap>();

                if (tilemap != null && (includeInactive || childTransform.gameObject.activeSelf))
                {
                    tilemaps.Add(tilemap);
                }
            }

            return tilemaps;
        }

        /// <summary>
        /// Gets tilemaps that should be used when copying room template tiles to shared tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static List<Tilemap> GetTilemapsForCopying(ICollection<Tilemap> tilemaps)
        {
            return tilemaps
                .Where(x =>
                    x.GetComponent<OutlineOverrideGrid2D>() == null &&
                    (x.GetComponent<IgnoreTilemapGrid2D>() == null ||
                     !x.GetComponent<IgnoreTilemapGrid2D>().IgnoreWhenCopyingTiles)
                ).ToList();
        }

        /// <summary>
        /// Gets tilemaps that should be used when computing room template outline.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static List<Tilemap> GetTilemapsForOutline(ICollection<Tilemap> tilemaps)
        {
            var overrideOutline = tilemaps.FirstOrDefault(x => x.GetComponent<OutlineOverrideGrid2D>());

            if (overrideOutline != null)
            {
                return new List<Tilemap>() {overrideOutline};
            }

            return tilemaps
                .Where(x =>
                    x.GetComponent<IgnoreTilemapGrid2D>() == null ||
                    !x.GetComponent<IgnoreTilemapGrid2D>().IgnoreWhenComputingOutline
                ).ToList();
        }

        /// <summary>
        /// Gets all tiles that are not null in given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static HashSet<Vector2Int> GetUsedTiles(IEnumerable<Tilemap> tilemaps)
        {
            var usedTiles = new HashSet<Vector2Int>();

            foreach (var tilemap in tilemaps)
            {
                foreach (var position in tilemap.cellBounds.allPositionsWithin)
                {
                    var tile = tilemap.GetTile(position);

                    if (tile == null)
                    {
                        continue;
                    }

                    usedTiles.Add((Vector2Int) position);
                }
            }

            return usedTiles;
        }
    }
}