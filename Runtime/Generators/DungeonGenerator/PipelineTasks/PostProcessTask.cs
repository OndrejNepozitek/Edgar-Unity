using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Handles individual post-processing steps.
    /// </summary>
    public class PostProcessTask : PipelineTask<DungeonGeneratorPayload>
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
                    PostProcessUtils.InitializeSharedTilemaps(level, config.TilemapLayersStructure, defaultTilemapLayersHandlerFactory(), config.TilemapLayersHandler, config.TilemapLayersExample);
                    PostProcessUtils.SetTilemapsMaterial(level, config.TilemapMaterial);
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