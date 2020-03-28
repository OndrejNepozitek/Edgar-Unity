using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Resources.Docs
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