using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using GeneralAlgorithms.Algorithms.Polygons;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Polygons;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public class Polygon2D
    {
        private readonly GridPolygon polygon;
        private static readonly IPolygonPartitioning polygonPartitioning = new CachedPolygonPartitioning(new GridPolygonPartitioning());

        public Polygon2D(List<Vector2Int> points)
        {
            polygon = new GridPolygon(points.Select(x => x.ToCustomIntVector2()));
        }

        public Polygon2D(GridPolygon polygon)
        {
            this.polygon = polygon;
        }

        public List<Vector2Int> GetAllPoints()
        {
            var points = new List<Vector2Int>();
            var partitions = polygonPartitioning.GetPartitions(polygon);

            foreach (var rectangle in partitions)
            {
                points.AddRange(GetAllPoints(rectangle));
            }

            return points.Distinct().ToList();
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