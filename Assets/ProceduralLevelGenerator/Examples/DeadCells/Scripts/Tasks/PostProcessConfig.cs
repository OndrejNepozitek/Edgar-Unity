using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    public class PostProcessConfig : PipelineConfig
    {

    }

    public class PostProcessTask<TPayload> : ConfigurablePipelineTask<TPayload, PostProcessConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IGeneratorPayload
    {
        public override void Process()
        {
            foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
            {
                var room = roomInstance.Room as DeadCellsRoom;

                if (room.Type == DeadCellsRoomType.Entrance)
                {
                    var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                    var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
                    var player = GameObject.FindWithTag("Player");
                    player.transform.position = spawnPosition.position;
                }
            }
        }
    }
}