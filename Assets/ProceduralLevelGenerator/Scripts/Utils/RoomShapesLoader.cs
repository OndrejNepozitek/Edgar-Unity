using GeneralAlgorithms.Algorithms.Common;
using MapGeneration.Interfaces.Core.MapDescriptions;

namespace Assets.ProceduralLevelGenerator.Scripts.Utils
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

	/// <summary>
	/// Class used to convert room templates to the representation used
	/// in the dungeon generator library.
	/// </summary>
	public class RoomShapesLoader
	{
		private static readonly List<IntVector2> DirectionVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.Bottom,
			IntVector2Helper.Left
		};

		private static readonly List<IntVector2> AllDirectionVectors = new List<IntVector2>()
		{
			IntVector2Helper.Top,
			IntVector2Helper.Right,
			IntVector2Helper.Bottom,
			IntVector2Helper.Left,
			IntVector2Helper.TopLeft,
			IntVector2Helper.TopRight,
			IntVector2Helper.BottomLeft,
			IntVector2Helper.BottomRight
		};

		/// <summary>
		/// Computes a polygon from its tiles.
		/// </summary>
        /// <param name="allPoints"></param>
		/// <returns></returns>
		public GridPolygon GetPolygonFromTiles(HashSet<IntVector2> allPoints)
		{
            if (allPoints.Count == 0)
            {
                throw new ArgumentException("There must be at least one point");
            }

			var orderedDirections = new Dictionary<IntVector2, List<IntVector2>>()
			{
				{ IntVector2Helper.Top, new List<IntVector2>() { IntVector2Helper.Left, IntVector2Helper.Top, IntVector2Helper.Right } },
				{ IntVector2Helper.Right, new List<IntVector2>() { IntVector2Helper.Top, IntVector2Helper.Right, IntVector2Helper.Bottom } },
				{ IntVector2Helper.Bottom, new List<IntVector2>() { IntVector2Helper.Right, IntVector2Helper.Bottom, IntVector2Helper.Left } },
				{ IntVector2Helper.Left, new List<IntVector2>() { IntVector2Helper.Bottom, IntVector2Helper.Left, IntVector2Helper.Top } },
			};

            var smallestX = allPoints.Min(x => x.X);
            var smallestXPoints = allPoints.Where(x => x.X == smallestX).ToList();
            var smallestXYPoint = smallestXPoints[smallestXPoints.MinBy(x => x.Y)];

            var startingPoint = smallestXYPoint;
            var startingDirection = IntVector2Helper.Top;

			var polygonPoints = new List<IntVector2>();
			var currentPoint = startingPoint + startingDirection;
			var firstPoint = currentPoint;
			var previousDirection = startingDirection;
			var first = true;

            if (!allPoints.Contains(currentPoint))
            {
                throw new ArgumentException("Invalid room shape.");
            }

			while (true)
			{
				var foundNeighbour = false;
				var currentDirection = new IntVector2();

				foreach (var directionVector in orderedDirections[previousDirection])
				{
					var newPoint = currentPoint + directionVector;

					if (allPoints.Contains(newPoint))
					{
						currentDirection = directionVector;
						foundNeighbour = true;
						break;
					}
				}

				if (!foundNeighbour)
					throw new ArgumentException("Invalid room shape.");

				if (currentDirection != previousDirection)
				{
					polygonPoints.Add(currentPoint);
				}

				currentPoint += currentDirection;
				previousDirection = currentDirection;

				if (first)
				{
					first = false;
				}
				else if (currentPoint == firstPoint)
				{
					break;
				}
            }

			if (!IsClockwiseOriented(polygonPoints))
			{
				polygonPoints.Reverse();
			}

			return new GridPolygon(polygonPoints);
		}

        /// <summary>
		/// Computes a polygon from points on given tilemaps.
		/// </summary>
		/// <param name="tilemaps"></param>
		/// <returns></returns>
		public GridPolygon GetPolygonFromTilemaps(IEnumerable<Tilemap> tilemaps)
		{
			var usedTiles = GetUsedTiles(tilemaps);

            return GetPolygonFromTiles(usedTiles);
		}

		/// <summary>
		/// Gets all tiles that are not null in given tilemaps.
		/// </summary>
		/// <param name="tilemaps"></param>
		/// <returns></returns>
		public HashSet<IntVector2> GetUsedTiles(IEnumerable<Tilemap> tilemaps)
		{
			var usedTiles = new HashSet<IntVector2>();

			foreach (var tilemap in tilemaps)
			{
				foreach (var position in tilemap.cellBounds.allPositionsWithin)
				{
					var tile = tilemap.GetTile(position);

					if (tile == null)
					{
						continue;
					}

					usedTiles.Add(position.ToCustomIntVector2());
				}
			}

			return usedTiles;
		}

        /// <summary>
        /// Computes a room room template from a given room template game object.
        /// </summary>
        /// <param name="roomTemplateGameObject"></param>
        /// <param name="allowedTransformations"></param>
        /// <returns></returns>
        public IRoomTemplate GetRoomDescription(GameObject roomTemplateGameObject, List<Transformation> allowedTransformations = null)
		{
            if (allowedTransformations == null)
            {
				allowedTransformations = new List<Transformation> { Transformation.Identity };
            }

			var polygon = GetPolygonFromTilemaps(roomTemplateGameObject.GetComponentsInChildren<Tilemap>());
            var doors = roomTemplateGameObject.GetComponent<Doors>();

			if (doors == null)
			{
				throw new DungeonGeneratorException($"Room template \"{roomTemplateGameObject.name}\" does not have any doors assigned.");
			}

			var doorMode = doors.GetDoorMode();
			var roomDescription = new RoomTemplate(polygon, doorMode, allowedTransformations);

			return roomDescription;
		}

		///// <summary>
		///// Computes the length of a given corridor.
		///// </summary>
		///// <param name="roomDescription"></param>
		///// <returns></returns>
		//public int GetCorridorLength(RoomDescription roomDescription)
		//{
		//	var doorsHandler = DoorHandler.DefaultHandler;
		//	var doorPositions = doorsHandler.GetDoorPositions(roomDescription.Shape, roomDescription.DoorsMode);

		//	if ((doorPositions.Count != 2
		//	    || doorPositions.Any(x => x.Line.Length != 0)
		//	    || doorPositions[0].Line.GetDirection() != GeneralAlgorithms.DataStructures.Common.OrthogonalLine.GetOppositeDirection(doorPositions[1].Line.GetDirection()))
		//		&& !((doorPositions.Count == 3 || doorPositions.Count == 4) && doorPositions.All(x => x.Length == 0))
		//		)
		//	{
		//		throw new ArgumentException("Corridors must currently have exactly 2 door positions that are on the opposite sides of the corridor.");
		//	}

		//	var firstLine = doorPositions[0].Line;
		//	var secondLine = doorPositions[1].Line;

		//	if (firstLine.Equals(secondLine))
		//	{
		//		secondLine = doorPositions.Select(x => x.Line).First(x => !x.Equals(secondLine));
		//	}

		//	if (firstLine.GetDirection() == GeneralAlgorithms.DataStructures.Common.OrthogonalLine.Direction.Bottom || firstLine.GetDirection() == GeneralAlgorithms.DataStructures.Common.OrthogonalLine.Direction.Top)
		//	{
		//		return Math.Abs(firstLine.From.X - secondLine.From.X);
		//	}
		//	else
		//	{
		//		return Math.Abs(firstLine.From.Y - secondLine.From.Y);
		//	}
		//}

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
