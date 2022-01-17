using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Edgar.Unity
{
    [Serializable]
    public class SimpleDoorModeSettingsGrid2D
    {
        [Min(1)]
        public int Length = 1;

        [Min(0)]
        [FormerlySerializedAs("Padding1")]
        public int Margin1 = 1;

        [Min(0)]
        [FormerlySerializedAs("Padding2")]
        public int Margin2 = 1;

        public bool Enabled = true;
    }
}