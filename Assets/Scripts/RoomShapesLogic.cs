namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneralAlgorithms.DataStructures.Polygons;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class RoomShapesLogic
	{
		private static readonly List<IntVector2> directionVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.Down,
			IntVector2Helper.Left
		};

		private static readonly List<IntVector2> allDirectionVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.Down,
			IntVector2Helper.Left,
			IntVector2Helper.TopLeft,
			IntVector2Helper.TopRight,
			IntVector2Helper.BottomLeft,
			IntVector2Helper.BottomRight
		};

		private static readonly List<IntVector2> topRightVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.TopRight
		};

		public static GridPolygon GetPolygonFromGridPoints(HashSet<IntVector2> pointsOriginal)
		{
			var points = new HashSet<IntVector2>(pointsOriginal);
			var polygonPoints = new List<IntVector2>();

			var currentPoint = points.First();
			var currentDirection = IntVector2Helper.Top;

			var firstPoint = currentPoint;
			var firstDirection = currentDirection;

			var first = true;
			points.Remove(currentPoint);

			while (points.Count != 0)
			{
				var found = 0;
				var foundDirection = new IntVector2(0, 0);

				foreach (var directionVector in directionVectors)
				{
					var newPoint = currentPoint + directionVector;

					if (points.Contains(newPoint))
					{
						foundDirection = directionVector;
						found++;
					}
				}

				if (first)
				{
					first = false;

					if (found != 2)
						throw new ArgumentException("The first point must be connected to exactly two points");
				}
				else
				{
					if (found != 1) 
						throw new ArgumentException("More than one point is connected");

					if (currentDirection != foundDirection)
					{
						polygonPoints.Add(currentPoint);
					} else if (points.Count == 1 && currentPoint + foundDirection + foundDirection == firstPoint && firstDirection != foundDirection)
					{
						polygonPoints.Add(firstPoint);
					}
				}

				currentPoint += foundDirection;
				currentDirection = foundDirection;
				points.Remove(currentPoint);
			}

			if (!IsClockwiseOriented(polygonPoints))
			{
				polygonPoints.Reverse();
			}

			return new GridPolygon(polygonPoints);
		}

		public static GridPolygon GetPolygonFromTilemap(Tilemap tilemap)
		{
			return GetPolygonFromGridPoints(GetOutline(tilemap));
		}

		public static HashSet<IntVector2> GetOutline(Tilemap tilemap)
		{
			var bounds = tilemap.cellBounds;
			var allTiles = tilemap.GetTilesBlock(bounds);

			var usedTiles = new HashSet<IntVector2>();
			var borderPoints = new HashSet<IntVector2>();

			foreach (var position in tilemap.cellBounds.allPositionsWithin)
			{
				var tile = tilemap.GetTile<Tile>(position);

				if (tile == null)
				{
					continue;
				}

				usedTiles.Add(position.ToCustomIntVector2());
			}

			// PreprocessTilemap(usedTiles);

			foreach (var tile in usedTiles)
			{
				foreach (var directionVector in allDirectionVectors)
				{
					var newTile = tile + directionVector;

					if (!usedTiles.Contains(newTile))
					{
						borderPoints.Add(tile);
						break;
					}
				}
			}

			return borderPoints;
		}

		private static void PreprocessTilemap(HashSet<IntVector2> tiles)
		{
			var toAdd = new HashSet<IntVector2>();

			foreach (var tile in tiles)
			{
				foreach (var directionVector in topRightVectors)
				{
					var newTile = tile + directionVector;

					if (!tiles.Contains(newTile) && !toAdd.Contains(newTile))
					{
						toAdd.Add(newTile);
					}
				}
			}

			foreach (var tile in toAdd)
			{
				tiles.Add(tile);
			}
		}

		private static int PositionToIndex(IntVector2 position, BoundsInt bounds)
		{
			if (position.X < 0 || position.X >= bounds.size.x || position.Y < 0 || position.Y >= bounds.size.y)
			{
				return -1;
			}

			return position.X + position.Y * bounds.size.x;
		}

		private static bool IsClockwiseOriented(IList<IntVector2> points)
		{
			var previous = points[points.Count - 1];
			var sum = 0L;

			foreach (var point in points)
			{
				sum += (point.X - previous.X) * (long)(point.Y + previous.Y);
				previous = point;
			}

			return sum > 0;
		}
	}
}
