using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for custom post-processing logic.
    /// </summary>
    public class DungeonGeneratorPostProcessingGrid2D : ScriptableObject, IDungeonGeneratorPostProcessing<DungeonGeneratorLevelGrid2D>
    {
        public Random Random { get; private set; }

        /// <inheritdoc />
        public virtual void Run(DungeonGeneratorLevelGrid2D level)
        {

        }

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}

