using ProceduralLevelGenerator.Unity.Generators.Common;
using UnityEngine;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public delegate void DungeonGeneratorPostProcessCallback(GeneratedLevel level, LevelDescription levelDescription);

    /// <summary>
    /// Base class for custom post-processing logic.
    /// </summary>
    public abstract class DungeonGeneratorPostProcessBase : ScriptableObject
    {
        /// <summary>
        /// Instance of the random numbers generator that is shared throughout the whole generator.
        /// </summary>
        protected Random Random;

        /// <summary>
        /// Runs the post-processing logic with a given generated level and corresponding level description.
        /// </summary>
        public virtual void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

        }

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}