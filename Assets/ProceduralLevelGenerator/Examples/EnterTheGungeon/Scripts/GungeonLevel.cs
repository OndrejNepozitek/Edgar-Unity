using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Level", fileName = "Level")]
    public class GungeonLevel : ScriptableObject
    {
        public GungeonLevelVariant[] Variants;
    }
}