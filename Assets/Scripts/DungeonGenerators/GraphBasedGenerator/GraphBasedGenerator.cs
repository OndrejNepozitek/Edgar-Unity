namespace Assets.Scripts.DungeonGenerators.GraphBasedGenerator
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using Data.Graphs;
	using Data.Rooms;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneratorPipeline;
	using MapGeneration.Core.Doors.DoorModes;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;
	using Pipeline;
	using RoomRotations;
	using RoomTemplates.Doors;
	using TileMapping;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;
	using Debug = UnityEngine.Debug;
	using Object = UnityEngine.Object;

	[PipelineTaskFor(typeof(GraphBasedGeneratorConfig))]
	public class GraphBasedGenerator<T> : IConfigurablePipelineTask<T, GraphBasedGeneratorConfig> where T : IGeneratorPayload
	{
		public GraphBasedGeneratorConfig Config { get; set; }

		protected T Payload;

		private List<RoomDescription> rooms;
		private List<RoomDescription> corridors;
		private TwoWayDictionary<RoomDescription, GameObject> roomDescriptionsToRoomTemplates;
		private Dictionary<Room, int> roomToNumber;

		public void Process(T payload)
		{
			Payload = payload;

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (Config.ShowElapsedTime)
			{
				Debug.Log("--- Script started ---"); 
			}

			//foreach (var roomSet in Config.RoomTemplatesWrapper.RoomsSets)
			//{
			//	foreach (var room in roomSet.Rooms)
			//	{
			//		var tilemap = room.Tilemap.GetComponentInChildren<Tilemap>();
			//		var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
			//		var doors = room.Tilemap.GetComponent<Doors>();
			//		var doorLines = new List<OrthogonalLine>();

			//		foreach (var door in doors.DoorsList)
			//		{
			//			var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(), door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

			//			//if (doorLine.Length == 0)
			//			//{
			//			//	continue;
			//			//}

			//			doorLines.Add(doorLine);
			//		}

			//		var doorMode = new SpecificPositionsMode(doorLines);

			//		var roomDescription = new RoomDescription(polygon, doorMode);
			//		rooms.Add(roomDescription);
			//		roomDescriptionsToRoomTemplates.Add(roomDescription, room);
			//	}
			//}

			//if (Config.UseCorridors)
			//{
			//	foreach (var roomSet in Config.CorridorTemplatesWrapper.RoomsSets)
			//	{
			//		foreach (var room in roomSet.Rooms)
			//		{
			//			var tilemap = room.Tilemap.GetComponentInChildren<Tilemap>();
			//			var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
			//			var doors = room.Tilemap.GetComponent<Doors>();
			//			var doorLines = new List<OrthogonalLine>();
						
			//			foreach (var door in doors.DoorsList)
			//			{
			//				var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(), door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

			//				//if (doorLine.Length == 0)
			//				//{
			//				//	continue;
			//				//}

			//				doorLines.Add(doorLine);
			//			}

			//			var doorMode = new SpecificPositionsMode(doorLines);

			//			var roomDescription = new RoomDescription(polygon, doorMode);
			//			corridors.Add(roomDescription);
			//			roomDescriptionsToRoomTemplates.Add(roomDescription, room);
			//		}
			//	}
			//}

			//var mapDescription = new MapDescription<int>();
			//var verticesCount = 7;

			//for (var i = 0; i < verticesCount; i++)
			//{
			//	mapDescription.AddRoom(i);
			//}

			//for (var i = 0; i < verticesCount - 1; i++)
			//{
			//	mapDescription.AddPassage(i, i + 1);
			//}

			//mapDescription.AddPassage(3, 0);
			//mapDescription.AddPassage(3, 6);

			//mapDescription.AddRoomShapes(rooms);

			//if (ShowElapsedTime)
			//{
			//	Debug.Log($"Map description created. {stopwatch.ElapsedMilliseconds / 1000f:F} s");
			//}

			//if (Config.UseCorridors)
			//{
			//	mapDescription.SetWithCorridors(true, new List<int>() { 2, 3 });

			//	mapDescription.AddCorridorShapes(corridors);
			//}

			//foreach (var room in Config.LayoutGraph.Rooms)
			//{
			//	mapDescription.AddRoom(roomCounter);
			//	roomToNumber.Add(room, roomCounter++);
			//}

			//foreach (var connection in Config.LayoutGraph.Connections)
			//{
			//	mapDescription.AddPassage(roomToNumber[connection.From], roomToNumber[connection.To]);
			//}

			//mapDescription.AddRoomShapes(rooms);

			var mapDescription = SetupMapDescription(); 

			IMapLayout<int> layout;

			if (Config.UseCorridors)
			{
				var generator = LayoutGeneratorFactory.GetChainBasedGeneratorWithCorridors<int>(new List<int>() { 2, 3 });
				generator.InjectRandomGenerator(new System.Random());

				var layouts = generator.GetLayouts(mapDescription, 1);
				layout = layouts[0];
			}
			else
			{
				var generator = LayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();

				generator.InjectRandomGenerator(new System.Random());

				var layouts = generator.GetLayouts(mapDescription, 1);
				layout = layouts[0];
			}

			if (Config.ShowElapsedTime)
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
			var generatedRooms = new List<DungeonGenerator.RoomInfo<int>>();

			// Initialize rooms
			foreach (var layoutRoom in layout.Rooms)
			{
				var room = roomDescriptionsToRoomTemplates[(RoomDescription)layoutRoom.RoomDescription];
				var go = Object.Instantiate(room);
				go.transform.SetParent(parentGameObject.transform);

				var roomInfo = new DungeonGenerator.RoomInfo<int>()
				{
					GameObject = go,
					RoomTemplate = room,
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

				TransferRoomToMarkerMap(roomInfo);
			}

			// Add doors
			{
				foreach (var roomInfo in generatedRooms)
				{
					foreach (var door in roomInfo.LayoutRoom.Doors)
					{
						foreach (var doorPoint in door.DoorLine.GetPoints())
						{
							var correctPosition = doorPoint.ToUnityIntVector3();
							payload.MarkerMaps[0].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.Floor });

							if (Config.AddDoors)
							{
								payload.MarkerMaps[1].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.Door });
							}
						}
					}
				}
			}


			// Correct walls
			if (Config.CorrectWalls)
			{
				var wallCorrection = new WallsCorrection();
				var wallsTilemap = Config.Walls.GetComponentInChildren<Tilemap>();

				foreach (var roomInfo in generatedRooms)
				{
					wallCorrection.CorrectWalls(roomInfo.GameObject, wallsTilemap);
				}
			}

			// Combine tilemaps
			if (Config.CombineTilemaps)
			{
				// Map individual rooms to the tilemap
				foreach (var roomInfo in generatedRooms)
				{
					Object.DestroyImmediate(roomInfo.GameObject);
				}

				payload.Tilemaps[0].ResizeBounds();
				payload.Tilemaps[0].transform.parent.position = -payload.Tilemaps[0].cellBounds.center;

				Object.DestroyImmediate(parentGameObject);
			}

			

			if (Config.ShowElapsedTime)
			{
				Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
			}
		}

		protected void TransferRoomToMarkerMap(DungeonGenerator.RoomInfo<int> roomInfo)
		{
			// TODO: should be done only once
			var wallTilesList = Config.Walls.GetComponentInChildren<Tilemap>()
				.GetAllTiles()
				.Select(x => x.Item2)
				.ToList();

			var tilemap = roomInfo.BaseTilemap;
			var layoutRoomPosition = roomInfo.LayoutRoom.Position;
			var correctPosition = new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) - roomInfo.BaseTilemap.cellBounds.position;

			foreach (var tileTuple in tilemap.GetAllTiles())
			{
				var originalTilePosition = tileTuple.Item1;
				var tilePosition = originalTilePosition + correctPosition;
				var tile = tileTuple.Item2;

				if (wallTilesList.Contains(tile))
				{
					Payload.MarkerMaps[0].SetMarker(tilePosition, new Marker() { Type = MarkerTypes.Wall });
				}
				else
				{
					Payload.MarkerMaps[0].SetMarker(tilePosition, new Marker() { Type = MarkerTypes.Floor });
				}
			}
		}

		protected MapDescription<int> SetupMapDescription()
		{
			rooms = new List<RoomDescription>();
			corridors = new List<RoomDescription>();
			roomDescriptionsToRoomTemplates = new TwoWayDictionary<RoomDescription, GameObject>();
			roomToNumber = new Dictionary<Room, int>();

			var layoutGraph = Config.LayoutGraph;
			var mapDescription = new MapDescription<int>();
			var roomCounter = -1;
			
			// Setup individual rooms
			foreach (var room in layoutGraph.Rooms)
			{
				roomCounter++;
				mapDescription.AddRoom(roomCounter);
				roomToNumber.Add(room, roomCounter);

				var roomTemplatesSets = room.RoomTemplateSets;
				var individualRoomTemplates = room.IndividualRoomTemplates;

				if (room.RoomsGroupGuid != Guid.Empty)
				{
					roomTemplatesSets = layoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).RoomTemplateSets;
					individualRoomTemplates = layoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).IndividualRoomTemplates;
				}

				foreach (var roomTemplatesSet in roomTemplatesSets)
				{
					foreach (var roomTemplate in roomTemplatesSet.Rooms)
					{
						var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
						mapDescription.AddRoomShapes(roomCounter, roomDescription);
					}
				}

				foreach (var roomTemplate in individualRoomTemplates)
				{
					var roomDescription = GetRoomDescription(roomTemplate);
					mapDescription.AddRoomShapes(roomCounter, roomDescription);
				}
			}

			// Add default room shapes
			foreach (var roomTemplatesSet in layoutGraph.DefaultRoomTemplateSets)
			{
				foreach (var roomTemplate in roomTemplatesSet.Rooms)
				{
					var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
					mapDescription.AddRoomShapes(roomDescription);
				}
			}

			foreach (var roomTemplate in layoutGraph.DefaultIndividualRoomTemplates)
			{
				var roomDescription = GetRoomDescription(roomTemplate);
				mapDescription.AddRoomShapes(roomDescription);
			}

			// Add corridors
			if (Config.UseCorridors)
			{
				mapDescription.SetWithCorridors(true, new List<int>() { 2, 3 });

				foreach (var roomTemplatesSet in layoutGraph.CorridorRoomTemplateSets)
				{
					foreach (var roomTemplate in roomTemplatesSet.Rooms)
					{
						var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
						mapDescription.AddCorridorShapes(roomDescription);
					}
				}

				foreach (var roomTemplate in layoutGraph.CorridorIndividualRoomTemplate)
				{
					var roomDescription = GetRoomDescription(roomTemplate);
					mapDescription.AddCorridorShapes(roomDescription);
				}
			}

			// Add passages
			foreach (var connection in Config.LayoutGraph.Connections)
			{
				mapDescription.AddPassage(roomToNumber[connection.From], roomToNumber[connection.To]);
			}

			return mapDescription;
		}

		protected RoomDescription GetRoomDescription(GameObject roomTemplate)
		{
			if (roomDescriptionsToRoomTemplates.ContainsValue(roomTemplate))
			{
				return roomDescriptionsToRoomTemplates.GetByValue(roomTemplate);
			}

			var tilemap = roomTemplate.GetComponentInChildren<Tilemap>();
			var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
			var doors = roomTemplate.GetComponent<Doors>();
			var doorLines = new List<OrthogonalLine>();

			foreach (var door in doors.DoorsList)
			{
				var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(), door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

				doorLines.Add(doorLine);
			}

			var doorMode = new SpecificPositionsMode(doorLines);

			var roomDescription = new RoomDescription(polygon, doorMode);
			rooms.Add(roomDescription);
			roomDescriptionsToRoomTemplates.Add(roomDescription, roomTemplate);

			return roomDescription;
		}
	}
}