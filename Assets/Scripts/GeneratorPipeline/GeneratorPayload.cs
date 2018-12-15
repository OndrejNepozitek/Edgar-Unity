namespace Assets.Scripts.GeneratorPipeline
{
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class GeneratorPayload : IGeneratorPayload
	{
		public GameObject GameObject { get; set; }

		public Tilemap Tilemap { get; set; }

		public IMarkerMap MarkerMap { get; set; }
	}
}