namespace Assets.Scripts.GeneratorPipeline
{
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu]
	public class PayloadGenerator : Pipeline.PayloadGenerator
	{
		public override object InitializePayload()
		{
			var gameHolderOld = GameObject.Find("Rooms holder");

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var gridObject = new GameObject("Rooms holder");
			gridObject.AddComponent<Grid>();
			var tilemapObject = new GameObject("Tilemap holder");
			tilemapObject.transform.SetParent(gridObject.transform);
			var commonTilemap = tilemapObject.AddComponent<Tilemap>();
			tilemapObject.AddComponent<TilemapRenderer>();

			return new GeneratorPayload()
			{
				MarkerMap = new MarkerMap(),
				Tilemap = commonTilemap
			};
		}
	}
}