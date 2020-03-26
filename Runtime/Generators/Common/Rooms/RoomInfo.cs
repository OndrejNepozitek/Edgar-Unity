using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Rooms
{
    public class RoomInfo : MonoBehaviour
    {
        [ReadOnly]
        public RoomInstance RoomInstance;
    }
}