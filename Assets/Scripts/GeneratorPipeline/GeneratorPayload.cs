namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class GeneratorPayload : IGeneratorPayload
	{
		public GameObject GameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

		public List<IMarkerMap> MarkerMaps { get; set; }
	}
}