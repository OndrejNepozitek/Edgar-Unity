using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class SimpleDoorModeSettings
    {
        [Min(1)]
        public int Length = 1;

        [Min(0)]
        public int Padding1 = 1;

        [Min(0)]
        public int Padding2 = 1;
    }
}