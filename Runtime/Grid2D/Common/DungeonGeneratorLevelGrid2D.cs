using System;
using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity
{
    /// <summary>
    /// Holds information about the generated level.
    /// Currently cannot be serialized.
    /// </summary>
    public class DungeonGeneratorLevelGrid2D : GeneratedLevelBase<RoomInstanceGrid2D, LevelDescriptionGrid2D>
    {
        private readonly LayoutGrid2D<RoomBase> mapLayout;

        public DungeonGeneratorLevelGrid2D(Dictionary<RoomBase, RoomInstanceGrid2D> roomInstances, LayoutGrid2D<RoomBase> mapLayout, GameObject rootGameObject, LevelDescriptionGrid2D levelDescription) : base(roomInstances, rootGameObject, levelDescription)
        {
            this.mapLayout = mapLayout;
        }

        /// <summary>
        /// Gets information about all the rooms that are present in the generated level.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Please use the RoomInstances property instead.")]
        public List<RoomInstanceGrid2D> GetRoomInstances()
        {
            return RoomInstances;
        }


        /// <summary>
        /// Gets the internal representation of the generated layout.
        /// </summary>
        /// <remarks>
        /// This method is usually only used by for very advanced/debugging use cases.
        /// </remarks>
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