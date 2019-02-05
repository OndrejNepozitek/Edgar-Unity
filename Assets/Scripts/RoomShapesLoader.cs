namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneralAlgorithms.DataStructures.Polygons;
	using GeneratorPipeline.DungeonGenerators;
	using GeneratorPipeline.RoomTemplates.Doors;
	using MapGeneration.Core.Doors;
	using MapGeneration.Core.MapDescriptions;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class RoomShapesLoader
	{
		private static readonly List<IntVector2> DirectionVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.Down,
			IntVector2Helper.Left
		};

		private static readonly List<IntVector2> AllDirectionVectors = new List<IntVector2>()
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

		public GridPolygon GetPolygonFromOutline(HashSet<IntVector2> pointsOriginal)
		{
			var points = new HashSet<IntVector2>(pointsOriginal);
			var polygonPoints = new List<IntVector2>();

			var currentPoint = points.First();
			var currentDirection = IntVector2Helper.Top;

			while (points.Count != 0)
			{
				var found = 0;
				var foundDirection = new IntVector2(0, 0);

				foreach (var directionVector in DirectionVectors)
				{
					var newPoint = currentPoint + directionVector;

					if (points.Contains(newPoint))
					{
						foundDirection = directionVector;
						found++;
					}
				}

				if (pointsOriginal.Count - points.Count < 2)
				{
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
					}
				}

				currentPoint += foundDirection;
				currentDirection = foundDirection;
				points.Remove(currentPoint);
			}

			if (polygonPoints.Count % 2 == 1)
			{
				polygonPoints.Add(currentPoint);
			}

			if (!IsClockwiseOriented(polygonPoints))
			{
				polygonPoints.Reverse();
			}

			return new GridPolygon(polygonPoints);
		}

		public GridPolygon GetPolygonFromTilemap(Tilemap tilemap)
		{
			return GetPolygonFromOutline(GetPolygonOutline(tilemap));
		}

		public HashSet<IntVector2> GetPolygonOutline(Tilemap tilemap)
		{
			var usedTiles = new HashSet<IntVector2>();
			var borderPoints = new HashSet<IntVector2>();

			foreach (var position in tilemap.cellBounds.allPositionsWithin)
			{
				var tile = tilemap.GetTile(position);

				if (tile == null)
				{
					continue;
				}

				usedTiles.Add(position.ToCustomIntVector2());
			}

			foreach (var tile in usedTiles)
			{
				foreach (var directionVector in AllDirectionVectors)
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

		public RoomDescription GetRoomDescription(GameObject roomTemplate)
		{
			var tilemap = roomTemplate.GetComponentInChildren<Tilemap>();
			tilemap.CompressBounds();
			var polygon = GetPolygonFromTilemap(tilemap);
			var doors = roomTemplate.GetComponent<Doors>();

			if (doors == null)
			{
				throw new DungeonGeneratorException($"Room template \"{roomTemplate.name}\" does not have any doors assigned.");
			}

			var doorMode = doors.GetDoorMode();
			var roomDescription = new RoomDescription(polygon, doorMode);

			return roomDescription;
		}

		public int GetCorridorLength(RoomDescription roomDescription)
		{
			var doorsHandler = DoorHandler.DefaultHandler;
			var doorPositions = doorsHandler.GetDoorPositions(roomDescription.Shape, roomDescription.DoorsMode);

			if (doorPositions.Count != 2
			    || doorPositions.Any(x => x.Line.Length != 0)
			    || doorPositions[0].Line.GetDirection() != OrthogonalLine.GetOppositeDirection(doorPositions[1].Line.GetDirection()))
			{
				throw new ArgumentException("Corridors must currently have exactly 2 door positions that are on the opposite sides of the corridor.");
			}

			var firstLine = doorPositions[0].Line;
			var secondLine = doorPositions[1].Line;

			if (firstLine.GetDirection() == OrthogonalLine.Direction.Bottom || firstLine.GetDirection() == OrthogonalLine.Direction.Top)
			{
				return Math.Abs(firstLine.From.X - secondLine.From.X);
			}
			else
			{
				return Math.Abs(firstLine.From.Y - secondLine.From.Y);
			}
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
