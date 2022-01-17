using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

#pragma warning disable 612, 618
namespace Edgar.Unity
{
    /// <summary>
    /// Holds information about the generated level.
    /// Currently cannot be serialized.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="GeneratedLevel"/> for an actual implementation.
    /// The GeneratedLevel class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of GeneratedLevel will move to this file.
    /// </remarks>
    public class DungeonGeneratorLevelGrid2D : GeneratedLevel
    {
        public DungeonGeneratorLevelGrid2D(Dictionary<RoomBase, RoomInstanceGrid2D> roomInstances, LayoutGrid2D<RoomBase> mapLayout, GameObject rootGameObject, LevelDescriptionGrid2D levelDescription) : base(roomInstances, mapLayout, rootGameObject, levelDescription)
        {
        }
    }
}
#pragma warning restore 612, 618
