namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using Payloads;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Payload generator")]
	public class PayloadGenerator : AbstractPayloadGenerator
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
			//var tilemapInfos = new List<TilemapInfo>()
			//{
			//	new TilemapInfo("Walls", 0),
			//	new TilemapInfo("Floor", 0),
			//	new TilemapInfo()
			//};

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
				Tilemaps = tilemaps,
				GameObject = gridObject
			};
		}

		private class TilemapInfo
		{
			public string Name { get; set; }

			public int SortingOrder { get; set; }

			public TilemapInfo(string name, int sortingOrder)
			{
				Name = name;
				SortingOrder = sortingOrder;
			}
		}
	}
}