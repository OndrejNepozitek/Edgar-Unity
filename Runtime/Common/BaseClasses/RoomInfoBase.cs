using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// This component contains information about a single room in the generated level.
    /// </summary>
    /// <remarks>
    /// The component is attached to the root game object of the corresponding room template instance.
    /// Room template game objects are places inside the root game object of the generated level.
    ///
    /// Example usage:
    /// After a level is generated, you can get the RoomInstance of the corresponding room template.
    /// After a level is generated, you can peek the data assigned to the room template, e.g. its position.
    /// </remarks>
    /// <typeparam name="TRoomInstance"></typeparam>
    public abstract class RoomInfoBase<TRoomInstance> : MonoBehaviour
    {
        [ReadOnly]
        public TRoomInstance RoomInstance;
    }
}