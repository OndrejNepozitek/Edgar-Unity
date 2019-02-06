namespace Assets.Scripts.GeneratorPipeline
{
	using System.Collections.Generic;
	using System.Linq;
	using Markers;
	using Payloads;
	using RoomTemplates.TilemapLayers;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Payload generator")]
	public class PayloadGenerator : AbstractPayloadGenerator
	{
		public AbstractTilemapLayersHandler TilemapLayersHandler;

		public override object InitializePayload()
		{
			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var gridObject = new GameObject("Rooms holder");
			gridObject.AddComponent<Grid>();

			TilemapLayersHandler.InitializeTilemaps(gridObject);

			var markerMaps = new List<IMarkerMap>();

			// TODO: change
			for (int i = 0; i < gridObject.GetComponentsInChildren<Tilemap>().Length; i++)
			{
				markerMaps.Add(new MarkerMap());
			}

			return new GeneratorPayload()
			{
				MarkerMaps = markerMaps,
				Tilemaps = gridObject.GetComponentsInChildren<Tilemap>().ToList(),
				GameObject = gridObject
			};
		}
	}
}