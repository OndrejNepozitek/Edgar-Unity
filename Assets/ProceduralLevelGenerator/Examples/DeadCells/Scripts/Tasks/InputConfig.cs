using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Input", fileName = "InputTask")]
    public class InputConfig : PipelineConfig
    {
        public LevelGraph LevelGraph;

        public GameObject[] DefaultRoomTemplates;

        public GameObject[] TeleportRoomTemplates;

        public GameObject[] TreasureRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;
    }

    public class InputTask<TPayload> : ConfigurablePipelineTask<TPayload, InputConfig> 
        where TPayload : class, IGraphBasedGeneratorPayload
    {
        public override void Process()
        {
            var levelDescription = new LevelDescription();

            foreach (var room in Config.LevelGraph.Rooms.Cast<DeadCellsRoom>())
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            foreach (var connection in Config.LevelGraph.Connections.Cast<DeadCellsConnection>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                corridorRoom.Type = DeadCellsRoomType.Corridor;

                // TODO: all these ToList()s look weird
                levelDescription.AddCorridorConnection(connection, Config.CorridorRoomTemplates.ToList(), corridorRoom);
            }

            Payload.LevelDescription = levelDescription;
        }

        // TODO: all these ToList()s look weird
        private List<GameObject> GetRoomTemplates(DeadCellsRoom room)
        {
            switch (room.Type)
            {
                case DeadCellsRoomType.Teleport:
                    return Config.TeleportRoomTemplates.ToList();

                case DeadCellsRoomType.Treasure:
                    return Config.TreasureRoomTemplates.ToList();

                case DeadCellsRoomType.CursedTreasure:
                    return Config.TreasureRoomTemplates.ToList();

                case DeadCellsRoomType.Exit:
                    return Config.ExitRoomTemplates.ToList();

                default:
                    return Config.DefaultRoomTemplates.ToList();
            }
        }
    }
}