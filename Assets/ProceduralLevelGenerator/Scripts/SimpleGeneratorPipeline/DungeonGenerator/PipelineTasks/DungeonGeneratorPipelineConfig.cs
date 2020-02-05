using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Logic;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class DungeonGeneratorPipelineConfig : PipelineConfig
    {
        public DungeonGeneratorConfig Config;
    }

    public class DungeonGeneratorPipelineTask<TPayload> : GraphBasedGeneratorBaseTask<TPayload, DungeonGeneratorPipelineConfig>
        where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override void Process()
        {
            var dungeonGenerator = new GraphBasedDungeonGenerator();
            var generatedLevel = dungeonGenerator.Generate(Payload.LevelDescription, Payload.Random, Config.Config);
            Payload.GeneratedLevel = generatedLevel;
        }
    }
}