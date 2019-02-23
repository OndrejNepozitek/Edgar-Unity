namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
	using UnityEngine;

	public abstract class AbstractTilemapLayersHandler : ScriptableObject, ITilemapLayersHandler
	{
		/// <inheritdoc />
		public abstract void InitializeTilemaps(GameObject gameObject);
	}
}