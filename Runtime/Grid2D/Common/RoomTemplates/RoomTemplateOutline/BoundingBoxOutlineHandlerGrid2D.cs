using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Bounding box outline handler.
    /// </summary>
    [AddComponentMenu("Edgar/Grid2D/Bounding Box Outline Handler (Grid2D)")]
    public class BoundingBoxOutlineHandlerGrid2D : MonoBehaviour, IRoomTemplateOutlineHandlerGrid2D
    {
        [Min(0)]
        public int PaddingTop = 0;

        public Polygon2D GetRoomTemplateOutline()
        {
            var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(gameObject);
            var outlineTilemaps = RoomTemplateUtilsGrid2D.GetTilemapsForOutline(tilemaps);
            var usedTiles = RoomTemplateUtilsGrid2D.GetUsedTiles(outlineTilemaps);

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