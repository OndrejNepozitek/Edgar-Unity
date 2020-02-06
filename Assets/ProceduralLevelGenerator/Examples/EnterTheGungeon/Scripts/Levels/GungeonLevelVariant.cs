using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Level variant", fileName = "LevelVariant")]
    public class GungeonLevelVariant : ScriptableObject
    {
        public LevelGraph LevelGraph;
    }
}