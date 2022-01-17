#pragma warning disable 612, 618
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
        private readonly List<DungeonGeneratorPostProcessBase> customPostProcessingTasks;
        private readonly List<DungeonGeneratorPostProcessingComponentGrid2D> customPostProcessingComponents;

        public PostProcessingTaskGrid2D(
            PostProcessingConfigGrid2D config,
            Func<ITilemapLayersHandlerGrid2D> defaultTilemapLayersHandlerFactory,
            List<DungeonGeneratorPostProcessBase> customPostProcessingTasks,
            List<DungeonGeneratorPostProcessingComponentGrid2D> customPostProcessingComponents)
        {
            this.config = config;
            this.defaultTilemapLayersHandlerFactory = defaultTilemapLayersHandlerFactory;
            this.customPostProcessingTasks = customPostProcessingTasks;
            this.customPostProcessingComponents = customPostProcessingComponents;
        }

        public override IEnumerator Process()
        {
            var callbacks = new PriorityCallbacks<DungeonGeneratorPostProcessCallback>();

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

                    // Check if the task inherits from the refactored base class
                    if (postProcessingTask is DungeonGeneratorPostProcessingGrid2D grid2DTask)
                    {
                        callbacks.RegisterAfterAll(WrapCallback(grid2DTask.Run));
                    }
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
                    callbacks.RegisterAfterAll(WrapCallback(postProcessingTask.Run));
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
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.InitializeSharedTilemaps, (level, _) =>
                {
                    PostProcessUtilsGrid2D.InitializeSharedTilemaps(level, config.TilemapLayersStructure, defaultTilemapLayersHandlerFactory(), config.TilemapLayersHandler, config.TilemapLayersExample);
                    PostProcessUtilsGrid2D.SetTilemapsMaterial(level, config.TilemapMaterial);
                });
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CopyTilesToSharedTilemaps, (level, _) =>
                {
                    PostProcessUtilsGrid2D.CopyTilesToSharedTilemaps(level);
                });
            }

            if (config.CenterGrid)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CenterGrid, (level, _) =>
                {
                    PostProcessUtilsGrid2D.CenterGrid(level);
                });
            }

            if (config.DisableRoomTemplatesRenderers)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateRenderers, (level, _) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplateRenderers(level);
                });
            }

            if (config.DisableRoomTemplatesColliders)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateColliders, (level, _) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplateColliders(level);
                });
            }
        }

        private static DungeonGeneratorPostProcessCallback WrapCallback(DungeonGeneratorPostProcessCallbackGrid2D callback)
        {
            return (level, _) => callback(level);
        }
    }
}
#pragma warning restore 612, 618