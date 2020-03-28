using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Payloads.PayloadInitializers
{
    /// <summary>
    ///     Class that is able to initializer a pipeline payload.
    /// </summary>
    public abstract class AbstractPayloadInitializer : ScriptableObject
    {
        /// <summary>
        ///     Returns a new instance of a pipeline payload.
        /// </summary>
        /// <returns></returns>
        public abstract object InitializePayload();
    }
}