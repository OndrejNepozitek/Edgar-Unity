using System;

namespace Edgar.Unity
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