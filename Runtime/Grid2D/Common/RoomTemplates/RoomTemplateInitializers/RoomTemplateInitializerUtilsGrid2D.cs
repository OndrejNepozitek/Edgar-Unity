#pragma warning disable 612, 618
namespace Edgar.Unity
{
    /// <summary>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="RoomTemplateInitializerUtils"/> for an actual implementation.
    /// The RoomTemplateInitializerUtils class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of RoomTemplateInitializerUtils will move to this file.
    /// </summary>
    public static class RoomTemplateInitializerUtilsGrid2D
    {
        /// <summary>
        /// Creates a room template prefab using a given room template initializer.
        /// </summary>
        /// <typeparam name="TRoomTemplateInitializer"></typeparam>
        public static void CreateRoomTemplatePrefab<TRoomTemplateInitializer>() where TRoomTemplateInitializer : RoomTemplateInitializerBaseGrid2D
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<TRoomTemplateInitializer>();
        }
    }
}
#pragma warning restore 612, 618
