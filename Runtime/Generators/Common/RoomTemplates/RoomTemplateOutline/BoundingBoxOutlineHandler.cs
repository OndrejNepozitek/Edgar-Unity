using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateOutline
{
    /// <summary>
    /// Bounding box outline handler.
    /// </summary>
    public class BoundingBoxOutlineHandler : MonoBehaviour, IRoomTemplateOutlineHandler
    {
        [Min(0)]
        public int PaddingTop = 0;

        public Polygon2D GetRoomTemplateOutline()
        {
            var tilemaps = RoomTemplateUtils.GetTilemaps(gameObject);
            var outlineTilemaps = RoomTemplateUtils.GetTilemapsForOutline(tilemaps);
            var usedTiles = RoomTemplateUtils.GetUsedTiles(outlineTilemaps);

            if (usedTiles.Count == 0)
            {
                return null;
            }

            var minX = usedTiles.Min(x => x.x);
            var maxX = usedTiles.Max(x => x.x);
            var minY = usedTiles.Min(x => x.y);
            var maxY = usedTiles.Max(x => x.y) + PaddingTop;

            var polygonPoints = new List<Vector2Int>()
            {
                new Vector2Int(minX, minY),
                new Vector2Int(minX, maxY),
                new Vector2Int(maxX, maxY),
                new Vector2Int(maxX, minY),
            };

            return new Polygon2D(polygonPoints);
        }
    }
}