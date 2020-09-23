using System.Collections.Generic;
using Edgar.Unity.Attributes;
using Edgar.Unity.Generators.Common.Rooms;
using UnityEngine;

namespace Edgar.Unity.Generators.Common.Utils
{
    public class LevelInfo : MonoBehaviour
    {
        [ReadOnly]
        public List<RoomInstance> RoomInstances;
    }
}