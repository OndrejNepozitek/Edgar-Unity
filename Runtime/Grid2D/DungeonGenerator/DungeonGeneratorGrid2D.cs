using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Dungeon generator. All logic is currently inherited from DungeonGeneratorBase.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="DungeonGenerator"/> for an actual implementation.
    /// The DungeonGenerator class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of DungeonGenerator will move to this file.
    /// </remarks>
    [AddComponentMenu("Edgar/Grid2D/Dungeon Generator (Grid2D)")]
    public class DungeonGeneratorGrid2D : DungeonGeneratorBaseGrid2D
    {
    }
}