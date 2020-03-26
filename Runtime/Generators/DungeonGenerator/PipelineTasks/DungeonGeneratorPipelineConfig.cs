using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Interfaces.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public class DungeonGeneratorPipelineConfig : PipelineConfig
    {
        public DungeonGeneratorConfig Config;
    }

    public class DungeonGeneratorPipelineTask<TPayload> : ConfigurablePipelineTask<TPayload, DungeonGeneratorPipelineConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    { 
        public override IEnumerator Process()
        {
            var config = Config.Config;
            var levelDescription = Payload.LevelDescription;

            if (config.Timeout <= 0)
            {
                throw new ArgumentException($"{nameof(config.Timeout)} must be greater than 0", nameof(config.Timeout));
            }

            var rootGameObject = config.RootGameObject;

            if (rootGameObject == null)
            {
                rootGameObject = GameObject.Find("Generated Level");

                if (rootGameObject == null)
                {
                    rootGameObject = new GameObject("Generated Level");
                }
            }

            foreach (var child in rootGameObject.transform.Cast<Transform>().ToList()) {
                child.transform.parent = null;
                PostProcessUtils.Destroy(child.gameObject);
            }

            var mapDescription = levelDescription.GetMapDescription();
            var configuration = new DungeonGeneratorConfiguration<Room>(mapDescription)
            {
                RoomsCanTouch = false,
                RepeatModeOverride = GeneratorUtils.GetRepeatMode(config.RepeatModeOverride),
                EarlyStopIfTimeExceeded = TimeSpan.FromMilliseconds(config.Timeout),
            };

            var generator = new DungeonGenerator<Room>(mapDescription, configuration);
            generator.InjectRandomGenerator(Payload.Random);

            IMapLayout<Room> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());

            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (layout == null)
            {
                throw new InvalidOperationException("Timeout was reached when generating level");
            }

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