using System.Collections.Generic;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph
{
    [CreateAssetMenu(fileName = "RoomTemplatesSet", menuName = "Dungeon generator/Room templates set")]
    public class RoomTemplatesSet : ScriptableObject
    {
        public List<GameObject> RoomTemplates = new List<GameObject>();
    }
}