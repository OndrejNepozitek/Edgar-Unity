using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Edgar.Unity
{
    /// <summary>
    ///     Default pipeline payload.
    /// </summary>
    /// <typeparam name="TRoom"></typeparam>
    public class PipelinePayload<TRoom> : IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        public LayoutGrid2D<Room> GeneratedLayout { get; set; }

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