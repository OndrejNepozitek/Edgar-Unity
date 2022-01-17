using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.Legacy.GeneralAlgorithms.Algorithms.Polygons;
using UnityEngine;
using Vector2Int = UnityEngine.Vector2Int;

namespace Edgar.Unity
{
    [Serializable]
    public class Polygon2D
    {
        [SerializeField]
        private List<Vector2Int> points;

        private PolygonGrid2D gridPolygon;

        private static readonly IPolygonPartitioning PolygonPartitioning = new CachedPolygonPartitioning(new GridPolygonPartitioning());

        public Polygon2D(List<Vector2Int> points)
        {
            this.points = points;

            if (!RoomTemplateLoaderGrid2D.IsClockwiseOriented(points.Select(x => x.ToCustomIntVector2()).ToList()))
            {
                this.points.Reverse();
            }

            CheckValidity();
        }

        public Polygon2D(PolygonGrid2D polygon)
        {
            points = polygon.GetPoints().Select(x => (Vector2Int) x.ToUnityIntVector3()).ToList();
            gridPolygon = polygon;
        }

        /// <summary>
        /// Returns all the corner points of the polygon.
        /// </summary>
        /// <remarks>
        /// Modifying the collection does not modify the polygon itself.
        /// </remarks>
        public List<Vector2Int> GetCornerPoints()
        {
            return points.ToList();
        }

        /// <summary>
        /// Returns all the outline points of the polygon.
        /// </summary>
        /// <remarks>
        /// Modifying the collection does not modify the polygon itself.
        /// </remarks>
        public List<Vector2Int> GetOutlinePoints()
        {
            return GetGridPolygon()
                .GetLines()
                .SelectMany(x => x.GetPoints())
                .Distinct()
                .Select(x => (Vector2Int) x.ToUnityIntVector3())
                .ToList();
        }

        /// <summary>
        /// Returns all the points of the polygon (outline + inside points).
        /// </summary>
        /// <remarks>
        /// Modifying the collection does not modify the polygon itself.
        /// </remarks>
        public List<Vector2Int> GetAllPoints()
        {
            var points = new List<Vector2Int>();
            var partitions = PolygonPartitioning.GetPartitions(GetGridPolygon());

            foreach (var rectangle in partitions)
            {
                points.AddRange(GetAllPoints(rectangle));
            }

            return points.Distinct().ToList();
        }

        private void CheckValidity()
        {
            GetGridPolygon();
        }

        public PolygonGrid2D GetGridPolygon()
        {
            if (gridPolygon == null)
            {
                gridPolygon = new PolygonGrid2D(points.Select(x => x.ToCustomIntVector2()));
            }

            return gridPolygon;
        }

        private List<Vector2Int> GetAllPoints(RectangleGrid2D rectangle)
        {
            var points = new List<Vector2Int>();

            for (int i = 0; i <= rectangle.Width; i++)
            {
                for (int j = 0; j <= rectangle.Height; j++)
                {
                    points.Add((Vector2Int) (rectangle.A + new EdgarVector2Int(i, j)).ToUnityIntVector3());
                }
            }

            return points;
        }
    }
}