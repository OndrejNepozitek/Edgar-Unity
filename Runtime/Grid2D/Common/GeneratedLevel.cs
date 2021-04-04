using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    /// <summary>
    /// Holds information about the generated level.
    /// Currently cannot be serialized.
    /// </summary>
    [Obsolete("Please use GeneratedLevelGrid2D instead.")]
    public class GeneratedLevel
    {
        /// <summary>
        /// GameObject that hold the generated level.
        /// </summary>
        public GameObject RootGameObject { get; }

        private readonly LayoutGrid2D<RoomBase> mapLayout;
        private readonly Dictionary<RoomBase, RoomInstanceGrid2D> roomInstances;

        public GeneratedLevel(Dictionary<RoomBase, RoomInstanceGrid2D> roomInstances, LayoutGrid2D<RoomBase> mapLayout, GameObject rootGameObject)
        {
            this.roomInstances = roomInstances;
            this.mapLayout = mapLayout;
            RootGameObject = rootGameObject;
        }

        /// <summary>
        /// Gets information about all the rooms that are present in the generated level.
        /// </summary>
        /// <returns></returns>
        public List<RoomInstanceGrid2D> GetRoomInstances()
        {
            return roomInstances.Values.ToList();
        }

        /// <summary>
        /// Gets information about a room instance that corresponds to a given room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public RoomInstanceGrid2D GetRoomInstance(RoomBase room)
        {
            return roomInstances[room];
        }

        /// <summary>
        /// Gets the internal representation of the generated layout.
        /// </summary>
        /// <returns></returns>
        public LayoutGrid2D<RoomBase> GetInternalLayoutRepresentation()
        {
            return mapLayout;
        }

        /// <summary>
        /// Gets all the shared tilemaps.
        /// </summary>
        /// <returns></returns>
        public List<Tilemap> GetSharedTilemaps()
        {
            return RoomTemplateUtilsGrid2D.GetTilemaps(RootGameObject);
        }
    }
}