using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.GraphBasedGenerator;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Core.LayoutGenerators.PlatformersGenerator;
using MapGeneration.Core.LayoutOperations;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;
using MapGeneration.Utils;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.Platformers
{
    /// <summary>
    ///     Actual implementation of the task that generates platfomers.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public class PlatformerGeneratorTask<TPayload> : GraphBasedGeneratorBaseTask<TPayload, PlatformerGeneratorConfig>
        where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private readonly DungeonGeneratorUtils dungeonGeneratorUtils = new DungeonGeneratorUtils();

        public override void Process()
        {
            if (Config.Timeout <= 0)
            {
                throw new ArgumentException("Timeout must be a positive number.");
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (Config.ShowDebugInfo)
            {
                UnityEngine.Debug.Log("--- Generator started ---");
            }

            // Setup map description
            var mapDescription = Payload.LevelDescription.GetMapDescription();

            if (Config.ExportMapDescription)
            {
                ExportMapDescription(mapDescription);
            }

            // Generate layout
            var generator = GetGenerator(mapDescription);

            if (Config.UsePrecomputedLevelsOnly)
            {
                if (Config.PrecomputedLevelsHandler == null)
                {
                    throw new InvalidOperationException(
                        $"{nameof(Config.PrecomputedLevelsHandler)} must not be null when {nameof(Config.UsePrecomputedLevelsOnly)} is enabled");
                }

                Config.PrecomputedLevelsHandler.LoadLevel(Payload);
            }
            else
            {
                var layout = GenerateLayout(mapDescription, generator, Config.Timeout, Config.ShowDebugInfo);

                // Setup room templates
                Payload.GeneratedLevel = TransformLayout(layout, Payload.LevelDescription);
            }

            // TODO: How to handle timeout when benchmarking?
            if (Payload is IBenchmarkInfoPayload benchmarkInfoPayload)
            {
                benchmarkInfoPayload.TimeTotal = generator.TimeTotal;
                benchmarkInfoPayload.Iterations = generator.IterationsCount;
            }

            // Apply tempaltes
            if (Config.ApplyTemplate)
            {
                ApplyTemplates();
            }

            // Center grid
            if (Config.CenterGrid)
            {
                Payload.Tilemaps[0].CompressBounds();
                Payload.Tilemaps[0].transform.parent.position = -Payload.Tilemaps[0].cellBounds.center;
            }

            if (Config.ShowDebugInfo)
            {
                Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
            }
        }

        // TODO: remove later
        protected IMapLayout<Room> GenerateLayout(IMapDescription<Room> mapDescription, PlatformersGenerator<Room> generator, int timeout = 0,
            bool showDebugInfo = false)
        {
            IMapLayout<Room> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());

            if (timeout > 0)
            {
                var taskCompleted = task.Wait(timeout);

                if (!taskCompleted)
                {
                    throw new DungeonGeneratorException("Timeout was reached when generating the layout");
                }
            }

            if (showDebugInfo)
            {
                PrintGeneratorStats(generator);
            }

            return layout;
        }

        private RoomShapesRepeatingConfig GetRoomShapesRepeatingConfig()
        {
            switch (Config.ForceDifferentRoomTemplates)
            {
                case ForceDifferentRoomTemplates.DoNotCare:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.Any,
                        ThrowIfNotSatisfied = false,
                    };

                case ForceDifferentRoomTemplates.NeighborsDifferent:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.NoNeighborsRepeats,
                        ThrowIfNotSatisfied = false,
                    };

                case ForceDifferentRoomTemplates.AllRoomsDifferent:
                    return new RoomShapesRepeatingConfig()
                    {
                        Type = RoomShapesRepeating.NoRepeats,
                        ThrowIfNotSatisfied = false,
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // TODO: remove later
        protected void PrintGeneratorStats(PlatformersGenerator<Room> generator)
        {
            Debug.Log($"Layout generated in {generator.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{generator.IterationsCount} iterations needed, {generator.IterationsCount / (generator.TimeTotal / 1000d):0} iterations per second");
        }

        private void ExportMapDescription(IMapDescription<Room> mapDescription)
        {
            var intMapDescription = GetIntMapDescription(mapDescription);
            var json = JsonConvert.SerializeObject(intMapDescription, Formatting.Indented, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.Auto,
            });
            File.WriteAllText("exportedMapDescription.json", json);
        }

        private IMapDescription<int> GetIntMapDescription(IMapDescription<Room> mapDescription)
        {
            var newMapDescription = new MapDescription<int>();
            var mapping = mapDescription.GetGraph().Vertices.CreateIntMapping();

            foreach (var vertex in mapDescription.GetGraph().Vertices)
            {
                newMapDescription.AddRoom(mapping[vertex], mapDescription.GetRoomDescription(vertex));
            }

            foreach (var edge in mapDescription.GetGraph().Edges)
            {
                newMapDescription.AddConnection(mapping[edge.From], mapping[edge.To]);
            }

            return newMapDescription;
        }

        protected PlatformersGenerator<Room> GetGenerator(IMapDescription<Room> mapDescription)
        {
            var generator = new PlatformersGenerator<Room>(mapDescription, new DungeonGeneratorConfiguration<Room>(mapDescription)
            {
                RoomsCanTouch = false,
                RoomShapesRepeatingConfig = GetRoomShapesRepeatingConfig()
            });
            generator.InjectRandomGenerator(Payload.Random);

            return generator;
        }

        /// <summary>
        ///     Copies tiles from individual room templates to the tilemaps that hold generated dungeons.
        /// </summary>
        protected void ApplyTemplates()
        {
            var nonCorridors = Payload.GeneratedLevel.GetAllRoomInstances().Where(x => !x.IsCorridor).ToList();
            var corridors = Payload.GeneratedLevel.GetAllRoomInstances().Where(x => x.IsCorridor).ToList();

            dungeonGeneratorUtils.ApplyTemplates(nonCorridors, Payload.Tilemaps);
            dungeonGeneratorUtils.ApplyTemplates(corridors, Payload.Tilemaps);
        }
    }
}