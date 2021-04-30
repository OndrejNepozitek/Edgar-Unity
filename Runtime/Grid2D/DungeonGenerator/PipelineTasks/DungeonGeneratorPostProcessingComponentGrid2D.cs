using UnityEngine;
using Random = System.Random;

namespace Edgar.Unity
{
    public abstract class DungeonGeneratorPostProcessingComponentGrid2D : MonoBehaviour, IDungeonGeneratorPostProcessing
    {
        public Random Random { get; private set; }

        public abstract void Run(DungeonGeneratorLevelGrid2D level);

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}