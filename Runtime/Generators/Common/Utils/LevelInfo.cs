using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Attributes;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    public class LevelInfo : MonoBehaviour
    {
        [ReadOnly]
        public List<RoomInstance> RoomInstances;
    }
}