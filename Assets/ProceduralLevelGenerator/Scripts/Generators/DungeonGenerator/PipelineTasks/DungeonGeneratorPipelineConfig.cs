using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Logic;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class DungeonGeneratorPipelineConfig : PipelineConfig
    {
        public DungeonGeneratorConfig Config;
    }

    public class DungeonGeneratorPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, DungeonGeneratorPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    { 
        public override void Process()
        {
            var dungeonGenerator = new GraphBasedDungeonGenerator();
            var (generatedLevel, stats) = dungeonGenerator.Generate(Payload.LevelDescription, Payload.Random, Config.Config);
            ((IGraphBasedGeneratorPayload) Payload).GeneratedLevel = generatedLevel;
            Payload.GeneratorStats = stats;

            Debug.Log($"Layout generated in {stats.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{stats.Iterations} iterations needed, {stats.Iterations / (stats.TimeTotal / 1000d):0} iterations per second");
        }
    }
}