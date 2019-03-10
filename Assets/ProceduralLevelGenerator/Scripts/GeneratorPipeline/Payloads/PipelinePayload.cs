namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Data.Graphs;
	using DungeonGenerators;
	using GeneralAlgorithms.DataStructures.Common;
	using Interfaces;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using RoomTemplates;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Random = System.Random;

	/// <summary>
	/// Default pipeline payload.
	/// </summary>
	/// <typeparam name="TRoom"></typeparam>
	public class PipelinePayload<TRoom> : IGeneratorPayload, IGraphBasedGeneratorPayload, INamedTilemapsPayload, IRandomGeneratorPayload, IRoomToIntMappingPayload<TRoom>
	{
		public GameObject GameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

		public MapDescription<int> MapDescription { get; set; }

		public TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates { get; set; }

		public Layout<int> Layout { get; set; }

		public Tilemap WallsTilemap => Tilemaps[0];

		public Tilemap FloorTilemap => Tilemaps[1];

		public Tilemap CollideableTilemap => Tilemaps[2];

		public Random Random { get; set; }

		public TwoWayDictionary<TRoom, int> RoomToIntMapping { get; set; }
	}
}