#if UNITY_2019_1_OR_NEWER
#else
using System.Collections.Generic;
using Edgar.Geometry;
using NUnit.Framework;
using UnityEngine;

namespace Edgar.Unity.Tests.Runtime
{
    public class Polygon2DTests : TestBase
    {
        [Test]
        public void GetAllPoints_Rectangle()
        {
            var polygon = new Polygon2D(PolygonGrid2D.GetRectangle(6, 10));
            var expectedPoints = new List<Vector2Int>();

            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    expectedPoints.Add(new Vector2Int(i, j));
                }
            }

            var points = polygon.GetAllPoints();

            Assert.That(points, Is.EquivalentTo(expectedPoints));
        }

        [Test]
        public void GetOutlinePoints_Rectangle()
        {
            var polygon = new Polygon2D(PolygonGrid2D.GetRectangle(6, 10));
            var expectedPoints = new List<Vector2Int>();

            // Vertical lines
            for (int i = 0; i <= 10; i++)
            {
                expectedPoints.Add(new Vector2Int(0, i));
                expectedPoints.Add(new Vector2Int(6, i));
            }

            // Horizontal lines
            for (int i = 1; i < 6; i++)
            {
                expectedPoints.Add(new Vector2Int(i, 0));
                expectedPoints.Add(new Vector2Int(i, 10));
            }

            var points = polygon.GetOutlinePoints();

            Assert.That(points, Is.EquivalentTo(expectedPoints));
        }

        [Test]
        public void GetPoints_Rectangle()
        {
            var polygon = new Polygon2D(PolygonGrid2D.GetRectangle(6, 10));
            var expectedPoints = new List<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 10),
                new Vector2Int(6, 10),
                new Vector2Int(6, 0),
            };

            var points = polygon.GetCornerPoints();

            Assert.That(points, Is.EquivalentTo(expectedPoints));
        }
    }
}
#endif