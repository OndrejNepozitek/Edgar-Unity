using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorInputBase : PipelineTask<DungeonGeneratorPayload>
    {
        public override void Process()
        {
            Payload.LevelDescription = GetLevelDescription();
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}