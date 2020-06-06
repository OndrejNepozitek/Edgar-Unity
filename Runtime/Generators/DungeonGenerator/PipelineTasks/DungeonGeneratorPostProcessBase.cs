using ProceduralLevelGenerator.Unity.Generators.Common;
using UnityEngine;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public delegate void DungeonGeneratorPostProcessCallback(GeneratedLevel level, LevelDescription levelDescription);

    public abstract class DungeonGeneratorPostProcessBase : ScriptableObject
    {
        protected Random Random;

        public virtual void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

        }

        public void SetRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}