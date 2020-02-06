using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class PostProcessPipelineConfig : PipelineConfig
    {
        public PostProcessConfig Config;
    }

    public class PostProcessPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, PostProcessPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override void Process()
        {
            var config = Config.Config;

            if (config.CombineTilemaps)
            {
                var tilemapLayersHandler = (ITilemapLayersHandler) config.TilemapLayersHandlerBase ?? new DungeonTilemapLayersHandler();
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