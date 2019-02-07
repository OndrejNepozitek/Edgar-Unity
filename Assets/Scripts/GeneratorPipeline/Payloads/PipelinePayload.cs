namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using RoomTemplates;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class PipelinePayload : IGeneratorPayload, IGraphBasedGeneratorPayload, INamedTilemapsPayload
	{
		public GameObject GameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

		public MapDescription<Room> MapDescription { get; set; }

		public TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates { get; set; }

		public Dictionary<Room, RoomInfo<Room>> LayoutData { get; set; }

		public Tilemap WallsTilemap => Tilemaps[0];

		public Tilemap FloorTilemap => Tilemaps[1];

		public Tilemap CollideableTilemap => Tilemaps[2];
	}
}