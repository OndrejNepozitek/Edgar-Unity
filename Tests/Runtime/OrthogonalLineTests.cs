using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Edgar.Unity.Tests.Runtime
{
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
    public class OrthogonalLineTests : TestBase
    {
        [Test]
        public void Constructor_ValidLine()
        {
            // Degenerated line (contains only 1 point)
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 3));

            // x direction
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(5, 2, 3));
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(-5, 2, 3));

            // y direction
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 10, 3));
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, -10, 3));

            // z direction
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 20));
            new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, -20));
        }

        [Test]
        public void Constructor_InvalidLine()
        {
            Assert.Throws<ArgumentException>(() => new OrthogonalLine(new Vector3Int(1, 3, 4), new Vector3Int(1, 2, 3)));
        }

        [Test]
        public void Direction()
        {
            // z direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 11)).Direction, Is.EqualTo(new Vector3Int(0, 0, 1)));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 21)).Direction, Is.EqualTo(new Vector3Int(0, 0, 1)));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 9)).Direction, Is.EqualTo(new Vector3Int(0, 0, -1)));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 1)).Direction, Is.EqualTo(new Vector3Int(0, 0, -1)));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 11), new Vector3Int(2, 5, 10)).Direction, Is.EqualTo(new Vector3Int(0, 0, -1)));

            // x direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(5, 5, 10)).Direction, Is.EqualTo(new Vector3Int(1, 0, 0)));

            // y direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 7, 10), new Vector3Int(2, 5, 10)).Direction, Is.EqualTo(new Vector3Int(0, -1, 0)));

            // degenerated direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 7, 10), new Vector3Int(2, 7, 10)).Direction, Is.EqualTo(new Vector3Int(0, 0, 0)));
        }

        [Test]
        public void Length()
        {
            // z direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 11)).Length, Is.EqualTo(2));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 21)).Length, Is.EqualTo(12));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 9)).Length, Is.EqualTo(2));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(2, 5, 1)).Length, Is.EqualTo(10));
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 11), new Vector3Int(2, 5, 10)).Length, Is.EqualTo(2));

            // x direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 5, 10), new Vector3Int(5, 5, 10)).Length, Is.EqualTo(4));

            // y direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 7, 10), new Vector3Int(2, 5, 10)).Length, Is.EqualTo(3));

            // degenerated direction
            Assert.That(new OrthogonalLine(new Vector3Int(2, 7, 10), new Vector3Int(2, 7, 10)).Length, Is.EqualTo(1));
        }

        [Test]
        public void GetPoints()
        {
            {
                // x direction
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(4, 2, 3));
                var points = line.GetPoints();
                var expectedPoints = new List<Vector3Int>()
                {
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(2, 2, 3),
                    new Vector3Int(3, 2, 3),
                    new Vector3Int(4, 2, 3),
                };

                Assert.That(points, Is.EqualTo(expectedPoints));
            }

            {
                // degenerated
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 3));
                var points = line.GetPoints();
                var expectedPoints = new List<Vector3Int>()
                {
                    new Vector3Int(1, 2, 3),
                };

                Assert.That(points, Is.EqualTo(expectedPoints));
            }
        }

        [Test]
        public void GetNthPoint()
        {
            {
                // x direction
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(4, 2, 3));
                var points = new List<Vector3Int>()
                {
                    line.GetNthPoint(0),
                    line.GetNthPoint(1),
                    line.GetNthPoint(2),
                    line.GetNthPoint(3),
                };
                var expectedPoints = new List<Vector3Int>()
                {
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(2, 2, 3),
                    new Vector3Int(3, 2, 3),
                    new Vector3Int(4, 2, 3),
                };

                Assert.That(points, Is.EqualTo(expectedPoints));

                Assert.Throws<ArgumentException>(() => line.GetNthPoint(-1));
                Assert.Throws<ArgumentException>(() => line.GetNthPoint(-2));
                Assert.Throws<ArgumentException>(() => line.GetNthPoint(4));
                Assert.Throws<ArgumentException>(() => line.GetNthPoint(5));
            }

            {
                // degenerated
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 3));

                Assert.That(line.GetNthPoint(0), Is.EqualTo(new Vector3Int(1, 2, 3)));
            }
        }

        [Test]
        public void Contains()
        {
            {
                // x direction positive
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(4, 2, 3));
                var points = new List<Vector3Int>()
                {
                    new Vector3Int(-1, 2, 3),
                    new Vector3Int(0, 2, 3),
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(2, 2, 3),
                    new Vector3Int(3, 2, 3),
                    new Vector3Int(4, 2, 3),
                    new Vector3Int(5, 2, 3),
                    new Vector3Int(6, 2, 3),
                };
                var indices = points.Select(x => line.Contains(x)).ToList();
                var expectedIndices = new List<int>()
                {
                    -1,
                    -1,
                    0,
                    1,
                    2,
                    3,
                    -1,
                    -1,
                };

                Assert.That(indices, Is.EqualTo(expectedIndices));

                // x direction (automatic)
                var lineReversed = new OrthogonalLine(line.To, line.From);
                points.Reverse();
                indices = points.Select(x => lineReversed.Contains(x)).ToList();
                Assert.That(indices, Is.EqualTo(expectedIndices));
            }

            {
                // x direction negative (manual)
                var line = new OrthogonalLine(new Vector3Int(4, 2, 3), new Vector3Int(1, 2, 3));
                var points = new List<Vector3Int>()
                {
                    new Vector3Int(-1, 2, 3),
                    new Vector3Int(0, 2, 3),
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(2, 2, 3),
                    new Vector3Int(3, 2, 3),
                    new Vector3Int(4, 2, 3),
                    new Vector3Int(5, 2, 3),
                    new Vector3Int(6, 2, 3),
                };
                points.Reverse();
                var indices = points.Select(x => line.Contains(x)).ToList();
                var expectedIndices = new List<int>()
                {
                    -1,
                    -1,
                    0,
                    1,
                    2,
                    3,
                    -1,
                    -1,
                };

                Assert.That(indices, Is.EqualTo(expectedIndices));
            }

            {
                // y direction positive
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 5, 3));
                var points = new List<Vector3Int>()
                {
                    new Vector3Int(1, 0, 3),
                    new Vector3Int(1, 1, 3),
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(1, 3, 3),
                    new Vector3Int(1, 4, 3),
                    new Vector3Int(1, 5, 3),
                    new Vector3Int(1, 6, 3),
                    new Vector3Int(1, 7, 3),
                };
                var indices = points.Select(x => line.Contains(x)).ToList();
                var expectedIndices = new List<int>()
                {
                    -1,
                    -1,
                    0,
                    1,
                    2,
                    3,
                    -1,
                    -1,
                };

                Assert.That(indices, Is.EqualTo(expectedIndices));

                // y direction (automatic)
                var lineReversed = new OrthogonalLine(line.To, line.From);
                points.Reverse();
                indices = points.Select(x => lineReversed.Contains(x)).ToList();
                Assert.That(indices, Is.EqualTo(expectedIndices));
            }

            {
                // z direction positive
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 6));
                var points = new List<Vector3Int>()
                {
                    new Vector3Int(1, 2, 1),
                    new Vector3Int(1, 2, 2),
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(1, 2, 4),
                    new Vector3Int(1, 2, 5),
                    new Vector3Int(1, 2, 6),
                    new Vector3Int(1, 2, 7),
                    new Vector3Int(1, 2, 8),
                };
                var indices = points.Select(x => line.Contains(x)).ToList();
                var expectedIndices = new List<int>()
                {
                    -1,
                    -1,
                    0,
                    1,
                    2,
                    3,
                    -1,
                    -1,
                };

                Assert.That(indices, Is.EqualTo(expectedIndices));

                // z direction (automatic)
                var lineReversed = new OrthogonalLine(line.To, line.From);
                points.Reverse();
                indices = points.Select(x => lineReversed.Contains(x)).ToList();
                Assert.That(indices, Is.EqualTo(expectedIndices));
            }

            {
                // degenerated
                var line = new OrthogonalLine(new Vector3Int(1, 2, 3), new Vector3Int(1, 2, 3));
                var points = new List<Vector3Int>()
                {
                    new Vector3Int(-1, 2, 3),
                    new Vector3Int(0, 2, 3),
                    new Vector3Int(1, 2, 3),
                    new Vector3Int(2, 2, 3),
                    new Vector3Int(3, 2, 3),
                };
                var indices = points.Select(x => line.Contains(x)).ToList();
                var expectedIndices = new List<int>()
                {
                    -1,
                    -1,
                    0,
                    -1,
                    -1,
                };

                Assert.That(indices, Is.EqualTo(expectedIndices));
            }
        }

        [Test]
        public void Contains_NonMatchingOtherCoordinates()
        {
            var position = new Vector3Int(7, 1, 0);
            var line = new OrthogonalLine(new Vector3Int(0, 2, 0), new Vector3Int(0, 1, 0));
            var index = line.Contains(position);
            Assert.That(index, Is.EqualTo(-1));
        }
    }
}