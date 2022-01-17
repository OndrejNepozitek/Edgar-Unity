using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// This component contains information about a generated level.
    /// </summary>
    /// <remarks>
    /// The component is attached to the root game object of the generated level after it is generated.
    ///
    /// Example usage:
    /// After a level is generated and you need to get all the rooms in the level, you can access this component to get the information.
    /// </remarks>
    /// <typeparam name="TRoomInstance"></typeparam>
    public abstract class LevelInfoBase<TRoomInstance> : MonoBehaviour
    {
        /// <summary>
        /// Room instances of all the rooms in the level.
        /// </summary>
        [ReadOnly]
        public List<TRoomInstance> RoomInstances;
    }
}