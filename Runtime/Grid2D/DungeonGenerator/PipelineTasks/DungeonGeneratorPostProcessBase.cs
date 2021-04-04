using System;
using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity
{
    public delegate void DungeonGeneratorPostProcessCallback(GeneratedLevelGrid2D level, LevelDescriptionGrid2D levelDescription);

    /// <summary>
    /// Base class for custom post-processing logic.
    /// </summary>
    [Obsolete("Please use DungeonGeneratorPostProcessBaseGrid2D instead.")]
    public abstract class DungeonGeneratorPostProcessBase : ScriptableObject
    {
        /// <summary>
        /// Instance of the random numbers generator that is shared throughout the whole generator.
        /// </summary>
        protected Random Random;

        /// <summary>
        /// Runs the post-processing logic with a given generated level and corresponding level description.
        /// </summary>
        [Obsolete("Please use Run(GeneratedLevelGrid2D level, LevelDescription levelDescription) instead.")]
        public virtual void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

        }

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}