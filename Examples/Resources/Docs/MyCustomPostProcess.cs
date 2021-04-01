using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
    public class MyCustomPostProcess : DungeonGeneratorPostProcessBaseGrid2D
    {
        public override void Run(GeneratedLevelGrid2D level, LevelDescriptionGrid2D levelDescription)
        { 
            // Implement the logic here
        }
    }
}