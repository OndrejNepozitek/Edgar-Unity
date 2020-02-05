using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.Data.Rooms;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class FixedLevelGraphPipelineConfig : PipelineConfig
    {
        public FixedLevelGraphConfig Config;
    }

    public class FixedInputPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, FixedLevelGraphPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload
    {
        private FixedLevelGraphConfig config;

        public override void Process()
        {
            // TODO: kind of weird
            config = Config.Config;

            if (config.LevelGraph == null)
            {
                throw new ArgumentException("LevelGraph must not be null.");
            }

            if (config.LevelGraph.Rooms.Count == 0)
            {
                throw new ArgumentException("LevelGraph must contain at least one room.");
            }

            var levelDescription = new LevelDescription();

            // Setup individual rooms
            foreach (var room in config.LevelGraph.Rooms)
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            var typeOfRooms = config.LevelGraph.Rooms.First().GetType();

            // Add passages
            foreach (var connection in config.LevelGraph.Connections)
            {
                if (config.UseCorridors)
                {
                    var corridorRoom = (Room) ScriptableObject.CreateInstance(typeOfRooms);
                    corridorRoom.Name = "Corridor";

                    levelDescription.AddCorridorConnection(connection,
                        GetRoomTemplates(config.LevelGraph.CorridorRoomTemplateSets, config.LevelGraph.CorridorIndividualRoomTemplates), corridorRoom);
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
        ///     Setups room shapes for a given room.
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
                return GetRoomTemplates(config.LevelGraph.DefaultRoomTemplateSets, config.LevelGraph.DefaultIndividualRoomTemplates);
            }

            return roomTemplates;
        }

        protected List<GameObject> GetRoomsGroupRoomTemplates(Guid roomsGroupGuid)
        {
            if (roomsGroupGuid == Guid.Empty)
            {
                throw new ArgumentException();
            }

            var roomTemplatesSets = config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).RoomTemplateSets;
            var individualRoomTemplates = config.LevelGraph.RoomsGroups.Single(x => x.Guid == roomsGroupGuid).IndividualRoomTemplates;

            return GetRoomTemplates(roomTemplatesSets, individualRoomTemplates);
        }
    }
}