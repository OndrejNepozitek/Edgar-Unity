namespace Assets.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public class RoomTemplateInitializer : MonoBehaviour
	{
		public AbstractTilemapLayersHandler TilemapLayersHandler;

		public void Initialize()
		{
			if (TilemapLayersHandler == null) 
				throw new ArgumentException($"{nameof(TilemapLayersHandler)} must not be null.");

			// Remove all children game objects
			var children = new List<GameObject>();
			foreach (Transform child in transform)
			{
				children.Add(child.gameObject);
			}
			children.ForEach(DestroyImmediate);

			// Initialize tilemaps
			TilemapLayersHandler.InitializeTilemaps(gameObject);

			// Destroy the initializer
			DestroyImmediate(this);
		}
	}
}