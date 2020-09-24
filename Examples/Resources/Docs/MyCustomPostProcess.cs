using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
    public class MyCustomPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        { 
            // Implement the logic here
        }
    }
}