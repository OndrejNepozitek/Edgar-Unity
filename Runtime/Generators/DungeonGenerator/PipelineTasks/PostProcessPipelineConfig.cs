using System.Collections;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    // TODO: add asset menu
    public class PostProcessPipelineConfig : PipelineConfig
    {
        public PostProcessConfig Config;
    }

    public class PostProcessPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, PostProcessPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    { 
        public override IEnumerator Process()
        {
            var config = Config.Config;

            if (config.InitializeSharedTilemaps)
            {
                var tilemapLayersHandler = (ITilemapLayersHandler) config.TilemapLayersHandler ?? new DungeonTilemapLayersHandler();
                PostProcessUtils.InitializeSharedTilemaps(Payload.GeneratedLevel, tilemapLayersHandler);
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                PostProcessUtils.CopyTilesToSharedTilemaps(Payload.GeneratedLevel);
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

            yield return null;
        }
    }
}