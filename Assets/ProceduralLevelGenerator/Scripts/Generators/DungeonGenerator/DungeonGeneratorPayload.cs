using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator
{
    public class DungeonGeneratorPayload : IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        public GameObject RootGameObject { get; set; }

        public List<Tilemap> Tilemaps { get; set; }

        public LevelDescription LevelDescription { get; set; }

        public GeneratedLevel GeneratedLevel { get; set; }

        public int Iterations { get; set; }

        public double TimeTotal { get; set; }

        public Random Random { get; set; }
    }
}