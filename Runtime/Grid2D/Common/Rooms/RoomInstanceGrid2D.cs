#pragma warning disable 612, 618
using System;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="RoomInstance"/> for an actual implementation.
    /// The RoomInstance class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of RoomInstance will move to this file.
    /// </summary>
    [Serializable]
    public class RoomInstanceGrid2D : RoomInstance
    {
        public RoomInstanceGrid2D(RoomBase room, bool isCorridor, ConnectionBase connection, GameObject roomTemplatePrefab, GameObject roomTemplateInstance, Vector3Int position, Polygon2D outlinePolygon) : base(room, isCorridor, connection, roomTemplatePrefab, roomTemplateInstance, position, outlinePolygon)
        {
        }
    }
}
#pragma warning restore 612, 618
