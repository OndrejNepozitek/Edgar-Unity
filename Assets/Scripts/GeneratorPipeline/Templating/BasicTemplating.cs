namespace Assets.Scripts.GeneratorPipeline.Templating
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneratorPipeline;
	using Markers;
	using Payloads;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline tasks/Basic templating")]
	public class BasicTemplating : PipelineTask<IGeneratorPayload>
	{
		public List<TileMapping> Mapping = new List<TileMapping>();

		[Serializable]
		public class TileMapping
		{
			public MarkerType MarkerType;

			public TileBase Tile;
		}

		public override void Process()
		{

			// TODO:
			//for (int i = 0; i < Payload.MarkerMaps.Count; i++)
			//{
			//	var markerMap = Payload.MarkerMaps[i];
			//	var tilemap = Payload.Tilemaps[i];

			//	foreach (var position in markerMap.Bounds.allPositionsWithin)
			//	{
			//		var marker = markerMap.GetMarker(position);

			//		if (marker != null)
			//		{
			//			var correspondingTile = Mapping.FirstOrDefault(x => x.MarkerType == marker.Type);

			//			if (correspondingTile != null)
			//			{
			//				tilemap.SetTile(position, correspondingTile.Tile);
			//			}
			//		}
			//	}
			//}
		}
	}
}