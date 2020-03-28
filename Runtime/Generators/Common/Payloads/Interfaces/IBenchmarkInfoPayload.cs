using ProceduralLevelGenerator.Unity.Generators.Common.Utils;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces
{
    public interface IBenchmarkInfoPayload
    {
        GeneratedLevel GeneratedLevel { get; set; }

        int Iterations { get; set; }

        double TimeTotal { get; set; }

        GeneratorStats GeneratorStats { get; set; }
    }
}