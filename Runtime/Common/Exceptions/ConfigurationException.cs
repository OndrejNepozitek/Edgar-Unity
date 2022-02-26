namespace Edgar.Unity
{
    /// <summary>
    /// This exception should be used when there is a problem with the configuration of the generator.
    /// (As opposed to getting a timeout error due to a difficult input.)
    /// </summary>
    public class ConfigurationException : GeneratorException
    {
        public ConfigurationException(string message) : base(message)
        {
            /* empty */
        }
    }
}