#pragma warning disable 612, 618
using System;
using System.Collections;
using System.Collections.Generic;

namespace Edgar.Unity
{
    /// <summary>
    /// Handles individual post-processing steps.
    /// </summary>
    internal class PostProcessTaskGrid2D : PipelineTask<DungeonGeneratorPayloadGrid2D>
    {
        private readonly PostProcessConfigGrid2D config;
        private readonly Func<ITilemapLayersHandlerGrid2D> defaultTilemapLayersHandlerFactory;

        // TODO(rename):
        private readonly List<DungeonGeneratorPostProcessBase> customPostProcessTasks;

        public PostProcessTaskGrid2D(PostProcessConfigGrid2D config, Func<ITilemapLayersHandlerGrid2D> defaultTilemapLayersHandlerFactory, List<DungeonGeneratorPostProcessBase> customPostProcessTasks)
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

                    if (postProcessTask is DungeonGeneratorPostProcessBaseGrid2D grid2Dtask)
                    {
                        callbacks.RegisterAfterAll(grid2Dtask.Run);
                    }
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
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.InitializeSharedTilemaps, (level, description) =>
                {
                    PostProcessUtilsGrid2D.InitializeSharedTilemaps(level, config.TilemapLayersStructure, defaultTilemapLayersHandlerFactory(), config.TilemapLayersHandler, config.TilemapLayersExample);
                    PostProcessUtilsGrid2D.SetTilemapsMaterial(level, config.TilemapMaterial);
                });
            }

            if (config.CopyTilesToSharedTilemaps)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CopyTilesToSharedTilemaps, (level, description) =>
                {
                    PostProcessUtilsGrid2D.CopyTilesToSharedTilemaps(level);
                });
            }

            if (config.CenterGrid)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.CenterGrid, (level, description) =>
                {
                    PostProcessUtilsGrid2D.CenterGrid(level);
                });
            }

            if (config.DisableRoomTemplatesRenderers)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateRenderers, (level, description) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplatesRenderers(level);
                });
            }

            if (config.DisableRoomTemplatesColliders)
            {
                callbacks.RegisterCallback(PostProcessPrioritiesGrid2D.DisableRoomTemplateColliders, (level, description) =>
                {
                    PostProcessUtilsGrid2D.DisableRoomTemplatesColliders(level);
                });
            }
        }
    }
}
#pragma warning restore 612, 618