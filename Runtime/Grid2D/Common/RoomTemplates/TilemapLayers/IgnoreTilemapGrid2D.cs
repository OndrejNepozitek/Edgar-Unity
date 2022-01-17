using UnityEngine;

#pragma warning disable 612, 618
namespace Edgar.Unity
{
    /// <summary>
    /// Component that can be used as a flag to ignore a tilemap layer to which it is attached in various situations.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="IgnoreTilemap"/> for an actual implementation.
    /// The IgnoreTilemap class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of IgnoreTilemap will move to this file.
    /// </remarks>
    [AddComponentMenu("Edgar/Grid2D/Ignore Tilemap (Grid2D)")]
    public class IgnoreTilemapGrid2D : IgnoreTilemap
    {
    }
}
#pragma warning restore 612, 618
