using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Logic;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
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