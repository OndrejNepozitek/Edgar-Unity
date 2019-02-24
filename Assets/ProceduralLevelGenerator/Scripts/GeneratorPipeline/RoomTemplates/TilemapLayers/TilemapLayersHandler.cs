namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	/// <summary>
	/// Basic implementation of tilemap layers handler.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline/Tilemap layers handler", fileName = "TilemapLayersHandler")]
	public class TilemapLayersHandler : AbstractTilemapLayersHandler
	{
		/// <summary>
		/// Initializes individual tilemap layers.
		/// </summary>
		/// <param name="gameObject"></param>
		public override void InitializeTilemaps(GameObject gameObject)
		{
			var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 0);
			AddCompositeCollider(wallsTilemapObject);

			var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject, 1);

			var collideableTilemapObject = CreateTilemapGameObject("Collideable", gameObject, 2);
			AddCompositeCollider(collideableTilemapObject);

			var other1TilemapObject = CreateTilemapGameObject("Other 1", gameObject, 3);

			var other2TilemapObject = CreateTilemapGameObject("Other 2", gameObject, 4);

			var other3TilemapObject = CreateTilemapGameObject("Other 3", gameObject, 5);
		}

		protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
		{
			var tilemapObject = new GameObject(name);
			tilemapObject.transform.SetParent(parentObject.transform);
			var tilemap = tilemapObject.AddComponent<Tilemap>();
			var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
			tilemapRenderer.sortingOrder = sortingOrder;

			return tilemapObject;
		}

		protected void AddCompositeCollider(GameObject gameObject)
		{
			var tilemapCollider2D = gameObject.AddComponent<TilemapCollider2D>();
			tilemapCollider2D.usedByComposite = true;

			gameObject.AddComponent<CompositeCollider2D>();
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
	}
}