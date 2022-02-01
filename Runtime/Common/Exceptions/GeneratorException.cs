using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Exception that is used inside the generator.
    /// Ideally, all exception thrown inside the generator should inherit from this exception.
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