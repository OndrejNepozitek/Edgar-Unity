namespace Edgar.Unity
{
    /// <summary>
    /// This exception is used when the outline of a room template is not valid.
    /// </summary>
    public class InvalidOutlineException : GeneratorException
    {
        public InvalidOutlineException(string message) : base(message)
        {
        }
    }
}