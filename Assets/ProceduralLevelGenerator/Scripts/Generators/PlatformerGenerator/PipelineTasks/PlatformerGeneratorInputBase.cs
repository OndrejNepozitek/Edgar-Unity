using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    public abstract class PlatformerGeneratorInputBase : PipelineTask<PlatformerGeneratorPayload>
    {
        public override void Process()
        {
            Payload.LevelDescription = GetLevelDescription();
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}