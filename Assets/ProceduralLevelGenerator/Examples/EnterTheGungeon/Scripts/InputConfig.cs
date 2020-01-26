using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Graphs;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapDescriptions;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Enter The Gungeon/Input", fileName = "Input")]
	public class InputConfig : PipelineConfig
    {
        public LevelGraph LevelGraph;

		public GameObject[] EntranceRoomTemplates;

		public GameObject[] BossRoomTemplates;

        public GameObject[] BossFoyersRoomTemplates;

		public GameObject[] BasicRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        public GameObject[] RewardRoomTemplates;

        public GameObject[] HubRoomTemplates;

        public GameObject[] DefaultRoomTemplates;

		/// <summary>
		/// Room templates for corridors.
		/// </summary>
		public GameObject[] CorridorRoomTemplates;
    }

	public class InputTask<TPayload> : InputSetupBaseTask<TPayload, InputConfig>
		where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IRoomToIntMappingPayload<Room>
	{
		protected TwoWayDictionary<Room, int> RoomToIntMapping;

		protected IGraph<Room> LevelGraph;

		protected MapDescription<int> MapDescription;

        protected IRoomDescription CorridorRoomDescription;

        private List<GameObject> notUsedNormalRoomTemplates = new List<GameObject>();

		protected override MapDescription<int> SetupMapDescription()
		{
			RoomToIntMapping = new TwoWayDictionary<Room, int>();
			LevelGraph = new UndirectedAdjacencyListGraph<Room>();
			MapDescription = new MapDescription<int>();

            CorridorRoomDescription = GetCorridorRoomDescription();

			// Setup map description rooms and connections
			SetupMapDescriptionFromLevelGraph();

            Payload.RoomToIntMapping = RoomToIntMapping;

			return MapDescription;
		}

        protected void SetupMapDescriptionFromLevelGraph()
        {
            foreach (var room in Config.LevelGraph.Rooms.Cast<GungeonRoom>())
            {
                var intAlias = RoomToIntMapping.Count;
                RoomToIntMapping.Add(room, intAlias);

				MapDescription.AddRoom(intAlias, GetRoomDescription(room));
            }

            var corridorCounter = 0;
            foreach (var connection in Config.LevelGraph.Connections)
            {
                var corridorRoom = ScriptableObject.CreateInstance<GungeonRoom>();
                corridorRoom.Name = $"Corridor {corridorCounter++}";
                corridorRoom.Type = RoomType.Corridor;
                    
                var corridorRoomNumber = RoomToIntMapping.Count;
                RoomToIntMapping.Add(corridorRoom, corridorRoomNumber);

                var from = RoomToIntMapping[connection.From];
                var to = RoomToIntMapping[connection.To];

				MapDescription.AddRoom(corridorRoomNumber, CorridorRoomDescription);

				MapDescription.AddConnection(from, corridorRoomNumber);
				MapDescription.AddConnection(to, corridorRoomNumber);
            }
        }

        private BasicRoomDescription GetRoomDescription(GungeonRoom room)
        {
            switch (room.Type)
            {
                case RoomType.Boss:
                    return GetRoomDescription(Config.BossRoomTemplates);

                case RoomType.BossFoyers:
                    return GetRoomDescription(Config.BossFoyersRoomTemplates);

                case RoomType.Shop:
                    return GetRoomDescription(Config.ShopRoomTemplates);

                case RoomType.Reward:
                    return GetRoomDescription(Config.RewardRoomTemplates);

                case RoomType.Hub:
                    return GetRoomDescription(Config.HubRoomTemplates);

                case RoomType.Entrance:
                    return GetRoomDescription(Config.EntranceRoomTemplates);

                case RoomType.Normal:
                    return GetNormalRoomDescription();

                default:
                    return GetRoomDescription(Config.DefaultRoomTemplates);
            }
        }

        private BasicRoomDescription GetNormalRoomDescription()
        {
            if (notUsedNormalRoomTemplates.Count == 0)
            {
                notUsedNormalRoomTemplates = Config.BasicRoomTemplates.Where(x => x != null).ToList();
            }

            var randomIndex = Payload.Random.Next(notUsedNormalRoomTemplates.Count);
            var roomTemplate = notUsedNormalRoomTemplates[randomIndex];
            notUsedNormalRoomTemplates.RemoveAt(randomIndex);

            return new BasicRoomDescription(new List<IRoomTemplate>() { GetRoomTemplate(roomTemplate) });
        }

        private BasicRoomDescription GetRoomDescription(GameObject[] roomTemplates)
        {
            return new BasicRoomDescription(roomTemplates.Select(GetRoomTemplate).ToList());
        }

        protected CorridorRoomDescription GetCorridorRoomDescription()
		{
			var roomTemplates = Config
				.CorridorRoomTemplates
				.Where(x => x != null)
				.Select(GetRoomTemplate)
				.ToList();
            return new CorridorRoomDescription(roomTemplates);
		}
	}
}