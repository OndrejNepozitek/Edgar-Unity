using System;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs
{
    [Obsolete]
    [Serializable]
    public class OtherConfig
    {
        /// <summary>
        /// Whether to use a random seed.
        /// </summary>
        public bool UseRandomSeed = true;

        /// <summary>
        /// Which seed should be used for the random numbers generator.
        /// Is used only when UseRandomSeed is false.
        /// </summary>
        public int RandomGeneratorSeed;

        /// <summary>
        /// Whether to generate a level on enter play mode.
        /// </summary>
        public bool GenerateOnStart = true;
    }
}