namespace Assets.Scripts.Payloads
{
	using System.Collections.Generic;
	using GeneratorPipeline;
	using RoomTemplates;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class GeneratorPayload : IGeneratorPayload, IRoomInfoPayload
	{
		public GameObject GameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

		public List<IMarkerMap> MarkerMaps { get; set; }

		public List<RoomInfo<int>> RoomInfos { get; set; }
	}
}