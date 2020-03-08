using System;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils
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