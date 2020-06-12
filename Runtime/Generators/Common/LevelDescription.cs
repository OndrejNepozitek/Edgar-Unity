using System;
using System.Collections.Generic;
using System.Linq;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Graphs;
using JetBrains.Annotations;
using MapGeneration.Core.MapDescriptions;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using UnityEngine;
using RoomTemplate = MapGeneration.Core.MapDescriptions.RoomTemplate;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    // TODO: where to place this file?
    public class LevelDescription
    {
        private readonly List<ConnectionBase> connections = new List<ConnectionBase>();
        private readonly List<CorridorRoomDescription> corridorRoomDescriptions = new List<CorridorRoomDescription>();

        private readonly TwoWayDictionary<RoomBase, ConnectionBase> corridorToConnectionMapping = new TwoWayDictionary<RoomBase, ConnectionBase>();
        private readonly MapDescription<RoomBase> mapDescription = new MapDescription<RoomBase>();
        private readonly TwoWayDictionary<GameObject, RoomTemplate> prefabToRoomTemplateMapping = new TwoWayDictionary<GameObject, RoomTemplate>();
        
        public void AddRoom(RoomBase room, [NotNull] List<GameObject> roomTemplates)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (roomTemplates == null) throw new ArgumentNullException(nameof(roomTemplates));
            if (roomTemplates.Count == 0) throw new ArgumentException($"There must be at least one room template for each room. Room: {room}", nameof(roomTemplates));

            mapDescription.AddRoom(room, GetBasicRoomDescription(roomTemplates));
        }

        public void AddRoom(RoomBase room, GameObject roomTemplate)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (roomTemplate == null) throw new ArgumentNullException(nameof(roomTemplate));

            AddRoom(room, new List<GameObject> {roomTemplate});
        }

        public void AddConnection(ConnectionBase connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            connections.Add(connection);
            mapDescription.AddConnection(connection.From, connection.To);
        }

        public void AddCorridorConnection(ConnectionBase connection, List<GameObject> corridorRoomTemplates, RoomBase corridorRoom)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (corridorRoom == null) throw new ArgumentNullException(nameof(corridorRoom));
            if (corridorRoomTemplates.Count == 0) throw new ArgumentException($"There must be at least one room template for each corridor room. Room: {corridorRoom}", nameof(corridorRoom));

            connections.Add(connection);
            corridorToConnectionMapping.Add(corridorRoom, connection);

            var corridorRoomDescription = GetCorridorRoomDescription(corridorRoomTemplates);
            mapDescription.AddRoom(corridorRoom, corridorRoomDescription);
            mapDescription.AddConnection(connection.From, corridorRoom);
            mapDescription.AddConnection(connection.To, corridorRoom);
        }

        private BasicRoomDescription GetBasicRoomDescription(List<GameObject> roomTemplatePrefabs)
        {
            return new BasicRoomDescription(roomTemplatePrefabs.Select(GetRoomTemplate).ToList());
        }

        private CorridorRoomDescription GetCorridorRoomDescription(List<GameObject> roomTemplatePrefabs)
        {
            foreach (var existingRoomDescription in corridorRoomDescriptions)
            {
                var existingPrefabs = existingRoomDescription
                    .RoomTemplates
                    .Select(x => prefabToRoomTemplateMapping.GetByValue(x))
                    .ToList();

                if (existingPrefabs.SequenceEqual(roomTemplatePrefabs))
                {
                    return existingRoomDescription;
                }
            }

            var corridorRoomDescription = new CorridorRoomDescription(roomTemplatePrefabs.Select(GetRoomTemplate).ToList());
            corridorRoomDescriptions.Add(corridorRoomDescription);

            return corridorRoomDescription;
        }

        private RoomTemplate GetRoomTemplate(GameObject roomTemplatePrefab)
        {
            if (prefabToRoomTemplateMapping.ContainsKey(roomTemplatePrefab))
            {
                return prefabToRoomTemplateMapping[roomTemplatePrefab];
            }

            var roomTemplate = RoomTemplatesLoader.GetRoomTemplate(roomTemplatePrefab);
            prefabToRoomTemplateMapping.Add(roomTemplatePrefab, roomTemplate);

            return roomTemplate;
        }

        // TODO: how to name this?
        public MapDescription<RoomBase> GetMapDescription()
        {
            return mapDescription;
        }

        /// <summary>
        /// Gets the graph of rooms.
        /// </summary>
        /// <remarks>
        /// The graph is not updated when new rooms are added to the level description.
        /// Adding rooms to the graph does not update the level description.
        /// This behaviour may change in the future.
        /// </remarks>
        /// <returns></returns>
        public IGraph<RoomBase> GetGraph()
        {
            return mapDescription.GetStageOneGraph();
        }

        /// <summary>
        /// Gets the graph of rooms where also corridors are considered to be rooms.
        /// </summary>
        /// <remarks>
        /// The graph is not updated when new rooms are added to the level description.
        /// Adding rooms to the graph does not update the level description.
        /// This behaviour may change in the future.
        /// </remarks>
        /// <returns></returns>
        public IGraph<RoomBase> GetGraphWithCorridors()
        {
            return mapDescription.GetGraph();
        }

        public TwoWayDictionary<GameObject, RoomTemplate> GetPrefabToRoomTemplateMapping()
        {
            return prefabToRoomTemplateMapping;
        }

        public TwoWayDictionary<RoomBase, ConnectionBase> GetCorridorToConnectionMapping()
        {
            return corridorToConnectionMapping;
        }
    }
}