using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorPostProcessBase : PipelineTask<DungeonGeneratorPayload>
    {
        protected Random Random;

        public override void Process()
        {
            Random = Payload.Random;
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
        }

        protected abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}