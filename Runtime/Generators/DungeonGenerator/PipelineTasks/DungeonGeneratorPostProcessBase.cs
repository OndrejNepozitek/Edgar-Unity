using System;
using System.Collections;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Pipeline;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorPostProcessBase : PipelineTask<DungeonGeneratorPayload>
    {
        protected Random Random;

        public override IEnumerator Process()
        {
            Random = Payload.Random;
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
            yield return null;
        }

        public abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}