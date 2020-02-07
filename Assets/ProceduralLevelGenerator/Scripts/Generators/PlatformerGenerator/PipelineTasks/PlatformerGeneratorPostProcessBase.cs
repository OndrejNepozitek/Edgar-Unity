using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    public abstract class PlatformerGeneratorPostProcessBase : PipelineTask<PlatformerGeneratorPayload>
    {
        public override void Process()
        {
            Run(Payload.GeneratedLevel, Payload.LevelDescription);
        }

        protected abstract void Run(GeneratedLevel level, LevelDescription levelDescription);
    }
}