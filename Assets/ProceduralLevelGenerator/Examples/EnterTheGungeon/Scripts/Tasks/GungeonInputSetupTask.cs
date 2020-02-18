using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Tasks
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Input setup", fileName = "Gungeon Input Setup")]
    public class GungeonInputSetupTask : DungeonGeneratorInputBase
    {
        public LevelGraph LevelGraph;

        public GameObject[] BasicRoomTemplates;

        public GameObject[] BossFoyersRoomTemplates;

        public GameObject[] BossRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        public GameObject[] DefaultRoomTemplates;

        public GameObject[] EntranceRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] HubRoomTemplates;
        
        public GameObject[] RewardRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        private List<GameObject> notUsedNormalRoomTemplates = new List<GameObject>();

        protected override LevelDescription GetLevelDescription()
        {
            // TODO: why does this must be here?
            notUsedNormalRoomTemplates = BasicRoomTemplates.Where(x => x != null).ToList();

            var levelDescription = new LevelDescription();

            foreach (var room in LevelGraph.Rooms.Cast<GungeonRoom>())
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            foreach (var connection in LevelGraph.Connections.Cast<GungeonConnection>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<GungeonRoom>();
                corridorRoom.Type = GungeonRoomType.Corridor;

                // TODO: all these ToList()s look weird
                levelDescription.AddCorridorConnection(connection, CorridorRoomTemplates.ToList(), corridorRoom);
            }

            return levelDescription;
        }

        // TODO: all these ToList()s look weird
        private List<GameObject> GetRoomTemplates(GungeonRoom room)
        {
            switch (room.Type)
            {
                case GungeonRoomType.Boss:
                    return BossRoomTemplates.ToList();

                case GungeonRoomType.BossFoyers:
                    return BossFoyersRoomTemplates.ToList();

                case GungeonRoomType.Shop:
                    return ShopRoomTemplates.ToList();

                case GungeonRoomType.Reward:
                    return RewardRoomTemplates.ToList();

                case GungeonRoomType.Hub:
                    return HubRoomTemplates.ToList();

                case GungeonRoomType.Entrance:
                    return EntranceRoomTemplates.ToList();

                case GungeonRoomType.Exit:
                    return ExitRoomTemplates.ToList();

                case GungeonRoomType.Normal:
                    return GetNormalRoomTemplates();

                default:
                    return DefaultRoomTemplates.ToList();
            }
        }

        private List<GameObject> GetNormalRoomTemplates()
        {
            if (notUsedNormalRoomTemplates.Count == 0)
            {
                notUsedNormalRoomTemplates = BasicRoomTemplates.Where(x => x != null).ToList();
            }

            var randomIndex = Payload.Random.Next(notUsedNormalRoomTemplates.Count);
            var roomTemplate = notUsedNormalRoomTemplates[randomIndex];
            notUsedNormalRoomTemplates.RemoveAt(randomIndex);

            return new List<GameObject> {roomTemplate};
        }
    }
}