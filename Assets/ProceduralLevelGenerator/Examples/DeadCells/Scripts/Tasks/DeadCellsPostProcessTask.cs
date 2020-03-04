using System;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Post process", fileName = "Dead Cells Post Process")]
    public class DeadCellsPostProcessTask : PlatformerGeneratorPostProcessBase
    {
        public bool SpawnEnemies = false;
        public GameObject[] Enemies;

        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            SetSpawnPosition(level);

            if (SpawnEnemies)
            {
                DoSpawnEnemies(level);
            }
        }

        private void DoSpawnEnemies(GeneratedLevel level)
        {
            if (Enemies == null || Enemies.Length == 0)
            {
                throw new InvalidOperationException("There must be at least one enemy prefab to spawn enemies");
            }

            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplate = roomInstance.RoomTemplateInstance;
                var enemySpawnPoints = roomTemplate.transform.Find("EnemySpawnPoints");

                if (enemySpawnPoints != null)
                {
                    foreach (Transform enemySpawnPoint in enemySpawnPoints)
                    {
                        var enemyPrefab = Enemies[Random.Next(Enemies.Length)];
                        var enemy = Instantiate(enemyPrefab);
                        enemy.transform.parent = roomTemplate.transform;
                        enemy.transform.position = enemySpawnPoint.position;
                    }
                }
            }
        }

        private void SetSpawnPosition(GeneratedLevel level)
        {
            var entranceRoomInstance = level
                .GetRoomInstances()
                .FirstOrDefault(x => ((DeadCellsRoom) x.Room).Type == DeadCellsRoomType.Entrance);

            if (entranceRoomInstance == null)
            {
                throw new InvalidOperationException("Could not find Entrance room");
            }

            var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
            var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
            var player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPosition.position;
        }
    }
}