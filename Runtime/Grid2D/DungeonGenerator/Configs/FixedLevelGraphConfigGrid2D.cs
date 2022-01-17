using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Config for the default input for the generator - a fixed level graph.
    /// </summary>
    [Serializable]
    public class FixedLevelGraphConfigGrid2D
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