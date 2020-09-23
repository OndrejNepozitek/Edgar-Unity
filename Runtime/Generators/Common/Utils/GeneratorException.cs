using System;

namespace Edgar.Unity.Generators.Common.Utils
{
    /// <summary>
    /// Exception that is used inside the generator.
    /// </summary>
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