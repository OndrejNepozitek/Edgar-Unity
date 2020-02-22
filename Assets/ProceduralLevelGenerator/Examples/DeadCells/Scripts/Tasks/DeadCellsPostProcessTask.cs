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
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            SetSpawnPosition(level);
        }

        private void SetSpawnPosition(GeneratedLevel level)
        {
            var entranceRoomInstance = level
                .GetAllRoomInstances()
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