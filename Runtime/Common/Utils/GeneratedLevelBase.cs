using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Holds information about a procedurally generated level.
    /// </summary>
    /// <remarks>
    /// Currently cannot be serialized by the default Unity serializer.
    /// </remarks>
    public abstract class GeneratedLevelBase<TRoomInstance, TLevelDescription>
    {
        /// <summary>
        /// GameObject that holds the generated level.
        /// </summary>
        public GameObject RootGameObject { get; }

        /// <summary>
        /// Level description based on which was the level generated.
        /// </summary>
        public TLevelDescription LevelDescription { get; }

        /// <summary>
        /// Room instances of all the rooms in the level.
        /// </summary>
        /// <remarks>
        /// For quick access to individual room instances, <see cref="GetRoomInstance"/> can be used.
        /// </remarks>
        public List<TRoomInstance> RoomInstances { get; }

        /// <summary>
        /// Seed that was used by the random numbers generator.
        /// </summary>
        public int Seed { get; }

        private readonly Dictionary<RoomBase, TRoomInstance> roomInstances;

        protected GeneratedLevelBase(Dictionary<RoomBase, TRoomInstance> roomInstances, GameObject rootGameObject, TLevelDescription levelDescription, int seed)
        {
            this.roomInstances = roomInstances;
            RoomInstances = roomInstances.Values.ToList();
            RootGameObject = rootGameObject;
            LevelDescription = levelDescription;
            Seed = seed;
        }

        public TRoomInstance GetRoomInstance(RoomBase room)
        {
            return roomInstances[room];
        }
    }
}