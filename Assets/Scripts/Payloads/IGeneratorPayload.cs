namespace Assets.Scripts.Payloads
{
	using System.Collections.Generic;
	using GeneratorPipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public interface IGeneratorPayload
	{
		GameObject GameObject { get; set; }

		List<Tilemap> Tilemaps { get; set; }

		List<IMarkerMap> MarkerMaps { get; set; }
	}
}