using ProceduralLevelGenerator.Unity.Attributes;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Rooms
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