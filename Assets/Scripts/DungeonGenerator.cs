namespace Assets.Scripts
{
	using System;
	using System.Collections.Generic;
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

	public class DungeonGenerator : MonoBehaviour
	{
		public Rooms Rooms;

		public Tile DoorTile;

		private IMapLayout<int> lastLayout;

		public void Generate()
		{
			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var rooms = new List<RoomDescription>();
			var polygonsToRooms = new Dictionary<RoomDescription, Room>();

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
						var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(),door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

						//if (doorLine.Length == 0)
						//{
						//	continue;
						//}

						doorLines.Add(doorLine); 
					}

					var doorMode = new SpecificPositionsMode(doorLines);

					var roomDescription = new RoomDescription(polygon, doorMode);
					rooms.Add(roomDescription);
					polygonsToRooms.Add(roomDescription, room);

					
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

			var generator = LayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();
			generator.InjectRandomGenerator(new System.Random());

			var layouts = generator.GetLayouts(mapDescription, 1);
			var layout = layouts[0];

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

			var roomRotation = new RoomRotation();

			var gridObject = new GameObject("Whole grid");
			gridObject.transform.SetParent(parentGameObject.transform);
			gridObject.AddComponent<Grid>();
			var tilemapObject = new GameObject();
			tilemapObject.transform.SetParent(gridObject.transform);
			var commonTilemap = tilemapObject.AddComponent<Tilemap>();
			tilemapObject.AddComponent<TilemapRenderer>();

			var isFirst = true;

			foreach (var generatorRoom in layout.Rooms)
			{
				var position = generatorRoom.Position;
				var room = polygonsToRooms[(RoomDescription) generatorRoom.RoomDescription];
				var go = Instantiate(room.Tilemap);
				var transform = go.transform;
				var tilemap = go.GetComponentInChildren<Tilemap>();
				var polygon = generatorRoom.RoomDescription.Shape.Rotate(generatorRoom.Rotation);

				roomRotation.RotateRoom(go, -generatorRoom.Rotation, new List<Sprite>());

				var correctPosition = new Vector3Int(position.X, position.Y, 0) - tilemap.cellBounds.position;
				
				transform.SetParent(parentGameObject.transform);
				transform.position = correctPosition;
				go.SetActive(false);

				var minX = polygon.GetPoints().Min(x => x.X);
				var minY = polygon.GetPoints().Min(x => x.Y);
				var correction = new Vector3Int(minX, minY, 0);

				foreach (var door in generatorRoom.Doors)
				{
					foreach (var doorPoint in door.DoorLine.GetPoints())
					{
						tilemap.SetTile(doorPoint.ToUnityIntVector3() - new Vector3Int(position.X, position.Y, 0) + tilemap.cellBounds.position, DoorTile);
					}
				}

				//if (isFirst)
				//{
				//	isFirst = false;
				//}
				//else
				//{
				//	go.SetActive(false);
				//}

				foreach (var tileTuple in tilemap.GetAllTiles())
				{
					var tilePosition = tileTuple.Item1;
					var tile = tileTuple.Item2;

					commonTilemap.SetTile(tilePosition + correctPosition, tile);
				}

				DestroyImmediate(go);
			}

			commonTilemap.ResizeBounds();
			gridObject.transform.position = -commonTilemap.cellBounds.center;

			var wallCorrection = GetComponent<WallsCorrection>();
			wallCorrection.GoToCorrect = gridObject;
			wallCorrection.Execute();

			lastLayout = layout;
		}
	}
}