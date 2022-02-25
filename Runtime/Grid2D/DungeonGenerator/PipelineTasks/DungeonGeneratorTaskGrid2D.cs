using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// The actual generator logic that calls the .NET generator.
    /// </summary>
    internal class DungeonGeneratorTaskGrid2D : PipelineTask<DungeonGeneratorPayloadGrid2D>
    {
        private readonly DungeonGeneratorConfigGrid2D config;

        public DungeonGeneratorTaskGrid2D(DungeonGeneratorConfigGrid2D config)
        {
            this.config = config;
        }

        public override IEnumerator Process()
        {
            var levelDescription = Payload.LevelDescription;

            if (config.Timeout <= 0)
            {
                throw new ArgumentException($"{nameof(config.Timeout)} must be greater than 0", nameof(config.Timeout));
            }

            var rootGameObject = config.RootGameObject;

            // If the root game objects was not set in the config, we do the following:
            // 1. Check if there already exists a game objects with a name reserved for the generated level
            // 2. Otherwise, we create a new empty game object
            if (rootGameObject == null)
            {
                rootGameObject = GameObject.Find("Generated Level");

                if (rootGameObject == null)
                {
                    rootGameObject = new GameObject("Generated Level");
                }
            }

            // We delete all the children from the root game object - we do not want to combine levels from different runs of the algorithm
            foreach (var child in rootGameObject.transform.Cast<Transform>().ToList())
            {
                child.transform.parent = null;
                PostProcessUtilsGrid2D.Destroy(child.gameObject);
            }

            // The LevelDescription class must be converted to MapDescription
            var levelDescriptionGrid2D = levelDescription.GetLevelDescription();
            levelDescriptionGrid2D.MinimumRoomDistance = config.MinimumRoomDistance;
            levelDescriptionGrid2D.RoomTemplateRepeatModeOverride = GeneratorUtilsGrid2D.GetRepeatMode(config.RepeatModeOverride);

            var configuration = new GraphBasedGeneratorConfiguration<RoomBase>()
            {
                EarlyStopIfTimeExceeded = TimeSpan.FromMilliseconds(config.Timeout),
            };

            // We create the instance of the dungeon generator and inject the correct Random instance
            var generator = new GraphBasedGeneratorGrid2D<RoomBase>(levelDescriptionGrid2D, configuration);
            generator.InjectRandomGenerator(Payload.Random);

            #if UNITY_WEBGL
            var layout = generator.GenerateLayout();

            if (layout == null)
            {
                throw new TimeoutException();
            }
            #else
            // Run the generator in a different thread so that the computation is not blocking
            LayoutGrid2D<RoomBase> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());

            while (!task.IsCompleted)
            {
                yield return null;
            }

            // Throw an exception when a timeout is reached
            // TODO: this should be our own exception and not a generic exception
            if (layout == null)
            {
                if (task.Exception != null)
                {
                    if (task.Exception.InnerException != null)
                    {
                        throw task.Exception.InnerException;
                    }

                    throw task.Exception;
                }
                else
                {
                    throw new TimeoutException();
                }
            }

            #endif

            // Transform the level to its Unity representation
            var generatedLevel = GeneratorUtilsGrid2D.TransformLayout(layout, levelDescription, rootGameObject);

            var stats = new GeneratorStats()
            {
                Iterations = generator.IterationsCount,
                TimeTotal = generator.TimeTotal,
            };

            Debug.Log($"Layout generated in {stats.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{stats.Iterations} iterations needed, {stats.Iterations / (stats.TimeTotal / 1000d):0} iterations per second");

            Payload.GeneratedLevel = generatedLevel;
            Payload.GeneratorStats = stats;

            yield return null;
        }
    }
}