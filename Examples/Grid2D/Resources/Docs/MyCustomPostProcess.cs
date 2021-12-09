using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
    public class MyCustomPostProcess : DungeonGeneratorPostProcessingGrid2D
    {
        public override void Run(DungeonGeneratorLevelGrid2D level)
        { 
            // Implement the logic here
        }
    }
}