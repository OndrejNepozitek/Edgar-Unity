namespace Assets.Scripts.GeneratorPipeline.Payloads.PayloadGenerators
{
	using System.Linq;
	using Payloads;
	using RoomTemplates.TilemapLayers;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Payload generator")]
	public class PayloadGenerator : AbstractPayloadGenerator
	{
		public AbstractTilemapLayersHandler TilemapLayersHandler;

		private static readonly string GameObjectName = "Generated dungeon";

		public override object InitializePayload()
		{
			var gameHolderOld = GameObject.Find(GameObjectName);

			if (gameHolderOld != null)
			{
				DestroyImmediate(gameHolderOld);
			}

			var gridObject = new GameObject(GameObjectName);
			gridObject.AddComponent<Grid>();

			TilemapLayersHandler.InitializeTilemaps(gridObject);

			return new PipelinePayload()
			{
				Tilemaps = gridObject.GetComponentsInChildren<Tilemap>().ToList(),
				GameObject = gridObject
			};
		}
	}
}