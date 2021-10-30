using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for post-processing logic implemented as a MonoBehaviour.
    /// </summary>
    public abstract class DungeonGeneratorPostProcessingComponentGrid2D : MonoBehaviour, IDungeonGeneratorPostProcessing<DungeonGeneratorLevelGrid2D>
    {
        /// <inheritdoc />
        public Random Random { get; private set; }

        /// <inheritdoc />
        public abstract void Run(DungeonGeneratorLevelGrid2D level);

        /// <inheritdoc />
        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}