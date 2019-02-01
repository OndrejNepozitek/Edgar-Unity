namespace Assets.Scripts.DungeonGenerators.GraphBasedGenerator
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneratorPipeline;
	using MapGeneration.Core.Doors;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutGenerator;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;
	using Payloads;
	using Pipeline;
	using RoomTemplates;
	using RoomTemplates.Doors;
	using RoomTemplates.Transformations;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;
	using Debug = UnityEngine.Debug;
	using Object = UnityEngine.Object;

	public class GraphBasedGeneratorTask<TPayload> : ConfigurablePipelineTask<TPayload, GraphBasedGeneratorConfig>
		where TPayload : class, IGeneratorPayload, IRoomInfoPayload
	{
		private List<RoomDescription> rooms;
		private List<RoomDescription> corridors;
		private TwoWayDictionary<RoomDescription, GameObject> roomDescriptionsToRoomTemplates;
		private Dictionary<Room, int> roomToNumber;
		private List<RoomInfo<int>> generatedRooms;

		public override void Process()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (Config.ShowElapsedTime)
			{
				Debug.Log("--- Script started ---"); 
			}

			// Setup map description
			var mapDescription = SetupMapDescription();

			// Setup layout generator
			IBenchmarkableLayoutGenerator<MapDescription<int>, IMapLayout<int>> generator;
			if (Config.UseCorridors)
			{
				var gen = LayoutGeneratorFactory.GetChainBasedGeneratorWithCorridors<int>(mapDescription.CorridorsOffsets);
				generator = gen;
			}
			else
			{
				var gen = LayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();
				generator = gen;
			}

			// Run generator
			IMapLayout<int> layout = null;
			var task = Task.Run(() => layout = generator.GetLayouts(mapDescription, 1)[0]);
			var taskCompleted = task.Wait(10000);

			if (!taskCompleted)
			{
				throw new DungeonGeneratorException("Timeout was reached when generating the layout");
			}

			if (Config.ShowElapsedTime)
			{
				Debug.Log($"Layout generated in {generator.TimeFirst / 1000f:F} seconds");
				Debug.Log($"{generator.IterationsCount} iterations needed, {(generator.IterationsCount / (generator.TimeFirst / 1000d)):0} iterations per second");
			}

			var parentGameObject = new GameObject("Helper objects");
			parentGameObject.transform.parent = Payload.GameObject.transform;

			// Initialize rooms
			generatedRooms = new List<RoomInfo<int>>();
			Payload.RoomInfos = new List<RoomInfo<int>>();
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

			Payload.RoomInfos = generatedRooms;

			TransformRooms();
			AddDoorMarkers();

			// Center grid
			if (Config.CenterGrid)
			{
				Payload.Tilemaps[0].CompressBounds();
				Payload.Tilemaps[0].transform.parent.position = -Payload.Tilemaps[0].cellBounds.center;
			}
			
			if (Config.ShowElapsedTime)
			{
				Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
			}
		}

		protected void AddDoorMarkers()
		{
			foreach (var roomInfo in generatedRooms)
			{
				foreach (var door in roomInfo.LayoutRoom.Doors)
				{
					foreach (var doorPoint in door.DoorLine.GetPoints())
					{
						var correctPosition = doorPoint.ToUnityIntVector3();
						Payload.MarkerMaps[0].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.Floor });

						if (Config.AddDoorMarkers)
						{
							Payload.MarkerMaps[0].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.UnderDoor });
							Payload.MarkerMaps[1].SetMarker(correctPosition, new Marker() { Type = MarkerTypes.Door });
						}
					}
				}
			}
		}

		// Set correct position and rotate
		protected void TransformRooms()
		{
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
			mapDescription.SetDefaultTransformations(new List<Transformation>() {Transformation.Identity});
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
				if (roomTemplatesSet == null)
					continue;

				foreach (var roomTemplate in roomTemplatesSet.Rooms)
				{
					if (roomTemplate == null)
						continue;

					var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
					mapDescription.AddRoomShapes(roomDescription);
				}
			}

			foreach (var roomTemplate in layoutGraph.DefaultIndividualRoomTemplates)
			{
				if (roomTemplate == null)
					continue;

				var roomDescription = GetRoomDescription(roomTemplate);
				mapDescription.AddRoomShapes(roomDescription);
			}

			// Add corridors
			if (Config.UseCorridors)
			{
				var corridorLengths = new List<int>();

				foreach (var roomTemplatesSet in layoutGraph.CorridorRoomTemplateSets)
				{
					if (roomTemplatesSet == null)
						continue;

					foreach (var roomTemplate in roomTemplatesSet.Rooms)
					{
						if (roomTemplate == null)
							continue;

						var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
						mapDescription.AddCorridorShapes(roomDescription);
						var corridorLength = GetCorridorLength(roomDescription);
						corridorLengths.Add(corridorLength);
					}
				}

				foreach (var roomTemplate in layoutGraph.CorridorIndividualRoomTemplate)
				{
					if (roomTemplate == null)
						continue;

					var roomDescription = GetRoomDescription(roomTemplate);
					mapDescription.AddCorridorShapes(roomDescription);
					var corridorLength = GetCorridorLength(roomDescription);
					corridorLengths.Add(corridorLength);
				}

				mapDescription.SetWithCorridors(true, corridorLengths.Distinct().ToList());
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

			if (doors == null)
			{
				Debug.LogError($"Room template \"{roomTemplate.name}\" does not have any doors assigned.", roomTemplate);
				throw new DungeonGeneratorException();
			}

			var doorMode = doors.GetDoorMode();

			var roomDescription = new RoomDescription(polygon, doorMode);
			rooms.Add(roomDescription);
			roomDescriptionsToRoomTemplates.Add(roomDescription, roomTemplate);

			return roomDescription;
		}

		protected int GetCorridorLength(RoomDescription roomDescription)
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
	}
}