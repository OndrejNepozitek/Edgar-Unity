using System;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs
{
    /// <summary>
    /// Config for the default input for the generator - a fixed level graph.
    /// </summary>
    [Serializable]
    public class FixedLevelGraphConfig
    {
        /// <summary>
        /// Level graph that will be used in the generator.
        /// </summary>
        public LevelGraph LevelGraph;

        /// <summary>
        /// Whether to add corridors between individual rooms in the level graph.
        /// </summary>
        public bool UseCorridors = true;
    }
}