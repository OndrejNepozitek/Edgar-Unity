namespace Assets.Scripts.DungeonGenerators.GraphBasedGenerator
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using Data.Graphs;
	using GeneralAlgorithms.Algorithms.Common;
	using GeneralAlgorithms.Algorithms.Polygons;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneratorPipeline;
	using MapGeneration.Core.ConfigurationSpaces;
	using MapGeneration.Core.Doors;
	using MapGeneration.Core.Doors.DoorModes;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;
	using Payloads;
	using Pipeline;
	using RoomRotations;
	using RoomTemplates;
	using RoomTemplates.Doors;
	using RoomTemplates.Transformations;
	using TileMapping;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;
	using Debug = UnityEngine.Debug;
	using Object = UnityEngine.Object;

	[PipelineTaskFor(typeof(GraphBasedGeneratorConfig))]
	public class GraphBasedGenerator<T> : IConfigurablePipelineTask<T, GraphBasedGeneratorConfig> 
		where T : IGeneratorPayload, IRoomInfoPayload
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

			var mapDescription = SetupMapDescription();

			var stopwatch2 = new Stopwatch();
			stopwatch2.Start();

			IMapLayout<int> layout;

			if (Config.UseCorridors)
			{
				var generator = LayoutGeneratorFactory.GetChainBasedGeneratorWithCorridors<int>(new List<int>() { 2, 3 });
				generator.InjectRandomGenerator(new System.Random());

				var c = 0;
				generator.OnPerturbed += (_) => { c++; };

				var layouts = generator.GetLayouts(mapDescription, 1);
				layout = layouts[0];

				var info =
					$"{c} iterations,{stopwatch2.ElapsedMilliseconds / 1000f:F} seconds, {c / ((float)stopwatch2.ElapsedMilliseconds / 1000f):##} iters per sec";

				if (Config.ShowElapsedTime)
				{
					Debug.Log($"{c} iterations");
					Debug.Log(info);
				}

				File.AppendAllText(@"info.txt", info + Environment.NewLine);
			}
			else
			{
				var generator = LayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();

				generator.InjectRandomGenerator(new System.Random());

				var c = 0;
				generator.OnPerturbed += (_) => { c++; };

				var layouts = generator.GetLayouts(mapDescription, 1);
				layout = layouts[0];

				var info =
					$"{c} iterations,{stopwatch2.ElapsedMilliseconds / 1000f:F} seconds, {c / ((float)stopwatch2.ElapsedMilliseconds / 1000f):F} iters per sec";

				if (Config.ShowElapsedTime)
				{
					Debug.Log($"{c} iterations");
					Debug.Log(info);
				}
					
				File.AppendAllText(@"info.txt", info + Environment.NewLine);
			}

			if (Config.ShowElapsedTime)
			{
				Debug.Log($"Layout generated. {stopwatch.ElapsedMilliseconds / 1000f:F} s");
			}

			var parentGameObject = new GameObject("Helper objects");
			parentGameObject.transform.parent = Payload.GameObject.transform;

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
			var generatedRooms = new List<RoomInfo<int>>();

			// Initialize rooms
			payload.RoomInfos = new List<RoomInfo<int>>();
			foreach (var layoutRoom in layout.Rooms)
			{
				var room = roomDescriptionsToRoomTemplates[(RoomDescription)layoutRoom.RoomDescription];
				var go = Object.Instantiate(room);
				go.SetActive(false);
				go.transform.SetParent(parentGameObject.transform);

				var roomInfo = new RoomInfo<int>()
				{
					Room = go,
					RoomTemplate = room,
					LayoutRoom = layoutRoom,
				};

				generatedRooms.Add(roomInfo);
			}

			payload.RoomInfos = generatedRooms;

			// Set correct position and rotate
			var roomTransformations = new RoomTransformations();
			foreach (var roomInfo in generatedRooms)
			{
				// Rotate
				// Rotation must precede position correction
				var transformation = roomInfo.LayoutRoom.Transformation;
				roomTransformations.Transform(roomInfo.Room, transformation);

				// Set correct position
				var layoutRoomPosition = roomInfo.LayoutRoom.Position;

				roomInfo.Room.GetComponentInChildren<Tilemap>().CompressBounds();
				var correctPosition = new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) - roomInfo.Room.GetComponentInChildren<Tilemap>().cellBounds.position;
				roomInfo.Room.transform.position = correctPosition;

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
								payload.MarkerMaps[0].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.UnderDoor });
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
					wallCorrection.CorrectWalls(roomInfo.Room, wallsTilemap);
				}
			}

			// Combine tilemaps
			if (Config.CombineTilemaps)
			{
				// Map individual rooms to the tilemap
				foreach (var roomInfo in generatedRooms)
				{
					// Object.DestroyImmediate(roomInfo.Room);
				}

				payload.Tilemaps[0].ResizeBounds();
				payload.Tilemaps[0].transform.parent.position = -payload.Tilemaps[0].cellBounds.center;

				// Object.DestroyImmediate(parentGameObject);
			}
			

			if (Config.ShowElapsedTime)
			{
				Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
			}
		}

		protected void TransferRoomToMarkerMap(RoomInfo<int> roomInfo)
		{
			// TODO: should be done only once
			var wallTilesList = Config.Walls.GetComponentInChildren<Tilemap>()
				.GetAllTiles()
				.Select(x => x.Item2)
				.ToList();

			var tilemap = roomInfo.Room.GetComponentInChildren<Tilemap>();
			var layoutRoomPosition = roomInfo.LayoutRoom.Position;
			var correctPosition = new Vector3Int(layoutRoomPosition.X, layoutRoomPosition.Y, 0) - tilemap.cellBounds.position;

			var tilemaps = roomInfo.Room.GetComponentsInChildren<Tilemap>()
				.OrderBy(x => x.gameObject.GetComponent<TilemapRenderer>().sortOrder).ToList();

			for (int i = 0; i < tilemaps.Count; i++)
			{
				var sourceTilemap = tilemaps[i];
				var destinationTilemap = Payload.Tilemaps[i];
				var markerMap = Payload.MarkerMaps[i];

				foreach (var tileTuple in sourceTilemap.GetAllTiles())
				{
					var originalTilePosition = tileTuple.Item1;
					var tilePosition = originalTilePosition + correctPosition;
					var tile = tileTuple.Item2;

					if (wallTilesList.Contains(tile))
					{
						markerMap.SetMarker(tilePosition, new Marker() { Type = MarkerTypes.Wall });
					}
					else
					{
						markerMap.SetMarker(tilePosition, new Marker() { Type = MarkerTypes.Floor });
					}

					if (Config.ApplyTemplate)
					{
						destinationTilemap.SetTile(tilePosition, tile);
					}
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
			tilemap.CompressBounds();
			var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);
			var doors = roomTemplate.GetComponent<Doors>();
			var doorMode = doors.GetDoorMode();

			var roomDescription = new RoomDescription(polygon, doorMode);
			rooms.Add(roomDescription);
			roomDescriptionsToRoomTemplates.Add(roomDescription, roomTemplate);

			return roomDescription;
		}
	}
}