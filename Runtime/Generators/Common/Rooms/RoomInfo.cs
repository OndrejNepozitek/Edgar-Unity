using Edgar.Unity.Attributes;
using UnityEngine;

namespace Edgar.Unity.Generators.Common.Rooms
{
    /// <summary>
    /// Component that holds information about corresponding room instance.
    /// </summary>
    public class RoomInfo : MonoBehaviour
    {
        [ReadOnly]
        public RoomInstance RoomInstance;
    }
}