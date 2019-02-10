namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
	using System;
	using TilemapLayers;

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