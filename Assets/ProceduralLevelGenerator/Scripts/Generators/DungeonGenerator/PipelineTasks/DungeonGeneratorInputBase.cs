using System.Collections;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorInputBase : PipelineTask<DungeonGeneratorPayload>
    {
        public override IEnumerator Process()
        {
            Payload.LevelDescription = GetLevelDescription();
            yield return null;
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}