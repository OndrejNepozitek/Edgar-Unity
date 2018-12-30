namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using Payloads;
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