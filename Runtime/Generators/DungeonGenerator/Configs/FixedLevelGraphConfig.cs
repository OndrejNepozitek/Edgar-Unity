using System;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class FixedLevelGraphConfig
    {
        public LevelGraph LevelGraph;

        public bool UseCorridors = true;
    }
}