using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Logic;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class DungeonGeneratorPipelineConfig : PipelineConfig
    {
        public DungeonGeneratorConfig Config;
    }

    public class DungeonGeneratorPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, DungeonGeneratorPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override void Process()
        {
            var dungeonGenerator = new GraphBasedDungeonGenerator();
            var generatedLevel = dungeonGenerator.Generate(Payload.LevelDescription, Payload.Random, Config.Config);
            Payload.GeneratedLevel = generatedLevel;
        }
    }
}