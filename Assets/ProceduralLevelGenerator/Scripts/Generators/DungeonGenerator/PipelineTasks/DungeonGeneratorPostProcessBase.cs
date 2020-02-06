using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorPostProcessBase : PipelineTask<DungeonGeneratorPayload>
    {
        public override void Process()
        {
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
        }

        protected abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}