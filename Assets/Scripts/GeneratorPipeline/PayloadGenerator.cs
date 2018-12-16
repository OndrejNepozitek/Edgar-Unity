namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu]
	public class PayloadGenerator : Pipeline.PayloadGenerator
	{
		public int NumberOfTilemaps = 5;

		public override object InitializePayload()
		{
			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var gridObject = new GameObject("Rooms holder");
			gridObject.AddComponent<Grid>();

			var tilemaps = new List<Tilemap>();

			for (int i = 0; i < NumberOfTilemaps; i++)
			{
				var tilemapObject = new GameObject($"Tilemap holder {i + 1}");
				tilemapObject.transform.SetParent(gridObject.transform);
				var tilemap = tilemapObject.AddComponent<Tilemap>();
				var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
				tilemapRenderer.sortingOrder = i;

				tilemaps.Add(tilemap);
			}

			var markerMaps = new List<IMarkerMap>();

			for (int i = 0; i < NumberOfTilemaps; i++)
			{
				markerMaps.Add(new MarkerMap());
			}

			return new GeneratorPayload()
			{
				MarkerMaps = markerMaps,
				Tilemaps = tilemaps
			};
		}
	}
}