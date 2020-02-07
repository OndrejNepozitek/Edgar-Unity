using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Logic;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class PlatformerGeneratorPipelineConfig : PipelineConfig
    {
        public PlatformerGeneratorConfig Config;
    }

    public class PlatformerGeneratorPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, PlatformerGeneratorPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    { 
        public override void Process()
        {
            var generator = new Logic.PlatformerGenerator();
            var (generatedLevel, stats) = generator.Generate(Payload.LevelDescription, Payload.Random, Config.Config);
            ((IGraphBasedGeneratorPayload) Payload).GeneratedLevel = generatedLevel;
            Payload.GeneratorStats = stats;

            Debug.Log($"Layout generated in {stats.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{stats.Iterations} iterations needed, {stats.Iterations / (stats.TimeTotal / 1000d):0} iterations per second");
        }
    }
}