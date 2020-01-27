using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
    public interface IBenchmarkInfoPayload
    {
        GeneratedLevel GeneratedLevel { get; set; }

        int Iterations { get; set; }

        double TimeTotal { get; set; }
    }
}