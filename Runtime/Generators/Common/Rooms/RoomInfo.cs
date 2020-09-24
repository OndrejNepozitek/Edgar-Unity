using UnityEngine;

namespace Edgar.Unity
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