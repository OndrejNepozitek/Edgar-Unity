namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
	using TilemapLayers;
	using UnityEditor;
	using UnityEngine;

	public class DefaultRoomTemplateInitializer : BaseRoomTemplateInitializer
	{
		public void Initialize()
		{
			var tilemapLayersHandler = AssetDatabase
				.LoadAssetAtPath<TilemapLayersHandler>("Assets/ProceduralLevelGenerator/ScriptableObjects/DefaultTilemapLayersHandler.asset");

			gameObject.transform.position = Vector3.zero;

			InitializeTilemaps(tilemapLayersHandler);

			InitializeDoors();

			// Destroy the initializer
			DestroyImmediate(this);
		}
	}
}