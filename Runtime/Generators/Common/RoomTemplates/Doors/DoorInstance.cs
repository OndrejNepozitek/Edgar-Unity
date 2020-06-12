using System;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors
{
    /// <summary>
    ///     Class containing information about a door of a room.
    /// </summary>
    [Serializable]
    public class DoorInstance
    {
        /// <summary>
        ///     Line containing all points of the door.
        /// </summary>
        public OrthogonalLine DoorLine => doorLine;
        [SerializeField] private OrthogonalLine doorLine;

        /// <summary>
        ///     Direction in which a room is connected to this door.
        /// </summary>
        /// <remarks>
        ///     Imagine that we have the following room shape and that
        ///     "OO" represents a door.
        ///     ----OO---
        ///     |       |
        ///     |       |
        ///     ---------
        ///     Then the facing direction of the door above is equal to Vector2Int.up.
        ///     ---------
        ///     |       O
        ///     |       O
        ///     ---------
        ///     Here the facing direction is equal to Vector2Int.right.
        /// </remarks>
        public Vector2Int FacingDirection => facingDirection;
        [SerializeField] private Vector2Int facingDirection;

        /// <summary>
        ///     Whether the door line is horizontal or vertical.
        /// </summary>
        public bool IsHorizontal => isHorizontal;
        [SerializeField] private bool isHorizontal;

        /// <summary>
        ///     To which room is the room that contains this door connected.
        /// </summary>
        public RoomBase ConnectedRoom => connectedRoom;
        [SerializeField] private RoomBase connectedRoom;

        /// <summary>
        ///     To which room instance is the room that contains this door connected.
        /// </summary>
        /// <remarks>
        ///     This property is not serialized. Unfortunately, object in Unity are serialized
        ///     by value and that would make Unity try to serialize the whole graph.
        /// </remarks>
        public RoomInstance ConnectedRoomInstance => connectedRoomInstance;
        [NonSerialized] private RoomInstance connectedRoomInstance;

        public DoorInstance(OrthogonalLine doorLine, Vector2Int facingDirection, RoomBase connectedRoom, RoomInstance connectedRoomInstance)
        {
            this.doorLine = doorLine;
            this.facingDirection = facingDirection;
            this.connectedRoom = connectedRoom;
            this.connectedRoomInstance = connectedRoomInstance;
            this.isHorizontal = FacingDirection == Vector2Int.up || FacingDirection == Vector2Int.down;
        }
    }
}