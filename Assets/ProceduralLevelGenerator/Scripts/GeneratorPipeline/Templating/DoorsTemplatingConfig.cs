namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Templating
{
	using Payloads;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline tasks/Doors templating")]
	public class DoorsTemplatingConfig : PipelineConfig
	{
		public TileBase HorizontalDoors;

		public TileBase VerticalDoorsBottom;

		public TileBase VerticalDoorsUpper;
	}

	public class DoorsTemplatingTask<TPayload> : ConfigurablePipelineTask<TPayload, DoorsTemplatingConfig>
		where TPayload : class, IGeneratorPayload
	{
		public override void Process()
		{
			// TODO:
			//var wallsMarkerMap = Payload.MarkerMaps[0];
			//var doorsMarkerMap = Payload.MarkerMaps[1];
			//var tilemap = Payload.Tilemaps[1];

			//foreach (var position in doorsMarkerMap.Bounds.allPositionsWithin)
			//{
			//	var marker = doorsMarkerMap.GetMarker(position);

			//	if (marker?.Type == MarkerTypes.Door)
			//	{
			//		var leftMarker = wallsMarkerMap.GetMarker(position + Vector3Int.left);
			//		var rightMarker = wallsMarkerMap.GetMarker(position + Vector3Int.right);
			//		var upMarker = wallsMarkerMap.GetMarker(position + Vector3Int.up);
			//		var downMarker = wallsMarkerMap.GetMarker(position + Vector3Int.down);

			//		if (leftMarker?.Type == MarkerTypes.Wall && rightMarker?.Type == MarkerTypes.Wall)
			//		{
			//			tilemap.SetTile(position, Config.HorizontalDoors);
			//		}
			//		else if (upMarker?.Type == MarkerTypes.Wall && downMarker?.Type == MarkerTypes.Wall)
			//		{
			//			tilemap.SetTile(position, Config.VerticalDoorsBottom);
			//			tilemap.SetTile(position + Vector3Int.up, Config.VerticalDoorsUpper);
			//		}
			//	}
			//}
		}
	}
}