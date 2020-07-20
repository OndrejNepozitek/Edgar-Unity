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
    /// <summary>
    /// Creates an input for the generator from a given level graph.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class FixedLevelGraphInputTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload
    {
        private readonly FixedLevelGraphConfig config;

        public FixedLevelGraphInputTask(FixedLevelGraphConfig config)
        {
            this.config = config;
        }

        public override IEnumerator Process()
        {
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
                    var corridorRoom = (RoomBase) ScriptableObject.CreateInstance(typeOfRooms);

                    if (corridorRoom is Room basicRoom)
                    {
                        basicRoom.Name = "Corridor";
                    }
                    
                    levelDescription.AddCorridorConnection(connection, corridorRoom,
                        GetRoomTemplates(config.LevelGraph.CorridorRoomTemplateSets, config.LevelGraph.CorridorIndividualRoomTemplates));
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
        protected List<GameObject> GetRoomTemplates(RoomBase room)
        {
            var roomTemplates = room.GetRoomTemplates();

            if (roomTemplates == null || roomTemplates.Count == 0)
            {
                return GetRoomTemplates(config.LevelGraph.DefaultRoomTemplateSets, config.LevelGraph.DefaultIndividualRoomTemplates);
            }

            return roomTemplates;
        }
    }
}