using System;
using System.Collections.Generic;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Grid2D;
using Edgar.Unity.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    /// <summary>
    /// Class used to convert room templates to the representation used in the dungeon generator library.
    /// </summary>
    [Obsolete("Please use RoomTemplateLoaderGrid2D instead.")]
    public static class RoomTemplatesLoader
    {
        /// <summary>
        /// Computes a polygon from its tiles.
        /// </summary>
        /// <param name="allPoints"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromTiles(HashSet<Vector3Int> allPoints)
        {
            return RoomTemplateLoaderGrid2D.GetPolygonFromTiles(allPoints);
        }

        /// <summary>
        /// Computes a polygon from points on given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromTilemaps(ICollection<Tilemap> tilemaps)
        {
            return RoomTemplateLoaderGrid2D.GetPolygonFromTilemaps(tilemaps);
        }

        /// <summary>
        /// Computes a polygon from points on given tilemaps.
        /// </summary>
        /// <param name="roomTemplate"></param>
        /// <returns></returns>
        public static PolygonGrid2D GetPolygonFromRoomTemplate(GameObject roomTemplate)
        {
            return RoomTemplateLoaderGrid2D.GetPolygonFromRoomTemplate(roomTemplate);
        }

        /// <summary>
        /// Gets all tiles that are not null in given tilemaps.
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <returns></returns>
        public static HashSet<Vector3Int> GetUsedTiles(IEnumerable<Tilemap> tilemaps)
        {
            return RoomTemplateLoaderGrid2D.GetUsedTiles(tilemaps);
        }

        /// <summary>
        ///     Computes a room room template from a given room template game object.
        /// </summary>
        /// <param name="roomTemplatePrefab"></param>
        /// <param name="roomTemplate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetRoomTemplate(GameObject roomTemplatePrefab, out RoomTemplateGrid2D roomTemplate, out ActionResult result)
        {
            return RoomTemplateLoaderGrid2D.TryGetRoomTemplate(roomTemplatePrefab, out roomTemplate, out result);
        }

        public static bool IsClockwiseOriented(IList<EdgarVector2Int> points)
        {
            return RoomTemplateLoaderGrid2D.IsClockwiseOriented(points);
        }
    }
}