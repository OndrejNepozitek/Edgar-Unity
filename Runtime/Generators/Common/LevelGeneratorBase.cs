using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Utils;
using Newtonsoft.Json;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Pipeline;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    public abstract class LevelGeneratorBase<TPayload> : VersionedMonoBehaviour, ILevelGenerator where TPayload : class
    {
        private readonly Random seedsGenerator = new Random();

        protected readonly PipelineRunner<TPayload> PipelineRunner = new PipelineRunner<TPayload>();

        protected virtual Random GetRandomNumbersGenerator(bool useRandomSeed, int seed)
        {
            if (useRandomSeed)
            {
                seed = seedsGenerator.Next();
            }

            Debug.Log($"Random generator seed: {seed}");

            return new Random(seed);
        }

        public virtual object Generate()
        {
            Debug.Log("--- Generator started ---");
             
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            PipelineRunner.Run(pipelineItems, payload);

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");

            return payload;
        }

        protected abstract (List<IPipelineTask<TPayload>> pipelineItems, TPayload payload) GetPipelineItemsAndPayload();

        protected abstract TPayload InitializePayload();

        protected void ExportMapDescription(PipelineTask<TPayload> inputSetup)
        {
            var payload = InitializePayload();

            if (payload is IGraphBasedGeneratorPayload graphBasedGeneratorPayload)
            {
                var pipelineItems = new List<PipelineTask<TPayload>> {inputSetup};

                PipelineRunner.Run(pipelineItems, payload);

                var levelDescription = graphBasedGeneratorPayload.LevelDescription;
                var mapDescription = levelDescription.GetMapDescription();
                var intMapDescription = GetIntMapDescription(mapDescription);
                var json = JsonConvert.SerializeObject(intMapDescription, Formatting.Indented, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    TypeNameHandling = TypeNameHandling.Auto,
                });

                var filename = "exportedMapDescription.json";
                File.WriteAllText(filename, json);
                Debug.Log($"Map description exported to {filename}");
            }
            else
            {
                throw new InvalidOperationException($"The payload must implement {nameof(IGraphBasedGeneratorPayload)} to export map descriptions");
            }
        }

        private MapDescription<int> GetIntMapDescription(MapDescription<Room> mapDescription)
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
    }
}