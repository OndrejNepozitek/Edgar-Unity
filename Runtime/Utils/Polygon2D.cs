using System;
using System.Collections.Generic;
using System.Linq;
using GeneralAlgorithms.Algorithms.Polygons;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Polygons;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Utils
{
    [Serializable]
    public class Polygon2D
    {
        [SerializeField]
        private List<Vector2Int> points;

        private GridPolygon gridPolygon;

        private static readonly IPolygonPartitioning PolygonPartitioning = new CachedPolygonPartitioning(new GridPolygonPartitioning());

        public Polygon2D(List<Vector2Int> points)
        {
            this.points = points;

            if (!RoomTemplatesLoader.IsClockwiseOriented(points.Select(x => x.ToCustomIntVector2()).ToList()))
            {
                this.points.Reverse();
            }

            CheckValidity();
        }

        public Polygon2D(GridPolygon polygon)
        {
            points = polygon.GetPoints().Select(x => (Vector2Int) x.ToUnityIntVector3()).ToList();
            gridPolygon = polygon;
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
                .GetPoints()
                .Select(x => (Vector2Int) x.ToUnityIntVector3())
                .ToList();
        }

        /// <summary>
        /// Returns all the points of the polygon.
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

        public GridPolygon GetGridPolygon()
        {
            if (gridPolygon == null)
            {
                gridPolygon = new GridPolygon(points.Select(x => x.ToCustomIntVector2()));
            }

            return gridPolygon;
        }

        private List<Vector2Int> GetAllPoints(GridRectangle rectangle)
        {
            var points = new List<Vector2Int>();

            for (int i = 0; i < rectangle.Width; i++)
            {
                for (int j = 0; j < rectangle.Height; j++)
                {
                    points.Add((Vector2Int) (rectangle.A + new IntVector2(i, j)).ToUnityIntVector3());
                }
            }

            return points;
        }
    }
}