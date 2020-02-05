using System;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs
{
    [Serializable]
    public class FixedLevelGraphConfig
    {
        public LevelGraph LevelGraph;

        public bool UseCorridors;
    }
}