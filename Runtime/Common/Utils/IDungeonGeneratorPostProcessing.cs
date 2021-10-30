using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Common interface for post-processing logic.
    /// </summary>
    /// <remarks>
    /// This interface is needed because post-processing logic can be implemented as a ScriptableObject or a MonoBehaviour.
    /// </remarks>
    /// <typeparam name="TLevel"></typeparam>
    public interface IDungeonGeneratorPostProcessing<in TLevel>
    {
        /// <summary>
        /// Instance of a random numbers generator that is used throughout the whole process of generating a level.
        /// </summary>
        /// <remarks>
        /// Use this instance in your post-processing tasks if you want to get reproducible results.
        /// </remarks>
        Random Random { get; }

        /// <summary>
        /// Runs the post-processing logic with a given generated level.
        /// </summary>
        void Run(TLevel level);

        void SetRandomGenerator(Random random);
    }
}