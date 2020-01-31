using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Ramparts Input", fileName = "InputTask")]
    public class RampartsInputConfig : PipelineConfig
    {
        public LevelGraph LevelGraph;

        public GameObject[] DefaultRoomTemplates;

        public GameObject[] TeleportRoomTemplates;

        public GameObject[] TreasureRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        public GameObject[] EntranceRoomTemplates;
    }

    public class RampartsInputTask<TPayload> : ConfigurablePipelineTask<TPayload, RampartsInputConfig>
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
                var from = (DeadCellsRoom) connection.From;
                var to = (DeadCellsRoom) connection.To;

                if (!from.Outside || !to.Outside)
                {
                    var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                    corridorRoom.Type = DeadCellsRoomType.Corridor;

                    // TODO: all these ToList()s look weird
                    levelDescription.AddCorridorConnection(connection, Config.CorridorRoomTemplates.ToList(), corridorRoom);
                } else
                {
                    levelDescription.AddConnection(connection);
                }
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

                case DeadCellsRoomType.Entrance:
                    return Config.EntranceRoomTemplates.ToList();

                default:
                    return Config.DefaultRoomTemplates.ToList();
            }
        }
    }
}