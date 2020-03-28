using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Rooms;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils
{
    public class LevelInfo : MonoBehaviour
    {
        [ReadOnly]
        public List<RoomInstance> RoomInstances;
    }
}