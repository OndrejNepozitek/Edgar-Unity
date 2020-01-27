using Assets.ProceduralLevelGenerator.Scripts.Data.Rooms;
using Assets.ProceduralLevelGenerator.Scripts.Utils;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Data.Graphs;
    using Payloads.Interfaces;
	using Pipeline;
	using UnityEngine;

	/// <summary>
	/// Pipeline task that prepares level description from a given layout graph.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline/Fixed input", fileName = "FixedInput")]
	public class FixedInputConfig : PipelineConfig
	{ 
		public LevelGraph LevelGraph;

		public bool UseCorridors;
	}

	public class FixedInputTask<TPayload> : ConfigurablePipelineTask<TPayload, FixedInputConfig>
		where TPayload : class, IGraphBasedGeneratorPayload
	{
        public override void Process()
        {
			if (Config.LevelGraph == null)
			{
				throw new ArgumentException("LevelGraph must not be null.");
			}

            if (Config.LevelGraph.Rooms.Count == 0)
            {
                throw new ArgumentException("LevelGraph must contain at least one room.");
            }

			var levelDescription = new LevelDescription();
			
			// Setup individual rooms
			foreach (var room in Config.LevelGraph.Rooms)
			{
				levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            var typeOfRooms = Config.LevelGraph.Rooms.First().GetType();

			// Add passages
            foreach (var connection in Config.LevelGraph.Connections)
			{
                if (Config.UseCorridors)
                {
                    var corridorRoom = (Room) ScriptableObject.CreateInstance(typeOfRooms);
                    corridorRoom.Name = $"Corridor";
                    
                    levelDescription.AddCorridorConnection(connection, GetRoomTemplates(Config.LevelGraph.CorridorRoomTemplateSets, Config.LevelGraph.CorridorIndividualRoomTemplates), corridorRoom);
                }
                else
                {
					levelDescription.AddConnection(connection);
                }
            }

            Payload.LevelDescription = levelDescription;
        }

        private List<GameObject> GetRoomTemplates(List<RoomTemplatesSet> roomTemplatesSets, List<GameObject> individualRoomTemplates)
        {
            return individualRoomTemplates.ToList();
        }

		/// <summary>
		/// Setups room shapes for a given room.
		/// </summary>
		/// <param name="room"></param>
        protected List<GameObject> GetRoomTemplates(Room room)
		{
            // If the room is assigned to a Rooms group, use the room descriptions assigned to that group
            if (room.RoomsGroupGuid != Guid.Empty)
            {
                return GetRoomsGroupRoomTemplates(room.RoomsGroupGuid);
            }

			// Get assigned room templates
			var roomTemplatesSets = room.RoomTemplateSets;
			var individualRoomTemplates = room.IndividualRoomTemplates;

			var roomTemplates = GetRoomTemplates(roomTemplatesSets, individualRoomTemplates).Distinct().ToList();

            if (roomTemplates.Count == 0)
            {
                return GetRoomTemplates(Config.LevelGraph.DefaultRoomTemplateSets, Config.LevelGraph.DefaultIndividualRoomTemplates);
            }

            return roomTemplates;
        }

        protected List<GameObject> GetRoomsGroupRoomTemplates(Guid roomsGroupGuid)
        {
            if (roomsGroupGuid == Guid.Empty)
            {
				throw new ArgumentException();
            }

            var roomTemplatesSets = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).RoomTemplateSets;
            var individualRoomTemplates = Config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).IndividualRoomTemplates;

            return GetRoomTemplates(roomTemplatesSets, individualRoomTemplates);
        }
    }
}