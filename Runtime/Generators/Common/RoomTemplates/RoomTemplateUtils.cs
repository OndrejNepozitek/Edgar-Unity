using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    [Obsolete("Please use RoomTemplateUtilsGrid2D instead.")]
    public static class RoomTemplateUtils
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
            return RoomTemplateUtilsGrid2D.GetTilemapsRoot(roomTemplate);
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
            return RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate, includeInactive);
        }

        /// <summary>
        /// Gets tilemaps that should be used when copying room template tiles to shared tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static List<Tilemap> GetTilemapsForCopying(ICollection<Tilemap> tilemaps)
        {
            return RoomTemplateUtilsGrid2D.GetTilemapsForCopying(tilemaps);
        }

        /// <summary>
        /// Gets tilemaps that should be used when computing room template outline.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static List<Tilemap> GetTilemapsForOutline(ICollection<Tilemap> tilemaps)
        {
            return RoomTemplateUtilsGrid2D.GetTilemapsForOutline(tilemaps);
        }

        /// <summary>
        /// Gets all tiles that are not null in given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static HashSet<Vector2Int> GetUsedTiles(IEnumerable<Tilemap> tilemaps)
        {
            return RoomTemplateUtilsGrid2D.GetUsedTiles(tilemaps);
        }
    }
}