namespace Assets.Scripts.Templating
{
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu]
	public class BasicTemplator : PipelineTask
	{
		public TileBase WallTile;

		public TileBase FloorTile;
	}

	[PipelineTaskFor(typeof(BasicTemplator))]
	public class BasicTemplator<T> : IConfigurablePipelineTask<T, BasicTemplator>
		where T : IGeneratorPayload
	{
		public BasicTemplator Config { get; set; }

		public void Process(T payload)
		{
			foreach (var position in payload.MarkerMap.Bounds.allPositionsWithin)
			{
				var marker = payload.MarkerMap.GetMarker(position);

				if (marker != null)
				{
					if (marker.Type == MarkerType.Wall)
					{
						payload.Tilemap.SetTile(position, Config.WallTile);
					}
					else if (marker.Type == MarkerType.Floor)
					{
						payload.Tilemap.SetTile(position, Config.FloorTile);
					}
				}
			}
		}
	}
}