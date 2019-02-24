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
		/// Computes a polygon from its outline points.
		/// </summary>
		/// <param name="outlinePoints"></param>
		/// <param name="allPoints"></param>
		/// <returns></returns>
		public GridPolygon GetPolygonFromOutline(HashSet<IntVector2> outlinePoints, HashSet<IntVector2> allPoints)
		{
			var orderedDirections = new Dictionary<IntVector2, List<IntVector2>>()
			{
				{ IntVector2Helper.Top, new List<IntVector2>() { IntVector2Helper.Left, IntVector2Helper.Top, IntVector2Helper.Right } },
				{ IntVector2Helper.Right, new List<IntVector2>() { IntVector2Helper.Top, IntVector2Helper.Right, IntVector2Helper.Bottom } },
				{ IntVector2Helper.Bottom, new List<IntVector2>() { IntVector2Helper.Right, IntVector2Helper.Bottom, IntVector2Helper.Left } },
				{ IntVector2Helper.Left, new List<IntVector2>() { IntVector2Helper.Bottom, IntVector2Helper.Left, IntVector2Helper.Top } },
			};

			var (startingPoint, startingDirection) = GetClockwiseOutlineDirection(outlinePoints, allPoints);
			var polygonPoints = new List<IntVector2>();
			var currentPoint = startingPoint + startingDirection;
			var firstPoint = currentPoint;
			var previousDirection = startingDirection;
			var first = true;

			while (true)
			{
				var foundNeighbour = false;
				var currentDirection = new IntVector2();

				foreach (var directionVector in orderedDirections[previousDirection])
				{
					var newPoint = currentPoint + directionVector;

					if (outlinePoints.Contains(newPoint))
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
		/// Gets a point on the outline of the polygon together with a clockwise direction
		/// to the next point on the outline.
		/// </summary>
		/// <param name="outlinePoints"></param>
		/// <param name="allPoints"></param>
		/// <returns></returns>
		protected (IntVector2 point, IntVector2 direction) GetClockwiseOutlineDirection(HashSet<IntVector2> outlinePoints, HashSet<IntVector2> allPoints)
		{
			var startingPoint = outlinePoints.First();

			foreach (var directionVectorTmp in DirectionVectors)
			{
				var directionVector = directionVectorTmp;
				var firstPoint = startingPoint;
				var secondPoint = startingPoint + directionVector;

				if (!outlinePoints.Contains(secondPoint))
					continue;

				// Switch points and direction to make the computation easier.
				// We will have to handle only Top and Right directions.
				if (directionVector == IntVector2Helper.Bottom || directionVector == IntVector2Helper.Left)
				{
					var tmp = firstPoint;
					firstPoint = secondPoint;
					secondPoint = tmp;
					directionVector = -1 * directionVector;
				}

				if (directionVector == IntVector2Helper.Top)
				{
					// Check if polygon points are on the left side or on the right side
					var onLeftSide = allPoints.Contains(firstPoint + IntVector2Helper.Left);
					var onRightSide = allPoints.Contains(firstPoint + IntVector2Helper.Right);

					// If there are points on both sides of the edge, then the edge is not part of the outline
					if (onLeftSide && onRightSide)
					{
						continue;
					}

					// If there are no points on both sides, then the room shape is not valid
					if (!onLeftSide && !onRightSide)
					{
						throw new ArgumentException("Invalid room shape");
					}

					if (onLeftSide)
					{
						return (secondPoint, IntVector2Helper.Bottom);
					}
					else // if (onRightSide)
					{
						return (firstPoint, IntVector2Helper.Top);
					}
				}
				else // if (directionVector == IntVector2Helper.Right)
				{
					// Check if polygon points are on the left side or on the right side
					var onTopSide = allPoints.Contains(firstPoint + IntVector2Helper.Top);
					var onBottomSide = allPoints.Contains(firstPoint + IntVector2Helper.Bottom);

					// If there are points on both sides of the edge, then the edge is not part of the outline
					if (onTopSide && onBottomSide)
					{
						continue;
					}

					// If there are no points on both sides, then the room shape is not valid
					if (!onTopSide && !onBottomSide)
					{
						throw new ArgumentException("Invalid room shape");
					}

					if (onTopSide)
					{
						return (secondPoint, IntVector2Helper.Left);
					}
					else // if (onBottomSide)
					{
						return (firstPoint, IntVector2Helper.Right);
					}
				}
			}

			throw new ArgumentException("Invalid room shape");
		}

		/// <summary>
		/// Computes a polygon from points on given tilemaps.
		/// </summary>
		/// <param name="tilemaps"></param>
		/// <returns></returns>
		public GridPolygon GetPolygonFromTilemaps(IEnumerable<Tilemap> tilemaps)
		{
			var usedTiles = GetUsedTiles(tilemaps);
			return GetPolygonFromOutline(GetPolygonOutline(usedTiles), usedTiles);
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
		/// Computes an outline of a polygon formed by non-null tiles in given tilemaps.
		/// </summary>
		/// <param name="tilemaps"></param>
		/// <returns></returns>
		public HashSet<IntVector2> GetPolygonOutline(IEnumerable<Tilemap> tilemaps)
		{
			return GetPolygonOutline(GetUsedTiles(tilemaps));
		}

		/// <summary>
		/// Computes an outline of a polygon formed by given tiles.
		/// </summary>
		/// <param name="usedTiles"></param>
		/// <returns></returns>
		public HashSet<IntVector2> GetPolygonOutline(HashSet<IntVector2> usedTiles)
		{
			if (usedTiles.Count == 0)
			{
				throw new ArgumentException("There are no used tiles");
			}

			var borderPoints = new HashSet<IntVector2>();

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

		/// <summary>
		/// Computes a room description from a given room template.
		/// </summary>
		/// <param name="roomTemplate"></param>
		/// <returns></returns>
		public RoomDescription GetRoomDescription(GameObject roomTemplate)
		{
			var polygon = GetPolygonFromTilemaps(roomTemplate.GetComponentsInChildren<Tilemap>());
			var doors = roomTemplate.GetComponent<Doors>();

			if (doors == null)
			{
				throw new DungeonGeneratorException($"Room template \"{roomTemplate.name}\" does not have any doors assigned.");
			}

			var doorMode = doors.GetDoorMode();
			var roomDescription = new RoomDescription(polygon, doorMode);

			return roomDescription;
		}

		/// <summary>
		/// Computes the length of a given corridor.
		/// </summary>
		/// <param name="roomDescription"></param>
		/// <returns></returns>
		public int GetCorridorLength(RoomDescription roomDescription)
		{
			var doorsHandler = DoorHandler.DefaultHandler;
			var doorPositions = doorsHandler.GetDoorPositions(roomDescription.Shape, roomDescription.DoorsMode);

			if ((doorPositions.Count != 2
			    || doorPositions.Any(x => x.Line.Length != 0)
			    || doorPositions[0].Line.GetDirection() != GeneralAlgorithms.DataStructures.Common.OrthogonalLine.GetOppositeDirection(doorPositions[1].Line.GetDirection()))
				&& !((doorPositions.Count == 3 || doorPositions.Count == 4) && doorPositions.All(x => x.Length == 0))
				)
			{
				throw new ArgumentException("Corridors must currently have exactly 2 door positions that are on the opposite sides of the corridor.");
			}

			var firstLine = doorPositions[0].Line;
			var secondLine = doorPositions[1].Line;

			if (firstLine.Equals(secondLine))
			{
				secondLine = doorPositions.Select(x => x.Line).First(x => !x.Equals(secondLine));
			}

			if (firstLine.GetDirection() == GeneralAlgorithms.DataStructures.Common.OrthogonalLine.Direction.Bottom || firstLine.GetDirection() == GeneralAlgorithms.DataStructures.Common.OrthogonalLine.Direction.Top)
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
