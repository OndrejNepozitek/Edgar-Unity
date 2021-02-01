namespace Edgar.Unity
{
    /// <summary>
    /// Interface that represents a level generator.
    /// </summary>
    public interface ILevelGenerator
    {
        /// <summary>
        /// Generates and returns a level.
        /// </summary>
        object Generate();
    }
}