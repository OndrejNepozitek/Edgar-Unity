namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Markers;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public interface IGeneratorPayload
	{
		GameObject GameObject { get; set; }

		List<Tilemap> Tilemaps { get; set; }

		List<IMarkerMap> MarkerMaps { get; set; }
	}
}