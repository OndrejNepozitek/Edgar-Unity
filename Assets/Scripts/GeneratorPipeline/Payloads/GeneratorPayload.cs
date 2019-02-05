namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneratorPipeline;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using Markers;
	using RoomTemplates;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class GeneratorPayload : IGeneratorPayload, IRoomInfoPayload, IGraphBasedInputPayload
	{
		public GameObject GameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

		public List<IMarkerMap> MarkerMaps { get; set; }

		public MapDescription<Room> MapDescription { get; set; }

		public TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates { get; set; }

		public List<RoomInfo<Room>> RoomInfos { get; set; }
	}
}