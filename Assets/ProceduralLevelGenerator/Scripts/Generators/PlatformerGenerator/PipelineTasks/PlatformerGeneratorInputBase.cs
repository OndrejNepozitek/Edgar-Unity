using System.Collections;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    public abstract class PlatformerGeneratorInputBase : PipelineTask<PlatformerGeneratorPayload>
    {
        public override IEnumerator Process()
        {
            Payload.LevelDescription = GetLevelDescription();
            yield return null;
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}