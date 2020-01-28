using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads
{
    /// <summary>
    ///     Default pipeline payload.
    /// </summary>
    /// <typeparam name="TRoom"></typeparam>
    public class PipelinePayload<TRoom> : IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        public IMapLayout<Room> GeneratedLayout { get; set; }

        public int Iterations { get; set; }

        public double TimeTotal { get; set; }
        public GameObject ParentGameObject { get; set; }

        public List<Tilemap> Tilemaps { get; set; }

        public LevelDescription LevelDescription { get; set; }

        public GeneratedLevel GeneratedLevel { get; set; }

        public Random Random { get; set; }
    }
}