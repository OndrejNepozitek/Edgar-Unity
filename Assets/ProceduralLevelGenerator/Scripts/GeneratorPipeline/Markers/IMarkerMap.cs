namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Markers
{
	using System;
	using UnityEngine;

	[Obsolete("Marker maps should not be used.")]
	public interface IMarkerMap
	{
		BoundsInt Bounds { get; set; }

		Marker GetMarker(Vector3Int position);

		void SetMarker(Vector3Int position, Marker marker);
	}
}