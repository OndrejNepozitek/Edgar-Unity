namespace ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces
{
    /// <summary>
    ///     Represents data used in and produced from a graph-based dungeon generator.
    /// </summary>
    public interface IGraphBasedGeneratorPayload
    {
        /// <summary>
        ///     Abstract description of the level that is used as input for the generator.
        /// </summary>
        LevelDescription LevelDescription { get; set; }

        /// <summary>
        ///     Representation of the generated level.
        ///     Each room from the level description now has a concrete position and room template assigned.
        /// </summary>
        GeneratedLevel GeneratedLevel { get; set; }
    }
}