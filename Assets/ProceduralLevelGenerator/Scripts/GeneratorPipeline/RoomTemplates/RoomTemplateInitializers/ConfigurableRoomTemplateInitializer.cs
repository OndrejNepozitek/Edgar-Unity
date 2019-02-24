namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
	using System;
	using TilemapLayers;

	/// <summary>
	/// Room template initializer that can be customized with different
	/// implementaion of tilemap layers handlers.
	/// </summary>
	public class ConfigurableRoomTemplateInitializer : BaseRoomTemplateInitializer
	{
		public AbstractTilemapLayersHandler TilemapLayersHandler;

		public void Initialize()
		{
			if (TilemapLayersHandler == null) 
				throw new ArgumentException($"{nameof(TilemapLayersHandler)} must not be null.");

			InitializeTilemaps(TilemapLayersHandler);

			InitializeDoors();

			// Destroy the initializer
			DestroyImmediate(this);
		}
	}
}