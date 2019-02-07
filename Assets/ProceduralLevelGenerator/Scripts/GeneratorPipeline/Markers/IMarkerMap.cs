namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Markers
{
	using UnityEngine;

	public interface IMarkerMap
	{
		BoundsInt Bounds { get; set; }

		Marker GetMarker(Vector3Int position);

		void SetMarker(Vector3Int position, Marker marker);
	}
}