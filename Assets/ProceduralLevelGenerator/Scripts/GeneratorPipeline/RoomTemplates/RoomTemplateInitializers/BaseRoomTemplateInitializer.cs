using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
    /// <summary>
    ///     Base class for initializing room templates.
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
            if (gameObject.GetComponent<Doors.Doors>() == null)
            {
                gameObject.AddComponent<Doors.Doors>();
            }
        }
    }
}