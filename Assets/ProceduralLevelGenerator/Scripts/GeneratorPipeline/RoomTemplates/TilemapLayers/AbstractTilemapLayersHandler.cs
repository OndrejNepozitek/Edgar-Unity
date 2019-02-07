namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public abstract class AbstractTilemapLayersHandler : ScriptableObject, ITilemapLayersHandler
	{
		public abstract void InitializeTilemaps(GameObject gameObject);

		public abstract IEnumerable<Tilemap> GetTilemapsForShapeComputation(GameObject gameObject);
	}
}