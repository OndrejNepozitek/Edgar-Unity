namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Markers
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	[Obsolete("Marker maps should not be used.")]
	public class MarkerMap : IMarkerMap
	{
		protected readonly Dictionary<Vector3Int, Marker> Markers = new Dictionary<Vector3Int, Marker>();

		private BoundsInt bounds;

		public BoundsInt Bounds
		{
			get { return bounds; }
			set { bounds = value; }
		}

		public Marker GetMarker(Vector3Int position)
		{
			Marker marker;

			if (Markers.TryGetValue(position, out marker))
			{
				return marker;
			}

			return null;
		}

		public void SetMarker(Vector3Int position, Marker marker)
		{
			Markers[position] = marker;

			if (!Bounds.Contains(position))
			{
				var min = new Vector3Int(Math.Min(Bounds.xMin, position.x) - 1, Math.Min(Bounds.yMin, position.y) - 1, 0);
				var max = new Vector3Int(Math.Max(Bounds.xMax, position.x + 1), Math.Max(Bounds.yMax, position.y) + 1, 1);

				bounds.SetMinMax(min, max);
			}
		}
	}
}