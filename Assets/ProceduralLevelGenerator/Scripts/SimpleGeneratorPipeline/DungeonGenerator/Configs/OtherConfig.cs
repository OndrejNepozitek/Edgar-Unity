using System;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs
{
    [Serializable]
    public class OtherConfig
    {
        public bool UseRandomSeed = true;

        public int RandomGeneratorSeed;

        public bool PrintUsedSeed = true;
    }
}