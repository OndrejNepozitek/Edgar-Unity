namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using Data2;
	using GeneralAlgorithms.Algorithms.Common;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneralAlgorithms.DataStructures.Polygons;
	using MapGeneration.Core.Doors.DoorModes;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;
	using RoomRotations;
	using TileMapping;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;
	using Debug = UnityEngine.Debug;

	public class DungeonGenerator : MonoBehaviour
	{
		public Rooms Rooms;

		public GameObject Walls;

		public Tile DoorTile;

		public bool ShowElapsedTime;

		public bool AddDoors;

		public bool CorrectWalls;

		public bool CombineTilemaps;

		private IMapLayout<int> lastLayout;

		public void Generate()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (ShowElapsedTime)
			{
				Debug.Log("--- Script started ---");
			}


			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var rooms = new List<RoomDescription>();
			var mapDescriptionsToRooms = new Dictionary<RoomDescription, Room>();

			foreach (var roomSet in Rooms.RoomsSets)
			{
				foreach (var room in roomSet.Rooms)
				{
					var tilemap = room.Tilemap.GetComponentInChildren<Tilemap>();
					var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
					var doors = room.Tilemap.GetComponent<Doors.Doors>();
					var doorLines = new List<OrthogonalLine>();

					foreach (var door in doors.doors)
					{
						var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(), door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

						//if (doorLine.Length == 0)
						//{
						//	continue;
						//}

						doorLines.Add(doorLine);
					}

					var doorMode = new SpecificPositionsMode(doorLines);

					var roomDescription = new RoomDescription(polygon, doorMode);
					rooms.Add(roomDescription);
					mapDescriptionsToRooms.Add(roomDescription, room);


				}
			}

			var mapDescription = new MapDescription<int>();
			var verticesCount = 7;

			for (var i = 0; i < verticesCount; i++)
			{
				mapDescription.AddRoom(i);
			}

			for (var i = 0; i < verticesCount - 1; i++)
			{
				mapDescription.AddPassage(i, i + 1);
			}

			mapDescription.AddPassage(3, 0);
			mapDescription.AddPassage(3, 6);

			mapDescription.AddRoomShapes(rooms);

			if (ShowElapsedTime)
			{
				Debug.Log($"Map description created. {stopwatch.ElapsedMilliseconds / 1000f:F} s");
			}


			var generator = LayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();
			generator.InjectRandomGenerator(new System.Random());

			var layouts = generator.GetLayouts(mapDescription, 1);
			var layout = layouts[0];



			if (ShowElapsedTime)
			{
				Debug.Log($"Layout generated. {stopwatch.ElapsedMilliseconds / 1000f:F} s");
			}
			var parentGameObject = new GameObject("Rooms holder");

			var polygons = layout.Rooms.Select(x => x.Shape + x.Position).ToList();
			var points = polygons.SelectMany(x => x.GetPoints()).ToList();

			var minx = points.Min(x => x.X);
			var miny = points.Min(x => x.Y);
			var maxx = points.Max(x => x.X);
			var maxy = points.Max(x => x.Y);

			var width = maxx - minx;
			var height = maxy - miny;
			var positionOffset = new IntVector2(width / 2, height / 2);

			




			// Postprocess pipeline
			var generatedRooms = new List<RoomInfo>();

			// Initialize rooms
			foreach (var layoutRoom in layout.Rooms)
			{
				var room = mapDescriptionsToRooms[(RoomDescription)layoutRoom.RoomDescription];
				var go = Instantiate(room.Tilemap);
				go.transform.SetParent(parentGameObject.transform);

				var roomInfo = new RoomInfo()
				{
					GameObject = go,
					Room = room,
					LayoutRoom = layoutRoom,
					BaseTilemap = go.GetComponentInChildren<Tilemap>()
				};

				generatedRooms.Add(roomInfo);
			}

			// Set correct position and rotate
			var roomRotation = new RoomRotation();
			foreach (var roomInfo in generatedRooms)
			{
				// Rotate
				// Rotation must precede position correction
				var rotation = roomInfo.LayoutRoom.Rotation;
				roomRotation.RotateRoom(roomInfo.GameObject, -rotation);

				// Set correct position
				var layoutRoomPosition = roomInfo.LayoutRoom.Position;
				var correctPosition = new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) - roomInfo.BaseTilemap.cellBounds.position;
				roomInfo.GameObject.transform.position = correctPosition;
			}

			// Add doors
			if (AddDoors)
			{
				foreach (var roomInfo in generatedRooms)
				{
					var layoutRoomPosition = roomInfo.LayoutRoom.Position;
					var tilemap = roomInfo.BaseTilemap;

					foreach (var door in roomInfo.LayoutRoom.Doors)
					{
						foreach (var doorPoint in door.DoorLine.GetPoints())
						{
							tilemap.SetTile(doorPoint.ToUnityIntVector3() - new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) + tilemap.cellBounds.position, DoorTile);
						}
					}
				}
			}

			// Correct walls
			if (CorrectWalls)
			{
				var wallCorrection = new WallsCorrection();
				var wallsTilemap = Walls.GetComponentInChildren<Tilemap>();

				foreach (var roomInfo in generatedRooms)
				{
					wallCorrection.CorrectWalls(roomInfo.GameObject, wallsTilemap);
				}
			}

			// Combine tilemaps
			if (CombineTilemaps)
			{
				// Prepare gameobjects and components
				var gridObject = parentGameObject;
				gridObject.transform.SetParent(parentGameObject.transform);
				gridObject.AddComponent<Grid>();
				var tilemapObject = new GameObject("Tilemap holder");
				tilemapObject.transform.SetParent(gridObject.transform);
				var commonTilemap = tilemapObject.AddComponent<Tilemap>();
				tilemapObject.AddComponent<TilemapRenderer>();

				// Map individual rooms to the tilemap
				foreach (var roomInfo in generatedRooms)
				{
					var tilemap = roomInfo.BaseTilemap;
					var layoutRoomPosition = roomInfo.LayoutRoom.Position;
					var correctPosition = new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) - roomInfo.BaseTilemap.cellBounds.position;

					foreach (var tileTuple in tilemap.GetAllTiles())
					{
						var originalTilePosition = tileTuple.Item1;
						var tilePosition = originalTilePosition + correctPosition;
						var tile = tileTuple.Item2;

						var transformMatrix = tilemap.GetTransformMatrix(originalTilePosition);

						commonTilemap.SetTile(tilePosition, tile);
						commonTilemap.SetTransformMatrix(tilePosition, transformMatrix);
					}

					DestroyImmediate(roomInfo.GameObject);
				}

				commonTilemap.ResizeBounds();
				gridObject.transform.position = -commonTilemap.cellBounds.center;

				
			}

			lastLayout = layout;

			if (ShowElapsedTime)
			{
				Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
			}
		}

		public class RoomInfo
		{
			public GameObject GameObject { get; set; }

			public IRoom<int> LayoutRoom { get; set; }

			public Room Room { get; set; }

			public Tilemap BaseTilemap { get; set; }
		}
	}
}