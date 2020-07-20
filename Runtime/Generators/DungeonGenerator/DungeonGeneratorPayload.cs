using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    /// <summary>
    /// Payload that is used to transfer data between individual stages of the generator.
    /// </summary>
    public class DungeonGeneratorPayload : IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
    {
        public LevelDescription LevelDescription { get; set; }

        public GeneratedLevel GeneratedLevel { get; set; }

        public Random Random { get; set; }

        public int Iterations { get; set; }

        public double TimeTotal { get; set; }

        public GeneratorStats GeneratorStats { get; set; }
    }
}