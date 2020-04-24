using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Example1.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Example 1/Post process", fileName = "Example1PostProcess")]
    public class Example1PostProcess : DungeonGeneratorPostProcessBase
    {
        [Range(0, 1)]
        public float EnemySpawnChance = 0.5f;

        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        { 
            HandleEnemies(level);
        }

        private void HandleEnemies(GeneratedLevel level)
        {
            // Iterate through all the rooms
            foreach (var roomInstance in level.GetRoomInstances())
            {
                // Get the transform that holds all the enemies
                var enemiesHolder = roomInstance.RoomTemplateInstance.transform.Find("Enemies");

                // Skip this room if there are no enemies
                if (enemiesHolder == null)
                {
                    continue;
                }

                // Iterate through all enemies (children of the enemiesHolder)
                foreach (Transform enemyTransform in enemiesHolder)
                {
                    var enemy = enemyTransform.gameObject;

                    // Roll a dice and check whether to spawn this enemy or not
                    // Use the provided Random instance so that the whole generator uses the same seed and the results can be reproduced
                    if (Random.NextDouble() < EnemySpawnChance)
                    {
                        enemy.SetActive(true);
                    }
                    else
                    {
                        enemy.SetActive(false);
                    }
                }
            }
        }
    }
}