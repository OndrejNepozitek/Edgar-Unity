using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    /// <summary>
    /// The actual generator logic that call the .NET generator.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class DungeonGeneratorTask<TPayload> : PipelineTask<TPayload>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        private readonly DungeonGeneratorConfig config;

        public DungeonGeneratorTask(DungeonGeneratorConfig config)
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
            foreach (var child in rootGameObject.transform.Cast<Transform>().ToList()) {
                child.transform.parent = null;
                PostProcessUtils.Destroy(child.gameObject);
            }

            // The LevelDescription class must be converted to MapDescription
            var mapDescription = levelDescription.GetMapDescription();
            var configuration = new DungeonGeneratorConfiguration<RoomBase>()
            {
                RoomsCanTouch = false,
                RepeatModeOverride = GeneratorUtils.GetRepeatMode(config.RepeatModeOverride),
                EarlyStopIfTimeExceeded = TimeSpan.FromMilliseconds(config.Timeout),
            };

            // We create the instance of the dungeon generator and inject the correct Random instance
            var generator = new DungeonGenerator<RoomBase>(mapDescription, configuration);
            generator.InjectRandomGenerator(Payload.Random);

            // Run the generator in a different class so that the computation is not blocking
            MapLayout<RoomBase> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());

            while (!task.IsCompleted)
            {
                yield return null;
            }

            // Throw an exception when a timeout is reached
            // TODO: this should be our own exception and not a generic exception
            if (layout == null)
            {
                throw new InvalidOperationException("Timeout was reached when generating level");
            }

            // Transform the level to its Unity representation
            var generatedLevel = GeneratorUtils.TransformLayout(layout, levelDescription, rootGameObject); 

            var stats = new GeneratorStats()
            {
                Iterations = generator.IterationsCount,
                TimeTotal = generator.TimeTotal,
            };

            Debug.Log($"Layout generated in {stats.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{stats.Iterations} iterations needed, {stats.Iterations / (stats.TimeTotal / 1000d):0} iterations per second");

            ((IGraphBasedGeneratorPayload) Payload).GeneratedLevel = generatedLevel;
            Payload.GeneratorStats = stats;

            yield return null;
        }
    }
}