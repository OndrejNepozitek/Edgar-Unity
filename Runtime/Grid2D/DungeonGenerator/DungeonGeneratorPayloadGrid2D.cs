using Random = System.Random;

namespace Edgar.Unity
{
    /// <summary>
    /// Payload that is used to transfer data between individual stages of the generator.
    /// </summary>
    public class DungeonGeneratorPayloadGrid2D
    {
        public LevelDescriptionGrid2D LevelDescription { get; set; }

        public DungeonGeneratorLevelGrid2D GeneratedLevel { get; set; }

        public Random Random { get; set; }

        public GeneratorStats GeneratorStats { get; set; }

        public DungeonGeneratorBaseGrid2D DungeonGenerator { get; set; }
    }
}