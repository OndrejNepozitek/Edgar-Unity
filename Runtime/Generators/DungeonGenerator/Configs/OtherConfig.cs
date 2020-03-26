using System;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class OtherConfig
    {
        public bool UseRandomSeed = true;

        public int RandomGeneratorSeed;

        public bool GenerateOnStart = true;
    }
}