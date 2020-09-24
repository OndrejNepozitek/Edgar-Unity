using System.Collections;

namespace Edgar.Unity
{
    public interface IPipelineTask<TPayload> where TPayload : class
    {
        /// <summary>
        ///     Payload object.
        /// </summary>
        TPayload Payload { get; set; }

        /// <summary>
        ///     Method containing all the logic of the task.
        /// </summary>
        /// <remarks>
        ///     When this method is called, the Payload property is already set.
        /// </remarks>
        IEnumerator Process();
    }
}