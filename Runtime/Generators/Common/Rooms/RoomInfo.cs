using ProceduralLevelGenerator.Unity.Attributes;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Rooms
{
    public class RoomInfo : MonoBehaviour
    {
        [ReadOnly]
        public RoomInstance RoomInstance;
    }
}