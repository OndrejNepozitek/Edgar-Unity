using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Dead Cells/Input setup", fileName = "Dead Cells Input Setup")]
    public class DeadCellsInputSetupTask : PlatformerGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public GameObject[] DefaultRoomTemplates;

        public GameObject[] TeleportRoomTemplates;

        public GameObject[] TreasureRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        protected override LevelDescription GetLevelDescription()
        {
            var levelDescription = new LevelDescription();

            foreach (var room in LevelGraph.Rooms.Cast<DeadCellsRoom>())
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            foreach (var connection in LevelGraph.Connections.Cast<DeadCellsConnection>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<DeadCellsRoom>();
                corridorRoom.Type = DeadCellsRoomType.Corridor;

                // TODO: all these ToList()s look weird
                levelDescription.AddCorridorConnection(connection, CorridorRoomTemplates.ToList(), corridorRoom);
            }

            return levelDescription;
        }

        // TODO: all these ToList()s look weird
        private List<GameObject> GetRoomTemplates(DeadCellsRoom room)
        {
            switch (room.Type)
            {
                case DeadCellsRoomType.Teleport:
                    return TeleportRoomTemplates.ToList();

                case DeadCellsRoomType.Treasure:
                    return TreasureRoomTemplates.ToList();

                case DeadCellsRoomType.CursedTreasure:
                    return TreasureRoomTemplates.ToList();

                case DeadCellsRoomType.Exit:
                    return ExitRoomTemplates.ToList();

                default:
                    return DefaultRoomTemplates.ToList();
            }
        }
    }
}