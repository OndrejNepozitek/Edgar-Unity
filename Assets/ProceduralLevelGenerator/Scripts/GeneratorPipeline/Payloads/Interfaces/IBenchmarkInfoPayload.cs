using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
    public interface IBenchmarkInfoPayload
    {
        IMapLayout<Room> GeneratedLayout { get; set; }

        int Iterations { get; set; }

        double TimeTotal { get; set; }
    }
}