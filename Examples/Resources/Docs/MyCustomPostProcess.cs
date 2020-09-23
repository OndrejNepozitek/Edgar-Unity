using Edgar.Unity.Generators.Common;
using Edgar.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

namespace Edgar.Unity.Examples.Resources.Docs
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