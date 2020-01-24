using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Level variant", fileName = "LevelVariant")]
    public class GungeonLevelVariant : ScriptableObject
    {
        public LevelGraph LevelGraph;
    }
}