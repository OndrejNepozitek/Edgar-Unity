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
        protected BasicRoomDescription DefaultRoomDescription;
        protected CorridorRoomDescription CorridorRoomDescription;
        protected Dictionary<Guid, BasicRoomDescription> RoomsGroupRoomDescriptions = new Dictionary<Guid, BasicRoomDescription>();

		protected override MapDescription<int> SetupMapDescription()
		{
			if (Config.LevelGraph == null)
			{
				throw new ArgumentException("LevelGraph must not be null.");
			}

			RoomToIntMapping = new TwoWayDictionary<Room, int>();
			var mapDescription = new MapDescription<int>();

            // Setup default room description
			var defaultRoomTemplates = GetRoomTemplates(Config.LevelGraph.DefaultRoomTemplateSets, Config.LevelGraph.DefaultIndividualRoomTemplates).Distinct().ToList();
			DefaultRoomDescription = new BasicRoomDescription(defaultRoomTemplates);

			// Setup individual rooms
			foreach (var room in Config.LevelGraph.Rooms)
			{
				RoomToIntMapping.Add(room, RoomToIntMapping.Count);
				mapDescription.AddRoom(RoomToIntMapping[room], GetRoomDescription(room));
            }

            // Add corridors
			if (Config.UseCorridors)
            {
                CorridorRoomDescription = GetCorridorRoomDescription();
            }

			// Add passages
            var corridorCounter = 0;
			foreach (var connection in Config.LevelGraph.Connections)
			{
				var from = RoomToIntMapping[connection.From];
				var to = RoomToIntMapping[connection.To];

                if (Config.UseCorridors)
                {
                    var corridorRoom = ScriptableObject.CreateInstance<Room>();
                    corridorRoom.Name = $"Corridor {corridorCounter++}";
                    
                    var corridorRoomNumber = RoomToIntMapping.Count;
                    RoomToIntMapping[corridorRoom] = corridorRoomNumber;

					mapDescription.AddRoom(corridorRoomNumber, CorridorRoomDescription);
                    mapDescription.AddConnection(from, corridorRoomNumber);
                    mapDescription.AddConnection(to, corridorRoomNumber);
                }
                else
                {
					mapDescription.AddConnection(from, to);
                }
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
        protected BasicRoomDescription GetRoomDescription(Room room)
		{
            // If the room is assigned to a Rooms group, use the room descriptions assigned to that group
            if (room.RoomsGroupGuid != Guid.Empty)
            {
                return GetRoomsGroupRoomDescription(room.RoomsGroupGuid);
            }

			// Get assigned room templates
			var roomTemplatesSets = room.RoomTemplateSets;
			var individualRoomTemplates = room.IndividualRoomTemplates;

			var roomTemplates = GetRoomTemplates(roomTemplatesSets, individualRoomTemplates).Distinct().ToList();

            if (roomTemplates.Count == 0)
            {
                return DefaultRoomDescription;
            }

            var roomDescription = new BasicRoomDescription(roomTemplates);

            return roomDescription;
        }

        protected BasicRoomDescription GetRoomsGroupRoomDescription(Guid roomsGroupGuid)
        {
            if (roomsGroupGuid == Guid.Empty)
            {
				throw new ArgumentException();
            }

            if (RoomsGroupRoomDescriptions.TryGetValue(roomsGroupGuid, out var roomsGroupRoomDescription))
            {
                return roomsGroupRoomDescription;
            }

            var roomTemplatesSets = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).RoomTemplateSets;
            var individualRoomTemplates = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).IndividualRoomTemplates;

            var roomTemplates = GetRoomTemplates(roomTemplatesSets, individualRoomTemplates);
			var roomDescription = new BasicRoomDescription(roomTemplates);

            RoomsGroupRoomDescriptions[roomsGroupGuid] = roomDescription;

            return roomDescription;
        }

        /// <summary>
		/// Setups corridor room shapes.
		/// </summary>
        protected CorridorRoomDescription GetCorridorRoomDescription()
		{
            var roomTemplates = GetRoomTemplates(Config.LevelGraph.CorridorRoomTemplateSets, Config.LevelGraph.CorridorIndividualRoomTemplates).Distinct().ToList();

			if (roomTemplates.Count == 0)
			{
				throw new ArgumentException("There must be at least 1 corridor room template if corridors are enabled.");
			}

            return new CorridorRoomDescription(roomTemplates);
		}
	}
}