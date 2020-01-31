using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Input", fileName = "Input")]
    public class InputConfig : PipelineConfig
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
    }

    public class InputTask<TPayload> : ConfigurablePipelineTask<TPayload, InputConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private readonly LevelDescription levelDescription = new LevelDescription();
        private List<GameObject> notUsedNormalRoomTemplates = new List<GameObject>();

        public override void Process()
        {
            foreach (var room in Config.LevelGraph.Rooms.Cast<GungeonRoom>())
            {
                levelDescription.AddRoom(room, GetRoomTemplates(room));
            }

            foreach (var connection in Config.LevelGraph.Connections.Cast<GungeonConnection>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<GungeonRoom>();
                corridorRoom.Type = RoomType.Corridor;

                // TODO: all these ToList()s look weird
                levelDescription.AddCorridorConnection(connection, Config.CorridorRoomTemplates.ToList(), corridorRoom);
            }

            Payload.LevelDescription = levelDescription;
        }

        // TODO: all these ToList()s look weird
        private List<GameObject> GetRoomTemplates(GungeonRoom room)
        {
            switch (room.Type)
            {
                case RoomType.Boss:
                    return Config.BossRoomTemplates.ToList();

                case RoomType.BossFoyers:
                    return Config.BossFoyersRoomTemplates.ToList();

                case RoomType.Shop:
                    return Config.ShopRoomTemplates.ToList();

                case RoomType.Reward:
                    return Config.RewardRoomTemplates.ToList();

                case RoomType.Hub:
                    return Config.HubRoomTemplates.ToList();

                case RoomType.Entrance:
                    return Config.EntranceRoomTemplates.ToList();

                case RoomType.Exit:
                    return Config.ExitRoomTemplates.ToList();

                case RoomType.Normal:
                    return GetNormalRoomTemplates();

                default:
                    return Config.DefaultRoomTemplates.ToList();
            }
        }

        private List<GameObject> GetNormalRoomTemplates()
        {
            if (notUsedNormalRoomTemplates.Count == 0)
            {
                notUsedNormalRoomTemplates = Config.BasicRoomTemplates.Where(x => x != null).ToList();
            }

            var randomIndex = Payload.Random.Next(notUsedNormalRoomTemplates.Count);
            var roomTemplate = notUsedNormalRoomTemplates[randomIndex];
            notUsedNormalRoomTemplates.RemoveAt(randomIndex);

            return new List<GameObject> {roomTemplate};
        }
    }
}