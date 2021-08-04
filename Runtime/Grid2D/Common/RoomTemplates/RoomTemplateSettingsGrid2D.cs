using UnityEngine;

#pragma warning disable 612, 618
namespace Edgar.Unity
{
    /// <summary>
    /// Component that is attached to each room template game objects and contains basic settings.
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="RoomTemplateSettings"/> for an actual implementation.
    /// The RoomTemplateSettings class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of RoomTemplateSettings will move to this file.
    /// </remarks>
    [AddComponentMenu("Edgar/Grid2D/Room Template Settings (Grid2D)")]
    public class RoomTemplateSettingsGrid2D : RoomTemplateSettings
    {
    }
}
#pragma warning restore 612, 618
