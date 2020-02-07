using System;
using System.Linq;
using System.Threading.Tasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Core.LayoutGenerators.PlatformersGenerator;
using MapGeneration.Core.LayoutOperations;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using Object = System.Object;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Logic
{
    public class PlatformerGenerator
    {
        public (GeneratedLevel, GeneratorStats) Generate(LevelDescription levelDescription, Random random, PlatformerGeneratorConfig config)
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

            // TODO: destroy or destroy immediate?
            foreach (var child in rootGameObject.transform.Cast<Transform>().ToList()) {
                UnityEngine.Object.DestroyImmediate(child.gameObject);
            }

            var mapDescription = levelDescription.GetMapDescription();
            var generator = new PlatformersGenerator<Room>(mapDescription, new DungeonGeneratorConfiguration<Room>(mapDescription)
            {
                RoomsCanTouch = false,
                RoomShapesRepeatingConfig = GetRoomShapesRepeatingConfig(config.RepeatMode)
            });
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

        private RoomShapesRepeatingConfig GetRoomShapesRepeatingConfig(RepeatMode repeatMode)
        {
            switch (repeatMode)
            {
                case RepeatMode.Allow:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.Any,
                        ThrowIfNotSatisfied = false,
                    };

                case RepeatMode.NeighborsDifferent:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.NoNeighborsRepeats,
                        ThrowIfNotSatisfied = false,
                    };

                case RepeatMode.AllRoomsDifferent:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.NoRepeats,
                        ThrowIfNotSatisfied = false,
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}