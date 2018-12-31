namespace Assets.Scripts.Templating
{
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline tasks/Doors templating")]
	public class DoorsTemplating : PipelineTask
	{
		public TileBase HorizontalDoors;

		public TileBase VerticalDoors;
	}

	[PipelineTaskFor(typeof(DoorsTemplating))]
	public class DoorsTemplating<T> : IConfigurablePipelineTask<T, DoorsTemplating>
		where T : IGeneratorPayload
	{
		public DoorsTemplating Config { get; set; }

		public void Process(T payload)
		{
			var wallsMarkerMap = payload.MarkerMaps[0];
			var doorsMarkerMap = payload.MarkerMaps[1];
			var tilemap = payload.Tilemaps[1];

			foreach (var position in doorsMarkerMap.Bounds.allPositionsWithin)
			{
				var marker = doorsMarkerMap.GetMarker(position);

				if (marker?.Type == MarkerTypes.Door)
				{
					var leftMarker = wallsMarkerMap.GetMarker(position + Vector3Int.left);
					var rightMarker = wallsMarkerMap.GetMarker(position + Vector3Int.right);
					var upMarker = wallsMarkerMap.GetMarker(position + Vector3Int.up);
					var downMarker = wallsMarkerMap.GetMarker(position + Vector3Int.down);

					if (leftMarker?.Type == MarkerTypes.Wall && rightMarker?.Type == MarkerTypes.Wall)
					{
						tilemap.SetTile(position, Config.HorizontalDoors);
					}
					else if (upMarker?.Type == MarkerTypes.Wall && downMarker?.Type == MarkerTypes.Wall)
					{
						tilemap.SetTile(position, Config.VerticalDoors);
					}
				}
			}
		}
	}
}