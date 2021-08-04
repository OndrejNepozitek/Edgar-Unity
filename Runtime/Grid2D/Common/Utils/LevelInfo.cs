using System;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    [Obsolete("Please use LevelInfoGrid2D instead.")]
    public abstract class LevelInfo : MonoBehaviour
    {
        [ReadOnly]
        public List<RoomInstanceGrid2D> RoomInstances;
    }
}