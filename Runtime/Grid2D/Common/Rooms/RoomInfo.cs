using System;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Component that holds information about corresponding room instance.
    /// </summary>
    [Obsolete("Please use RoomInfoGrid2D instead.")]
    public abstract class RoomInfo : MonoBehaviour
    {
        [ReadOnly]
        public RoomInstanceGrid2D RoomInstance;
    }
}