using System;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    public class GeneratorException : Exception
    {
        public GeneratorException()
        {
        }

        public GeneratorException(string message) : base(message)
        {
        }
    }
}