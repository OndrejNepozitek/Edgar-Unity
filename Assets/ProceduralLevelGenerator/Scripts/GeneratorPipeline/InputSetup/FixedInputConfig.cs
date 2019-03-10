namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using Payloads.Interfaces;
	using Pipeline;
	using UnityEngine;

	/// <summary>
	/// Pipeline task that prepares map description from a given layout graph.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline/Fixed input", fileName = "FixedInput")]
	public class FixedInputConfig : PipelineConfig
	{ 
		public LevelGraph LevelGraph;

		public bool UseCorridors;
	}

	public class FixedInputTask<TPayload> : InputSetupBaseTask<TPayload, FixedInputConfig>
		where TPayload : class, IGraphBasedGeneratorPayload, IGeneratorPayload
	{
		protected TwoWayDictionary<Room, int> RoomToIntMapping;

		protected override MapDescription<int> SetupMapDescription()
		{
			if (Config.LevelGraph == null)
			{
				throw new ArgumentException("LevelGraph must not be null.");
			}

			RoomToIntMapping = new TwoWayDictionary<Room, int>();
			var mapDescription = new MapDescription<int>();
			mapDescription.SetDefaultTransformations(new List<Transformation>() { Transformation.Identity });

			// Setup individual rooms
			foreach (var room in Config.LevelGraph.Rooms)
			{
				RoomToIntMapping.Add(room, RoomToIntMapping.Count);
				mapDescription.AddRoom(RoomToIntMapping[room]);
				SetupRoomShapesForRoom(mapDescription, room);
			}

			// Add default room shapes
			SetupDefaultRoomShapes(mapDescription, Config.LevelGraph);

			// Add corridors
			if (Config.UseCorridors)
			{
				SetupCorridorRoomShapes(mapDescription, Config.LevelGraph);
			}

			// Add passages
			foreach (var connection in Config.LevelGraph.Connections)
			{
				var from = RoomToIntMapping[connection.From];
				var to = RoomToIntMapping[connection.To];

				mapDescription.AddPassage(from, to);
			}

			if (Payload is IRoomToIntMappingPayload<Room> payloadWithMapping)
			{
				payloadWithMapping.RoomToIntMapping = RoomToIntMapping;
			}

			return mapDescription;
		}

		/// <summary>
		/// Setups room shapes for a given room.
		/// </summary>
		/// <param name="room"></param>
		/// <param name="mapDescription"></param>
		protected void SetupRoomShapesForRoom(MapDescription<int> mapDescription, Room room)
		{
			// Get assigned room templates
			var roomTemplatesSets = room.RoomTemplateSets;
			var individualRoomTemplates = room.IndividualRoomTemplates;

			// If the room is assigned to a Rooms group, use room templates for the group instead
			if (room.RoomsGroupGuid != Guid.Empty)
			{
				roomTemplatesSets = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).RoomTemplateSets;
				individualRoomTemplates = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).IndividualRoomTemplates;
			}

			var roomDescriptions = GetRoomDescriptions(roomTemplatesSets, individualRoomTemplates).Distinct();

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddRoomShapes(RoomToIntMapping[room], roomDescription);
			}
		}

		/// <summary>
		/// Setups default room shapes.
		/// These are used if a room does not have any room shapes assigned.
		/// </summary>
		/// <param name="mapDescription"></param>
		/// <param name="levelGraph"></param>
		protected void SetupDefaultRoomShapes(MapDescription<int> mapDescription, LevelGraph levelGraph)
		{
			var roomDescriptions = GetRoomDescriptions(levelGraph.DefaultRoomTemplateSets, levelGraph.DefaultIndividualRoomTemplates).Distinct();

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddRoomShapes(roomDescription);
			}
		}

		/// <summary>
		/// Setups corridor room shapes.
		/// </summary>
		/// <param name="mapDescription"></param> 
		/// <param name="levelGraph"></param>
		protected void SetupCorridorRoomShapes(MapDescription<int> mapDescription, LevelGraph levelGraph)
		{
			var corridorLengths = new List<int>();
			var roomDescriptions = GetRoomDescriptions(levelGraph.CorridorRoomTemplateSets, levelGraph.CorridorIndividualRoomTemplates).Distinct().ToList();

			if (roomDescriptions.Count == 0)
			{
				throw new ArgumentException("There must be at least 1 corridor room template if corridors are enabled.");
			}

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddCorridorShapes(roomDescription);

				var corridorLength = RoomShapesLoader.GetCorridorLength(roomDescription);
				corridorLengths.Add(corridorLength);
			}

			mapDescription.SetWithCorridors(true, corridorLengths.Distinct().ToList());
		}
	}
}