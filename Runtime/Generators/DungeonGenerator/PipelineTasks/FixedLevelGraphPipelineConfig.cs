using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
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

        public override IEnumerator Process()
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

            yield return null;
        }

        private List<GameObject> GetRoomTemplates(List<RoomTemplatesSet> roomTemplatesSets, List<GameObject> individualRoomTemplates)
        {
            return individualRoomTemplates.Union(roomTemplatesSets.SelectMany(x => x.RoomTemplates)).ToList();
        }

        /// <summary>
        ///     Setups room shapes for a given room.
        /// </summary>
        /// <param name="room"></param>
        protected List<GameObject> GetRoomTemplates(Room room)
        {
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
    }
}