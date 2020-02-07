using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Pro;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class PlatformerPostProcessPipelineConfig : PipelineConfig
    {
        public PostProcessConfig Config;
    }

    public class PlatformerPostProcessPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, PlatformerPostProcessPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override void Process()
        {
            var config = Config.Config;

            if (config.InitializeSharedTilemaps)
            {
                // TODO: change the default handler
                var tilemapLayersHandler = (ITilemapLayersHandler) config.TilemapLayersHandlerBase ?? new DungeonTilemapLayersHandler();
                ProUtils.InitializeSharedTilemaps(Payload.GeneratedLevel, tilemapLayersHandler);
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                ProUtils.CopyTilesToSharedTilemaps(Payload.GeneratedLevel);
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