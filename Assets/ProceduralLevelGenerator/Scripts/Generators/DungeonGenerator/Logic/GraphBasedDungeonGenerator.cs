using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using Object = UnityEngine.Object;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;
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
            var generator = new DungeonGenerator<Room>(mapDescription, new DungeonGeneratorConfiguration<Room>(mapDescription) {RoomsCanTouch = false});
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