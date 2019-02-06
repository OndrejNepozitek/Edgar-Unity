namespace Assets.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public interface ITilemapLayersHandler
	{
		void InitializeTilemaps(GameObject gameObject);

		IEnumerable<Tilemap> GetTilemapsForShapeComputation(GameObject gameObject);
	}
}