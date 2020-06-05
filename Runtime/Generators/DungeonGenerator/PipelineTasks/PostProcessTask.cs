using System.Collections;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public class PostProcessTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private readonly PostProcessConfig config;

        public PostProcessTask(PostProcessConfig config)
        {
            this.config = config;
        }

        public override IEnumerator Process()
        {
            if (config.InitializeSharedTilemaps)
            {
                var tilemapLayersHandler = config.TilemapLayersHandler ? config.TilemapLayersHandler : GetDefaultTilemapLayersHandler();
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

        protected virtual ITilemapLayersHandler GetDefaultTilemapLayersHandler()
        {
            return new DungeonTilemapLayersHandler();
        }
    }
}