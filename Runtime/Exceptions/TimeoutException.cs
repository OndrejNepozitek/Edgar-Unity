namespace Edgar.Unity
{
    public class TimeoutException : GeneratorException
    {
        public TimeoutException() : base($"The generator was not able to produce a level within a given time limit. Please see the console for additional diagnostic information.")
        {
            /* empty */
        }
    }
}