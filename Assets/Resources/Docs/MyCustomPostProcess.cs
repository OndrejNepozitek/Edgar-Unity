using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.Resources.Docs
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