#pragma warning disable 612, 618
using System;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Class containing information about a door of a room.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="DoorInstance"/> for an actual implementation.
    /// The DoorInstance class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of DoorInstance will move to this file.
    /// </remarks>
    [Serializable]
    public class DoorInstanceGrid2D : DoorInstance
    {
        public DoorInstanceGrid2D(OrthogonalLine doorLine, Vector2Int facingDirection, RoomBase connectedRoom, RoomInstanceGrid2D connectedRoomInstance) : base(doorLine, facingDirection, connectedRoom, connectedRoomInstance)
        {
        }
    }
}
#pragma warning restore 612, 618
