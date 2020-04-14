using System.Collections.Generic;
using MapGeneration.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Payloads
{
    /// <summary>
    ///     Default pipeline payload.
    /// </summary>
    /// <typeparam name="TRoom"></typeparam>
    public class PipelinePayload<TRoom> : IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        public MapLayout<Room> GeneratedLayout { get; set; }

        public int Iterations { get; set; }

        public double TimeTotal { get; set; }

        public GeneratorStats GeneratorStats { get; set; }

        public GameObject RootGameObject { get; set; }

        public List<Tilemap> Tilemaps { get; set; }

        public LevelDescription LevelDescription { get; set; }

        public GeneratedLevel GeneratedLevel { get; set; }

        public Random Random { get; set; }
    }
}