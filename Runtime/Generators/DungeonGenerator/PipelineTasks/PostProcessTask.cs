using System;
using System.Collections;
using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    /// <summary>
    /// Handles individual post-processing steps.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class PostProcessTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private readonly PostProcessConfig config;
        private readonly Func<ITilemapLayersHandler> defaultTilemapLayersHandlerFactory;
        private readonly List<DungeonGeneratorPostProcessBase> customPostProcessTasks;

        public PostProcessTask(PostProcessConfig config, Func<ITilemapLayersHandler> defaultTilemapLayersHandlerFactory, List<DungeonGeneratorPostProcessBase> customPostProcessTasks)
        {
            this.config = config;
            this.defaultTilemapLayersHandlerFactory = defaultTilemapLayersHandlerFactory;
            this.customPostProcessTasks = customPostProcessTasks;
        }

        public override IEnumerator Process()
        {
            var callbacks = new PriorityCallbacks<DungeonGeneratorPostProcessCallback>();

            // Register default callbacks
            RegisterCallbacks(callbacks);

            // Register custom callbacks
            if (customPostProcessTasks != null)
            {
                foreach (var postProcessTask in customPostProcessTasks)
                {
                    postProcessTask.SetRandomGenerator(Payload.Random);
                    callbacks.RegisterAfterAll(postProcessTask.Run);
                }
            }

            // Run callbacks
            foreach (var callback in callbacks.GetCallbacks())
            {
                callback(Payload.GeneratedLevel, Payload.LevelDescription);
                yield return null;
            }
        }

        private void RegisterCallbacks(PriorityCallbacks<DungeonGeneratorPostProcessCallback> callbacks)
        {
            if (config.InitializeSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPriorities.InitializeSharedTilemaps, (level, description) =>
                {
                    var tilemapLayersHandler = config.TilemapLayersHandler ? config.TilemapLayersHandler : defaultTilemapLayersHandlerFactory();
                    PostProcessUtils.InitializeSharedTilemaps(level, tilemapLayersHandler);
                });
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPriorities.CopyTilesToSharedTilemaps, (level, description) =>
                {
                    PostProcessUtils.CopyTilesToSharedTilemaps(level);
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