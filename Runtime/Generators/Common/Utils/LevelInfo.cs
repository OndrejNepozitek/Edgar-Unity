using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    public class LevelInfo : MonoBehaviour
    {
        [ReadOnly]
        public List<RoomInstance> RoomInstances;
    }
}