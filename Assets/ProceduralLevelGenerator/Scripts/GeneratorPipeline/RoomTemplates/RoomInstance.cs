using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Doors;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates
{
    /// <summary>
    ///     Class that holds information about a laid out room.
    /// </summary>
    /// <typeparam name="TRoom"></typeparam>
    public class RoomInstance
    {
        public RoomInstance(Room room, Connection connection, GameObject roomTemplatePrefab, GameObject roomTemplateInstance, Vector3Int position,
            IRoom<Room> generatorData, bool isCorridor)
        {
            Room = room;
            Connection = connection;
            RoomTemplatePrefab = roomTemplatePrefab;
            RoomTemplateInstance = roomTemplateInstance;
            Position = position;
            GeneratorData = generatorData;
            IsCorridor = isCorridor;
        }

        /// <summary>
        ///     The room associated with this room instance.
        /// </summary>
        public Room Room { get; }

        /// <summary>
        ///     Whether the room instance corresponds to a Room or to a Corridor.
        /// </summary>
        public bool IsCorridor { get; }

        /// <summary>
        ///     If this is a corridor room, this property contains the corresponding connection.
        ///     Otherwise it is null.
        ///     TODO: how to provide this value?
        /// </summary>
        public Connection Connection { get; }

        /// <summary>
        ///     Room template that was selected for a given room.
        /// </summary>
        /// <remarks>
        ///     This is the original saved asset used in the input.
        /// </remarks>
        public GameObject RoomTemplatePrefab { get; }

        /// <summary>
        ///     Instance of the RoomTemplatePrefab that is correctly positioned.
        /// </summary>
        /// <remarks>
        ///     This is a new instance of a corresponding room template.
        ///     It is moved to a correct position and transformed/rotated.
        ///     It can be used to copy tiles from the template to the combined tilemaps.
        /// </remarks>
        public GameObject RoomTemplateInstance { get; }

        /// <summary>
        ///     Position of the room relative to the generated layout.
        /// </summary>
        /// <remarks>
        ///     To obtain a position in the combined tilemaps, this position
        ///     must be added to relative positions of tile in Room's tilemaps.
        /// </remarks>
        public Vector3Int Position { get; }

        /// <summary>
        ///     List of doors.
        /// </summary>
        public List<DoorInstance> Doors { get; set; }

        /// <summary>
        ///     Information from the dungeon generator library.
        /// </summary>
        public IRoom<Room> GeneratorData { get; }
    }
}