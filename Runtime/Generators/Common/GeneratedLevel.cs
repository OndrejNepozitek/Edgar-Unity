using System.Collections.Generic;
using System.Linq;
using MapGeneration.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    /// <summary>
    /// Holds information about the generated level.
    /// Currently cannot be serialized.
    /// </summary>
    public class GeneratedLevel
    {
        /// <summary>
        /// GameObject that hold the generated level.
        /// </summary>
        public GameObject RootGameObject { get; }

        private readonly MapLayout<RoomBase> mapLayout;
        private readonly Dictionary<RoomBase, RoomInstance> roomInstances;

        public GeneratedLevel(Dictionary<RoomBase, RoomInstance> roomInstances, MapLayout<RoomBase> mapLayout, GameObject rootGameObject)
        {
            this.roomInstances = roomInstances;
            this.mapLayout = mapLayout;
            RootGameObject = rootGameObject;
        }

        /// <summary>
        /// Gets information about all the rooms that are present in the generated level.
        /// </summary>
        /// <returns></returns>
        public List<RoomInstance> GetRoomInstances()
        {
            return roomInstances.Values.ToList();
        }

        /// <summary>
        /// Gets information about a room instance that corresponds to a given room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public RoomInstance GetRoomInstance(RoomBase room)
        {
            return roomInstances[room];
        }

        /// <summary>
        /// Gets the internal representation of the generated layout.
        /// </summary>
        /// <returns></returns>
        public MapLayout<RoomBase> GetInternalLayoutRepresentation()
        {
            return mapLayout;
        }

        /// <summary>
        /// Gets all the shared tilemaps.
        /// </summary>
        /// <returns></returns>
        public List<Tilemap> GetSharedTilemaps()
        {
            return RoomTemplateUtils.GetTilemaps(RootGameObject);
        }
    }
}