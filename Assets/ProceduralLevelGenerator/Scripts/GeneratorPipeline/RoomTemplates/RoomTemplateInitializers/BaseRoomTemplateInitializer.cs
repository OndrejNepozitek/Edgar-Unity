namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
	using System.Collections.Generic;
	using Doors;
	using TilemapLayers;
	using UnityEngine;

	/// <summary>
	/// Base class for initializing room templates.
	/// </summary>
	public abstract class BaseRoomTemplateInitializer : MonoBehaviour
	{
		protected void InitializeTilemaps(ITilemapLayersHandler tilemapLayersHandler)
		{
			// Add Doors component
			if (gameObject.GetComponent<Grid>() == null)
			{
				gameObject.AddComponent<Grid>();
			}

			// Remove all children game objects
			var children = new List<GameObject>();
			foreach (Transform child in transform)
			{
				children.Add(child.gameObject);
			}
			children.ForEach(DestroyImmediate);

			// Initialize tilemaps
			tilemapLayersHandler.InitializeTilemaps(gameObject);
		}

		protected void InitializeDoors()
		{
			// Add Doors component
			if (gameObject.GetComponent<Doors>() == null)
			{
				gameObject.AddComponent<Doors>();
			}
		}
	}
}