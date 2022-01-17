using System;

namespace Edgar.Unity
{
    [Obsolete("Please use GeneratorConstantsGrid2D instead.")]
    public class GeneratorConstants
    {
        /// <summary>
        /// Name of the game object that holds tilemaps layers. This name is used both in room templates and in generated levels.
        /// </summary>
        public static string TilemapsRootName => GeneratorConstantsGrid2D.TilemapsRootName;

        /// <summary>
        /// Name of the game object that holds instance of all the room templates that are used in a generated level.
        /// </summary>
        public static string RoomsRootName = GeneratorConstantsGrid2D.RoomsRootName;

        /// <summary>
        /// Name of the Outline Override tilemap layer.
        /// </summary>
        public static string OutlineOverrideLayerName = GeneratorConstantsGrid2D.OutlineOverrideLayerName;
    }
}