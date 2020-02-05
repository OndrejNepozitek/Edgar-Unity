using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using GeneralAlgorithms.DataStructures.Common;
using GeneralAlgorithms.DataStructures.Graphs;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapDescriptions;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.ProceduralLevelGraphs.Scripts
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Procedural level graphs/Procedural input", fileName = "ProceduralInput")]
    public class ProceduralInputConfig : PipelineConfig
    {
        /// <summary>
        ///     Whether to add redundant rooms.
        /// </summary>
        public bool AddRedundantRooms;

        /// <summary>
        ///     Whether to add a shortcut.
        /// </summary>
        public bool AddShortcuts;

        /// <summary>
        ///     Room templates for basic rooms.
        /// </summary>
        public GameObject[] BasicRoomTemplates;

        /// <summary>
        ///     Room templates used for the boss room.
        /// </summary>
        public GameObject[] BossRoomTemplates;

        /// <summary>
        ///     Room templates for corridors.
        /// </summary>
        public GameObject[] CorridorRoomTemplates;

        /// <summary>
        ///     Maximum length of the main path.
        /// </summary>
        public int MaxLength = 15;

        /// <summary>
        ///     Minimum lenght of the main path.
        /// </summary>
        public int MinLength = 10;

        /// <summary>
        ///     What is the chance of adding a redundant room.
        /// </summary>
        [Range(0f, 1f)]
        public float RedundantRoomChance = 0.5f;

        /// <summary>
        ///     Room templates used for the spawn room.
        /// </summary>
        public GameObject[] SpawnRoomTemplates;

        /// <summary>
        ///     Whether to use corridors;
        /// </summary>
        public bool UseCorridors;
    }

    public class ProceduralInputTask<TPayload> : ConfigurablePipelineTask<TPayload, ProceduralInputConfig>
        where TPayload : class, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        protected IRoomDescription BasicRoomDescription;
        protected IRoomDescription BossRoomDescription;
        protected IRoomDescription CorridorRoomDescription;

        protected IGraph<Room> LevelGraph;

        protected MapDescription<int> MapDescription;
        protected TwoWayDictionary<Room, int> RoomToIntMapping;
        protected IRoomDescription SpawnRoomDescription;

        public override void Process()
        {
            throw new NotImplementedException();
        }

        // TODO: fix this
        //protected override MapDescription<int> SetupMapDescription()
        //{
        //	RoomToIntMapping = new TwoWayDictionary<Room, int>();
        //	LevelGraph = new UndirectedAdjacencyListGraph<Room>();
        //	MapDescription = new MapDescription<int>();

        //          BasicRoomDescription = GetBasicRoomDescription();
        //          BossRoomDescription = GetBossRoomDescription();
        //          SpawnRoomDescription = GetSpawnRoomDescription();
        //          CorridorRoomDescription = GetCorridorRoomDescription();

        //          // Get random length of the main path
        //	var mainPathLength = Payload.Random.Next(Config.MinLength, Config.MaxLength);

        //	// Add vertices on the main path
        //	var mainPath = SetupMainPath(mainPathLength);

        //	// Add shortcut
        //	if (Config.AddShortcuts)
        //	{
        //		SetupShortcut(mainPath);
        //	}

        //	// Add redundant rooms
        //	if (Config.AddRedundantRooms)
        //	{
        //		SetupRedundantRooms();
        //	}

        //	// Setup map description rooms and connections
        //	SetupMapDescriptionFromLevelGraph();

        //          Payload.RoomToIntMapping = RoomToIntMapping;

        //	return MapDescription;
        //}

        //protected List<Room> SetupMainPath(int length)
        //{
        //	var mainPath = new List<Room>();

        //	// Prepare rooms on the main path
        //	mainPath.Add(new Room() { Type = RoomType.Spawn });

        //	for (int i = 0; i < length - 2; i++)
        //	{
        //		mainPath.Add(new Room() { Type = RoomType.Basic });
        //	}

        //	mainPath.Add(new Room() { Type = RoomType.Boss });

        //	// Add vertices to levelGraph
        //	foreach (var room in mainPath)
        //	{
        //		LevelGraph.AddVertex(room);
        //	}

        //	// Add edges to levelGraph
        //	for (int i = 0; i < mainPath.Count - 1; i++)
        //	{
        //		LevelGraph.AddEdge(mainPath[i], mainPath[i + 1]);
        //	}

        //	return mainPath;
        //}

        ///// <summary>
        ///// Setups redundant rooms.
        ///// </summary>
        ///// <remarks>
        ///// Goes through all basic and shortcut rooms and with a predefined
        ///// chance connects one or two redundant rooms.
        ///// 
        ///// To make it easier for the generator:
        ///// - consider only rooms with at most 2 neighbours
        ///// - consider only rooms whose neighbours do not have any redundant rooms connected
        ///// </remarks>
        //protected void SetupRedundantRooms()
        //{
        //	var basicRooms = LevelGraph.Vertices.Where(x => x.Type == RoomType.Basic || x.Type == RoomType.Shortcut).ToList();

        //	foreach (var room in basicRooms)
        //	{
        //		if (Payload.Random.NextDouble() < Config.RedundantRoomChance 
        //		    && LevelGraph.GetNeighbours(room).Count() < 3
        //		    && !HasNeigbourWithRedundantRoom(room))
        //		{
        //			var redundantRoom = new Room() { Type = RoomType.Redundant };
        //			LevelGraph.AddVertex(redundantRoom);
        //			LevelGraph.AddEdge(room, redundantRoom);

        //			if (Payload.Random.NextDouble() < Config.RedundantRoomChance / 2)
        //			{
        //				var redundantRoom2 = new Room() { Type = RoomType.Redundant };
        //				LevelGraph.AddVertex(redundantRoom2);
        //				LevelGraph.AddEdge(redundantRoom, redundantRoom2);
        //			}
        //		}
        //	}
        //}

        ///// <summary>
        ///// Checks if any neighbours of a given room have redundant rooms connected to them.
        ///// </summary>
        ///// <param name="room"></param>
        ///// <returns></returns>
        //protected bool HasNeigbourWithRedundantRoom(Room room)
        //{
        //	return LevelGraph
        //		.GetNeighbours(room)
        //		.Any(x =>
        //			LevelGraph.GetNeighbours(x).Any(y => y.Type == RoomType.Redundant)
        //		);
        //}

        ///// <summary>
        ///// Setups a shortcut.
        ///// </summary>
        ///// <param name="mainPath"></param>
        //protected void SetupShortcut(List<Room> mainPath)
        //{
        //	// Compute start, end and lenght of a shortcut
        //	var startIndex = Payload.Random.Next(2, mainPath.Count - 5);
        //	var endIndex = startIndex + Payload.Random.Next(2, 4);
        //	var shortcutLength = Payload.Random.Next(2, 4);

        //	var startRoom = mainPath[startIndex];
        //	var endRoom = mainPath[endIndex];

        //	// Add shortcut rooms
        //	var shortcutRooms = new List<Room>();
        //	shortcutRooms.Add(startRoom);
        //	for (int i = 0; i < shortcutLength; i++)
        //	{
        //		var shortcutRoom = new Room() {Type = RoomType.Shortcut};
        //		shortcutRooms.Add(shortcutRoom);
        //		LevelGraph.AddVertex(shortcutRoom);
        //	}
        //	shortcutRooms.Add(endRoom);

        //	// Add connections
        //	for (int i = 0; i < shortcutRooms.Count - 1; i++)
        //	{
        //		LevelGraph.AddEdge(shortcutRooms[i], shortcutRooms[i + 1]);
        //	}
        //}

        ///// <summary>
        ///// Adds rooms and connections from the level graph to the map description.
        ///// </summary>
        ///// <returns></returns>
        //protected TwoWayDictionary<Room, int> SetupMapDescriptionFromLevelGraph()
        //{
        //	foreach (var room in LevelGraph.Vertices)
        //	{
        //		RoomToIntMapping.Add(room, RoomToIntMapping.Count);
        //		MapDescription.AddRoom(RoomToIntMapping[room], GetRoomDescription(room));
        //	}

        //	foreach (var edge in LevelGraph.Edges)
        //	{
        //		var from = RoomToIntMapping[edge.From];
        //		var to = RoomToIntMapping[edge.To];

        //              if (Config.UseCorridors)
        //              {
        //                  var corridorRoom = new Room() { Type = RoomType.Corridor };
        //                  var corridorRoomNumber = RoomToIntMapping.Count;
        //                  RoomToIntMapping[corridorRoom] = corridorRoomNumber;

        //                  MapDescription.AddRoom(corridorRoomNumber, GetRoomDescription(corridorRoom));
        //                  MapDescription.AddConnection(from, corridorRoomNumber);
        //                  MapDescription.AddConnection(to, corridorRoomNumber);
        //              }
        //              else
        //              {
        //                  MapDescription.AddConnection(from, to);
        //              }
        //	}

        //	return RoomToIntMapping;
        //}

        //      protected IRoomDescription GetRoomDescription(Room room)
        //      {
        //          switch (room.Type)
        //          {
        //              case RoomType.Boss:
        //                  return BossRoomDescription;

        //              case RoomType.Spawn:
        //                  return SpawnRoomDescription;

        //              case RoomType.Corridor:
        //                  return CorridorRoomDescription;

        //		default:
        //                  return BasicRoomDescription;
        //          }
        //      }

        //protected BasicRoomDescription GetBasicRoomDescription()
        //{
        //          var roomTemplates = Config
        //              .BasicRoomTemplates
        //              .Where(x => x != null)
        //              .Select(GetRoomTemplate)
        //              .ToList();
        //          return new BasicRoomDescription(roomTemplates);
        //}

        //protected BasicRoomDescription GetBossRoomDescription()
        //      {
        //          var roomTemplates = Config
        //              .BossRoomTemplates
        //              .Where(x => x != null)
        //              .Select(GetRoomTemplate)
        //              .ToList();
        //	return new BasicRoomDescription(roomTemplates);
        //      }

        //protected BasicRoomDescription GetSpawnRoomDescription()
        //{
        //          var roomTemplates = Config
        //              .SpawnRoomTemplates
        //              .Where(x => x != null)
        //              .Select(GetRoomTemplate)
        //              .ToList();
        //          return new BasicRoomDescription(roomTemplates);
        //}

        //protected CorridorRoomDescription GetCorridorRoomDescription()
        //{
        //	var roomTemplates = Config
        //		.CorridorRoomTemplates
        //		.Where(x => x != null)
        //		.Select(GetRoomTemplate)
        //		.ToList();
        //          return new CorridorRoomDescription(roomTemplates);
        //}
    }
}