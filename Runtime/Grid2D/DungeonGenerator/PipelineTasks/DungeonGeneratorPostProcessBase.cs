using System;
using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity
{
    public delegate void DungeonGeneratorPostProcessCallback(DungeonGeneratorLevelGrid2D level, LevelDescriptionGrid2D levelDescription);

    public delegate void DungeonGeneratorPostProcessCallbackGrid2D(DungeonGeneratorLevelGrid2D level);

    /// <summary>
    /// Base class for custom post-processing logic.
    /// </summary>
    [Obsolete("Please use DungeonGeneratorPostProcessingGrid2D instead.")]
    public abstract class DungeonGeneratorPostProcessBase : ScriptableObject
    {
        /// <summary>
        /// Instance of the random numbers generator that is shared throughout the whole generator.
        /// </summary>
        public Random Random { get; private set; }

        /// <summary>
        /// Runs the post-processing logic with a given generated level and corresponding level description.
        /// </summary>
        [Obsolete("Please use Run(GeneratedLevelGrid2D level) instead.")]
        public virtual void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

        }

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}