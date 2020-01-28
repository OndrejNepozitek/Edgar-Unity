using System;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
    public class DungeonGeneratorException : Exception
    {
        public DungeonGeneratorException()
        {
        }

        public DungeonGeneratorException(string message) : base(message)
        {
        }
    }
}