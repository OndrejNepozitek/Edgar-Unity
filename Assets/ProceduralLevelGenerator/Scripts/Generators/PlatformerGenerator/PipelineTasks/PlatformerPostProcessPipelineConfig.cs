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
            var callbacks = new PriorityCallbacks<PlatformerPostProcessCallback>();

            // Register default callbacks
            RegisterCallbacks(callbacks);

            // Register custom callbacks
            if (config.CustomPostProcessTasks != null)
            {
                foreach (var postProcessTask in config.CustomPostProcessTasks)
                {
                    postProcessTask.SetRandomGenerator(Payload.Random);
                    postProcessTask.RegisterCallbacks(callbacks);
                    callbacks.RegisterAfterAll(postProcessTask.Run);
                }
            }

            // Run callbacks
            foreach (var callback in callbacks.GetCallbacks())
            {
                callback(Payload.GeneratedLevel, Payload.LevelDescription);
            }
        }

        private void RegisterCallbacks(PriorityCallbacks<PlatformerPostProcessCallback> callbacks)
        {
            var config = Config.Config;

            if (config.InitializeSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPriorities.InitializeSharedTilemaps, (level, description) =>
                {
                    var tilemapLayersHandler = (ITilemapLayersHandler) config.TilemapLayersHandlerBase ?? new PlatformerTilemapLayersHandler();
                    PostProcessUtils.InitializeSharedTilemaps(level, tilemapLayersHandler);
                });
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPriorities.CopyTilesToSharedTilemaps, (level, description) =>
                {
                    ProUtils.CopyTilesToSharedTilemaps(level);
                });
            }

            if (config.CenterGrid)
            {
                callbacks.RegisterCallback(PostProcessPriorities.CenterGrid, (level, description) =>
                {
                    PostProcessUtils.CenterGrid(level);
                });
            }

            if (config.DisableRoomTemplatesRenderers)
            {
                callbacks.RegisterCallback(PostProcessPriorities.DisableRoomTemplateRenderers, (level, description) =>
                {
                    PostProcessUtils.DisableRoomTemplatesRenderers(level);
                });
            }

            if (config.DisableRoomTemplatesColliders)
            {
                callbacks.RegisterCallback(PostProcessPriorities.DisableRoomTemplateColliders, (level, description) =>
                {
                    PostProcessUtils.DisableRoomTemplatesColliders(level);
                });
            }
        }
    }
}