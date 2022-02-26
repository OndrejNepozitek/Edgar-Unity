using System;
using System.Collections;
using System.Collections.Generic;

namespace Edgar.Unity
{
    /// <summary>
    /// Handles individual post-processing steps.
    /// </summary>
    internal class PostProcessingTaskGrid2D : PipelineTask<DungeonGeneratorPayloadGrid2D>
    {
        private readonly PostProcessingConfigGrid2D config;
        private readonly Func<ITilemapLayersHandlerGrid2D> defaultTilemapLayersHandlerFactory;

        // TODO(rename):
        private readonly List<DungeonGeneratorPostProcessingGrid2D> customPostProcessingTasks;
        private readonly List<DungeonGeneratorPostProcessingComponentGrid2D> customPostProcessingComponents;

        public PostProcessingTaskGrid2D(
            PostProcessingConfigGrid2D config,
            Func<ITilemapLayersHandlerGrid2D> defaultTilemapLayersHandlerFactory,
            List<DungeonGeneratorPostProcessingGrid2D> customPostProcessingTasks,
            List<DungeonGeneratorPostProcessingComponentGrid2D> customPostProcessingComponents)
        {
            this.config = config;
            this.defaultTilemapLayersHandlerFactory = defaultTilemapLayersHandlerFactory;
            this.customPostProcessingTasks = customPostProcessingTasks;
            this.customPostProcessingComponents = customPostProcessingComponents;
        }

        public override IEnumerator Process()
        {
            var callbacks = new PriorityCallbacks<DungeonGeneratorPostProcessCallbackGrid2D>();

            // Register default callbacks
            RegisterCallbacks(callbacks);

            // Register custom callbacks from scriptable objects
            if (customPostProcessingTasks != null)
            {
                foreach (var postProcessingTask in customPostProcessingTasks)
                {
                    if (postProcessingTask == null)
                    {
                        continue;
                    }

                    postProcessingTask.SetRandomGenerator(Payload.Random);
                    callbacks.RegisterAfterAll(postProcessingTask.Run);
                }
            }

            // Register custom callbacks from components
            if (customPostProcessingComponents != null)
            {
                foreach (var postProcessingTask in customPostProcessingComponents)
                {
                    if (postProcessingTask == null)
                    {
                        continue;
                    }

                    postProcessingTask.SetRandomGenerator(Payload.Random);
                    callbacks.RegisterAfterAll(postProcessingTask.Run);
                }
            }

            // Run callbacks
            foreach (var callback in callbacks.GetCallbacks())
            {
                callback(Payload.GeneratedLevel);
                yield return null;
            }
        }

        private void RegisterCallbacks(PriorityCallbacks<DungeonGeneratorPostProcessCallbackGrid2D> callbacks)
        {
            if (config.InitializeSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.InitializeSharedTilemaps, (level) =>
                {
                    PostProcessUtilsGrid2D.InitializeSharedTilemaps(level, config.TilemapLayersStructure, defaultTilemapLayersHandlerFactory(), config.TilemapLayersHandler, config.TilemapLayersExample);
                    PostProcessUtilsGrid2D.SetTilemapsMaterial(level, config.TilemapMaterial);
                });
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CopyTilesToSharedTilemaps, (level) =>
                {
                    PostProcessUtilsGrid2D.CopyTilesToSharedTilemaps(level);
                });
            }

            if (config.CenterGrid)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CenterGrid, (level) =>
                {
                    PostProcessUtilsGrid2D.CenterGrid(level);
                });
            }

            if (config.DisableRoomTemplatesRenderers)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateRenderers, (level) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplateRenderers(level);
                });
            }

            if (config.DisableRoomTemplatesColliders)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateColliders, (level) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplateColliders(level);
                });
            }
        }
    }
}