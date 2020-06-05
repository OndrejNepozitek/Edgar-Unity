using System.Collections;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorPostProcessBase : ScriptableObject, IPipelineTask<DungeonGeneratorPayload>
    {
        protected Random Random;

        public DungeonGeneratorPayload Payload { get; set; }

        public IEnumerator Process()
        {
            Random = Payload.Random;
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
            yield return null;
        }

        public abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}