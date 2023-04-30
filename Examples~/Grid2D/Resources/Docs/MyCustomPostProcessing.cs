using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    #region codeBlock:2d_customPostProcessing

    [CreateAssetMenu(menuName = "Edgar/Examples/Docs/My custom post-processing", fileName = "MyCustomPostProcessing")]
    public class MyCustomPostProcessing : DungeonGeneratorPostProcessingGrid2D
    {
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            // Implement the logic here
        }
    }

    #endregion

    #region codeBlock:2d_customPostProcessingComponent

    public class MyCustomPostProcessingComponent : DungeonGeneratorPostProcessingComponentGrid2D
    {
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            // Implement the logic here
        }
    }

    #endregion
}