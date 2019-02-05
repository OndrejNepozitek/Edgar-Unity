namespace Assets.Scripts.GeneratorPipeline.DungeonGenerators.GraphBasedGenerator
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneratorPipeline;
	using InputSetup;
	using MapGeneration.Core.Doors;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutGenerator;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;
	using Markers;
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
		where TPayload : class, IGeneratorPayload, IRoomInfoPayload, IGraphBasedInputPayload
	{
		private List<RoomInfo<Room>> generatedRooms;

		public override void Process()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (Config.ShowElapsedTime)
			{
				Debug.Log("--- Script started ---"); 
			}

			// Setup map description
			var mapDescription = Payload.MapDescription;

			// Setup layout generator
			IBenchmarkableLayoutGenerator<MapDescription<Room>, IMapLayout<Room>> generator;
			if (mapDescription.IsWithCorridors)
			{
				var gen = UnityLayoutGeneratorFactory.GetChainBasedGeneratorWithCorridors<Room>(mapDescription.CorridorsOffsets, corridorNodesCreator: new CorridorsNodeCreator(mapDescription));
				generator = gen;
			}
			else
			{
				var gen = UnityLayoutGeneratorFactory.GetDefaultChainBasedGenerator<Room>();
				generator = gen;
			}

			// Run generator
			IMapLayout<Room> layout = null;
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
			generatedRooms = new List<RoomInfo<Room>>();
			foreach (var layoutRoom in layout.Rooms)
			{
				var roomTemplate = Payload.RoomDescriptionsToRoomTemplates[layoutRoom.RoomDescription];
				var go = Object.Instantiate(roomTemplate);
				go.SetActive(false);
				go.transform.SetParent(parentGameObject.transform);

				var roomInfo = new RoomInfo<Room>()
				{
					Room = go,
					RoomTemplate = roomTemplate,
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

		protected void TransferRoomToMarkerMap(RoomInfo<Room> roomInfo)
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
	}
}