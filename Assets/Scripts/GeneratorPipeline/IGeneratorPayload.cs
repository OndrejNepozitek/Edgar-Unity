namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public interface IGeneratorPayload
	{
		GameObject GameObject { get; set; }

		List<Tilemap> Tilemaps { get; set; }

		List<IMarkerMap> MarkerMaps { get; set; }
	}
}