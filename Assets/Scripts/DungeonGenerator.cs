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
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class DungeonGenerator : MonoBehaviour
	{
		public Rooms Rooms;
		private IMapLayout<int> lastLayout;

		public void Generate()
		{
			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var rooms = new List<RoomDescription>();
			var polygonsToRooms = new Dictionary<GridPolygon, Room>();

			foreach (var roomSet in Rooms.RoomsSets)
			{
				foreach (var room in roomSet.Rooms)
				{
					var tilemap = room.Tilemap.GetComponentInChildren<Tilemap>();
					var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
					var roomDescription = new RoomDescription(polygon, new OverlapMode(1, 1));
					rooms.Add(roomDescription);
					polygonsToRooms.Add(polygon, room);
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

			mapDescription.AddRoomShapes(rooms, false);

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

			foreach (var generatorRoom in layout.Rooms)
			{
				var position = generatorRoom.Position - positionOffset;
				var room = polygonsToRooms[generatorRoom.Shape];
				var go = Instantiate(room.Tilemap);
				var transform = go.transform;
				var tilemap = go.GetComponentInChildren<Tilemap>();

				transform.SetParent(parentGameObject.transform);
				transform.position = new Vector3(position.X, position.Y) - tilemap.cellBounds.position;

			}

			lastLayout = layout;
		}
	}
}