using System;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Platformer.Pipeline_tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Platformer/Doors task", fileName = "DoorsTask")]
    public class DoorsConfig : PipelineConfig
    {
    }

    public class DoorsTask<TPayload> : ConfigurablePipelineTask<TPayload, DoorsConfig>
        where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        public override void Process()
        {
            var wallTilemap = Payload.Tilemaps.SingleOrDefault(x => x.name == "Walls");

            if (wallTilemap == null)
            {
                throw new InvalidOperationException("Tilemap named \"Walls\" not found");
            }

            // Iterate through all rooms
            foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
            {
                // Iterate through all used door positions
                foreach (var doorInfo in roomInstance.Doors)
                {
                    foreach (var point in doorInfo.DoorLine.GetPoints())
                    {
                        wallTilemap.SetTile(point, null);
                    }
                }
            }
        }
    }
}