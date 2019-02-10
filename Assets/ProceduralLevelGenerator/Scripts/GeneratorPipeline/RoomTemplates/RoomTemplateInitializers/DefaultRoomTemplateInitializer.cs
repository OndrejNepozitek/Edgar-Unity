namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
	using TilemapLayers;
	using UnityEditor;

	public class DefaultRoomTemplateInitializer : BaseRoomTemplateInitializer
	{
		public void Initialize()
		{
			var tilemapLayersHandler = AssetDatabase
				.LoadAssetAtPath<TilemapLayersHandler>("Assets/ProceduralLevelGenerator/ScriptableObjects/DefaultTilemapLayersHandler.asset");

			InitializeTilemaps(tilemapLayersHandler);

			InitializeDoors();

			// Destroy the initializer
			DestroyImmediate(this);
		}
	}
}