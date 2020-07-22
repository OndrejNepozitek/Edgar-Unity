using System;
using System.Collections.Generic;
using System.Linq;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Graphs;
using MapGeneration.Core.MapDescriptions;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using UnityEngine;
using RoomTemplate = MapGeneration.Core.MapDescriptions.RoomTemplate;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    /// <summary>
    /// Class that describes the structure of a level. It contains all the rooms, connections and available room templates.
    /// </summary>
    public class LevelDescription
    {
        private readonly List<ConnectionBase> connections = new List<ConnectionBase>();
        private readonly List<CorridorRoomDescription> corridorRoomDescriptions = new List<CorridorRoomDescription>();

        private readonly TwoWayDictionary<RoomBase, ConnectionBase> corridorToConnectionMapping = new TwoWayDictionary<RoomBase, ConnectionBase>();
        private readonly MapDescription<RoomBase> mapDescription = new MapDescription<RoomBase>();
        private readonly TwoWayDictionary<GameObject, RoomTemplate> prefabToRoomTemplateMapping = new TwoWayDictionary<GameObject, RoomTemplate>();
        
        /// <summary>
        /// Adds a given room together with a list of available room templates.
        /// </summary>
        /// <param name="room">Room that is added to the level description.</param>
        /// <param name="roomTemplates">Room templates that are available for the room.</param>
        public void AddRoom(RoomBase room, List<GameObject> roomTemplates)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (roomTemplates == null) throw new ArgumentNullException(nameof(roomTemplates));
            if (roomTemplates.Count == 0) throw new ArgumentException($"There must be at least one room template for each room. Room: {room}", nameof(roomTemplates));

            mapDescription.AddRoom(room, GetBasicRoomDescription(roomTemplates));
        }

        /// <summary>
        /// Adds a given connection without a corridor between the two rooms.
        /// </summary>
        /// <param name="connection">Connection that is added to the level description</param>
        public void AddConnection(ConnectionBase connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            connections.Add(connection);
            mapDescription.AddConnection(connection.From, connection.To);
        }

        /// <summary>
        /// Adds a given connection together with a corridor room between the two rooms.
        /// </summary>
        /// <param name="connection">Connection that is added to the level description</param>
        /// <param name="corridorRoom">Room that represents the corridor room between the two rooms from the connection</param>
        /// <param name="corridorRoomTemplates">Room templates that are available for the corridor</param>
        public void AddCorridorConnection(ConnectionBase connection,RoomBase corridorRoom, List<GameObject> corridorRoomTemplates)
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

        /// <summary>
        /// Gets the map description.
        /// </summary>
        /// <returns></returns>
        internal MapDescription<RoomBase> GetMapDescription()
        {
            return mapDescription;
        }

        
        /// <summary>
        /// Gets the mapping from room template game objects to room template instances.
        /// </summary>
        /// <returns></returns>
        internal TwoWayDictionary<GameObject, RoomTemplate> GetPrefabToRoomTemplateMapping()
        {
            return prefabToRoomTemplateMapping;
        }

        /// <summary>
        /// Gets the mapping from corridor rooms to corresponding connections.
        /// </summary>
        /// <returns></returns>
        internal TwoWayDictionary<RoomBase, ConnectionBase> GetCorridorToConnectionMapping()
        {
            return corridorToConnectionMapping;
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
    }
}