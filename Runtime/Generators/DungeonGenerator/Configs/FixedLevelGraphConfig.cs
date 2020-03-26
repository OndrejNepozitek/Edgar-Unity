using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class FixedLevelGraphConfig
    {
        public LevelGraph LevelGraph;

        public bool UseCorridors = true;
    }
}