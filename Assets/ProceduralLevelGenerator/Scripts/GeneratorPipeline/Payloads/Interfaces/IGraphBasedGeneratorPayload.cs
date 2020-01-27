using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Interfaces.Core.MapLayouts;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
    using DungeonGenerators;

    /// <summary>
	/// Represents data used in and produced from a graph-based dungeon generator.
	/// </summary>
    public interface IGraphBasedGeneratorPayload
    {
		LevelDescription LevelDescription { get; set; }

        GeneratedLevel GeneratedLevel { get; set; }

		IMapLayout<Room> GeneratedLayout { get; set; }
    }
}