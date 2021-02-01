using System.Collections;

namespace Edgar.Unity
{
    /// <summary>
    ///     Base class for pipeline tasks. Used in simpler scenarios.
    /// </summary>
    /// <typeparam name="TPayload">Type of the payload</typeparam>
    public abstract class PipelineTask<TPayload> : IPipelineTask<TPayload>
        where TPayload : class
    {
        /// <summary>
        ///     Payload object.
        /// </summary>
        public TPayload Payload { get; set; }

        /// <summary>
        ///     Method containing all the logic of the task.
        /// </summary>
        /// <remarks>
        ///     When this method is called, the Payload property is already set.
        /// </remarks>
        public abstract IEnumerator Process();
    }
}