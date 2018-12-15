namespace Assets.Scripts.GeneratorPipeline
{
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public interface IGeneratorPayload
	{
		GameObject GameObject { get; set; }

		Tilemap Tilemap { get; set; }

		IMarkerMap MarkerMap { get; set; }
	}
}