using System;
using System.Linq;
using System.Threading.Tasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Logic
{
    public class GraphBasedDungeonGenerator
    {
        public (GeneratedLevel, GeneratorStats) Generate(LevelDescription levelDescription, Random random, DungeonGeneratorConfig config)
        {
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
            };

            var generator = new DungeonGenerator<Room>(mapDescription, configuration);
            generator.InjectRandomGenerator(random);

            IMapLayout<Room> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());
            task.Wait(config.Timeout);

            if (layout == null)
            {
                throw new InvalidOperationException("Timeout was reached when generating level");
            }

            var generatedLevel = GeneratorUtils.TransformLayout(layout, levelDescription, rootGameObject);

            return (generatedLevel, new GeneratorStats()
            {
                Iterations = generator.IterationsCount,
                TimeTotal = generator.TimeTotal,
            });
        }
    }
}