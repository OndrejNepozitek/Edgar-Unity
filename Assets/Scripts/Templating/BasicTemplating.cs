namespace Assets.Scripts.Templating
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Pipeline tasks/Basic templating")]
	public class BasicTemplating : PipelineTask
	{
		public List<TileMapping> Mapping = new List<TileMapping>();

		[Serializable]
		public class TileMapping
		{
			public MarkerType MarkerType;

			public TileBase Tile;
		}
	}

	[PipelineTaskFor(typeof(BasicTemplating))]
	public class BasicTemplating<T> : IConfigurablePipelineTask<T, BasicTemplating>
		where T : IGeneratorPayload
	{
		public BasicTemplating Config { get; set; }

		public void Process(T payload)
		{
			for (int i = 0; i < payload.MarkerMaps.Count; i++)
			{
				var markerMap = payload.MarkerMaps[i];
				var tilemap = payload.Tilemaps[i];

				foreach (var position in markerMap.Bounds.allPositionsWithin)
				{
					var marker = markerMap.GetMarker(position);

					if (marker != null)
					{
						var correspondingTile = Config.Mapping.FirstOrDefault(x => x.MarkerType == marker.Type);

						if (correspondingTile != null)
						{
							tilemap.SetTile(position, correspondingTile.Tile);
						}
					}
				}
			}
		}
	}
}