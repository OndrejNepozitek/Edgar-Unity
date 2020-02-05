using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.Common;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Logic;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class PostProcessPipelineConfig : PipelineConfig
    {
        public PostProcessConfig Config;
    }

    public class PostProcessPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, PostProcessPipelineConfig>
        where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override void Process()
        {
            var config = Config.Config;

            if (config.CombineTilemaps)
            {
                var tilemapLayersHandler = (ITilemapLayersHandler) config.TilemapLayersHandler ?? new DungeonTilemapLayersHandler();
                PostProcessUtils.CombineTilemaps(Payload.GeneratedLevel, tilemapLayersHandler);
            }

            if (config.CenterGrid)
            {
                PostProcessUtils.CenterGrid(Payload.GeneratedLevel);
            }

            if (config.DisableRoomTemplatesRenderers)
            {
                PostProcessUtils.DisableRoomTemplatesRenderers(Payload.GeneratedLevel);
            }

            if (config.DisableRoomTemplatesColliders)
            {
                PostProcessUtils.DisableRoomTemplatesColliders(Payload.GeneratedLevel);
            }
        }
    }
}