using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Ramparts input setup", fileName = "Dead Cells Ramparts Input Setup")]
    public class DeadCellsRampartsInputSetup : PlatformerGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public GameObject[] EntranceRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        public GameObject[] OutsideNormalRoomTemplates;

        public GameObject[] OutsideTeleportRoomTemplates;

        public GameObject[] InsideTreasureRoomTemplates;

        public GameObject[] InsideNormalRoomTemplates;

        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            foreach (var room in LevelGraph.Rooms.Cast<DeadCellsRoom>())
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            foreach (var connection in LevelGraph.Connections.Cast<DeadCellsConnection>())
            {
                var from = (DeadCellsRoom) connection.From;
                var to = (DeadCellsRoom) connection.To;

                if (!from.Outside || !to.Outside)
                {
                    var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                    corridorRoom.Type = DeadCellsRoomType.Corridor;

                    // TODO: all these ToList()s look weird
                    levelDescription.AddCorridorConnection(connection, CorridorRoomTemplates.ToList(), corridorRoom);
                } else
                {
                    levelDescription.AddConnection(connection);
                }
            }

            return levelDescription;
        }

        // TODO: all these ToList()s look weird
        private List<GameObject> GetRoomTemplates(DeadCellsRoom room)
        {
            switch (room.Type)
            {
                case DeadCellsRoomType.Entrance:
                    return EntranceRoomTemplates.ToList();

                case DeadCellsRoomType.Exit:
                    return ExitRoomTemplates.ToList();

                case DeadCellsRoomType.Teleport when room.Outside:
                    return OutsideTeleportRoomTemplates.ToList();

                case DeadCellsRoomType.Treasure when !room.Outside:
                    return InsideTreasureRoomTemplates.ToList();

                case DeadCellsRoomType.Normal when !room.Outside:
                    return InsideNormalRoomTemplates.ToList();

                case DeadCellsRoomType.Normal when room.Outside:
                    return OutsideNormalRoomTemplates.ToList();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}