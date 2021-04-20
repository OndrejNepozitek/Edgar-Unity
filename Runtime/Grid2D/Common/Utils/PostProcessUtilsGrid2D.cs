using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

#pragma warning disable 612, 618
namespace Edgar.Unity
{
    /// <summary>
    /// Utility post-processing functions
    /// </summary>
    /// <remarks>
    /// This file is temporarily empty to make it easier to adapt the new classNameGrid2D naming convention.
    /// The motivation for this action is to prevent name clashes in the future when/if a 3D version is released.
    /// 
    /// See <see cref="PostProcessUtils"/> for an actual implementation.
    /// The PostProcessUtils class is now obsolete and will be removed in a future release.
    /// When that happens, the implementation of PostProcessUtils will move to this file.
    /// </remarks>
    public static class PostProcessUtilsGrid2D
    {
        /// <summary>
        /// Gets the center point of given tilemaps-
        /// </summary>
        /// <param name="tilemaps"></param>
        /// <param name="compressBounds">Whether to compress bounds of individual tilemaps before computing the center.</param>
        /// <returns></returns>
        public static Vector3 GetTilemapsCenter(List<Tilemap> tilemaps, bool compressBounds = false)
        {
            return PostProcessUtils.GetTilemapsCenter(tilemaps, compressBounds);
        }

        /// <summary>
        /// Position the grid so that the level is centered at (0,0).
        /// </summary>
        /// <param name="level"></param>
        /// <param name="compressBounds">Whether to compress bounds of individual tilemaps before computing the center.</param>
        public static void CenterGrid(GeneratedLevelGrid2D level, bool compressBounds = false)
        {
            PostProcessUtils.CenterGrid(level, compressBounds);
        }

        /// <summary>
        /// Initializes shared tilemaps of a given level.
        /// </summary>
        /// <param name="level">Generated level.</param>
        /// <param name="mode">Tilemap layers mode.</param>
        /// <param name="defaultTilemapLayersHandler">Default tilemap layers handler. Used for the Default mode.</param>
        /// <param name="customTilemapLayersHandler">Custom tilemap layers handler. Used for the Custom mode.</param>
        /// <param name="example">Example game object for tilemaps structure. Used for the FromExample mode.</param>
        public static void InitializeSharedTilemaps(GeneratedLevelGrid2D level, TilemapLayersStructureModeGrid2D mode, ITilemapLayersHandlerGrid2D defaultTilemapLayersHandler, ITilemapLayersHandlerGrid2D customTilemapLayersHandler, GameObject example)
        {
            PostProcessUtils.InitializeSharedTilemaps(level, mode, defaultTilemapLayersHandler, customTilemapLayersHandler, example);
        }

        /// <summary>
        /// Sets a given material to all shared tilemap layers.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="tilemapMaterial"></param>
        public static void SetTilemapsMaterial(GeneratedLevel level, Material tilemapMaterial)
        {
            PostProcessUtils.SetTilemapsMaterial(level, tilemapMaterial);
        }

        /// <summary>
        /// Copies to from individual room templates to shared tilemaps.
        /// </summary>
        /// <remarks>
        /// The order is important. First, copy all basic rooms and only then copy corridor rooms.
        /// </remarks>
        /// <param name="level"></param>
        public static void CopyTilesToSharedTilemaps(GeneratedLevel level)
        {
            PostProcessUtils.CopyTilesToSharedTilemaps(level);
        }

        /// <summary>
        /// Copies tiles from a given room template to given destination tilemaps.
        /// </summary>
        /// <remarks>
        /// One important aspect of this method is how to handle already existing tiles in destination tilemaps.
        ///
        /// When deleteNonNullTiles is true, it computes all non-null positions across all layers in the room template.
        /// After that, it deletes all tiles on these positions in destination tilemaps.
        ///
        /// When deleteTilesInsideOutline is true, it computes all tiles inside the outline of the room template and
        /// deletes them from the destination tilemaps.
        /// So even if there is a hole inside the room template, the position is still removed.
        ///
        /// deleteNonNullTiles and deleteTilesInsideOutline can be combined together.
        /// </remarks>
        /// <param name="roomInstance">Room instance to be copied to the destination tilemaps.</param>
        /// <param name="destinationTilemaps">List of destination tilemaps.</param>
        /// <param name="deleteNonNullTiles">Whether to delete non-null tiles from destination tilemaps.</param>
        /// <param name="deleteTilesInsideOutline">Whether to delete all tiles insides the outline from destination tilemaps.</param>
        public static void CopyTiles(RoomInstance roomInstance, List<Tilemap> destinationTilemaps, bool deleteNonNullTiles, bool deleteTilesInsideOutline)
        {
            PostProcessUtils.CopyTiles(roomInstance, destinationTilemaps, deleteNonNullTiles, deleteTilesInsideOutline);
        }

        /// <summary>
        /// Disables tilemap renderers in room template instances.
        /// </summary>
        /// <remarks>
        /// This method is useful when using shared tilemaps.
        /// </remarks>
        /// <param name="level"></param>
        public static void DisableRoomTemplatesRenderers(GeneratedLevel level)
        {
            PostProcessUtils.DisableRoomTemplatesRenderers(level);
        }

        /// <summary>
        /// Disables colliders of individual room template tilemaps in the generated level.
        /// The goal is to try to keep triggers functioning.
        /// </summary>
        /// <param name="level"></param>
        public static void DisableRoomTemplatesColliders(GeneratedLevel level)
        {
            PostProcessUtils.DisableRoomTemplatesColliders(level);
        }

        // TODO: where to put this?
        public static void Destroy(Object gameObject)
        {
            PostProcessUtils.Destroy(gameObject);
        }
    }
}
#pragma warning restore 612, 618
