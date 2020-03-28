using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Utils;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common
{
    public abstract class LevelGeneratorBase<TPayload> : MonoBehaviour, ILevelGenerator
    {
        private readonly Random seedsGenerator = new Random();

        protected readonly PipelineRunner PipelineRunner = new PipelineRunner();

        protected virtual Random GetRandomNumbersGenerator(bool useRandomSeed, int seed)
        {
            if (useRandomSeed)
            {
                seed = seedsGenerator.Next();
            }

            Debug.Log($"Random generator seed: {seed}");

            return new Random(seed);
        }

        public abstract object Generate();

        protected abstract TPayload InitializePayload();

        protected void ExportMapDescription(PipelineItem inputSetup)
        {
            var payload = InitializePayload();

            if (payload is IGraphBasedGeneratorPayload graphBasedGeneratorPayload)
            {
                var pipelineItems = new List<PipelineItem> {inputSetup};

                PipelineRunner.Run(pipelineItems, graphBasedGeneratorPayload);

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
    }
}