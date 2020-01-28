using System;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.RoomTemplateInitializers
{
    /// <summary>
    ///     Room template initializer that can be customized with different
    ///     implementaion of tilemap layers handlers.
    /// </summary>
    public class ConfigurableRoomTemplateInitializer : BaseRoomTemplateInitializer
    {
        public AbstractTilemapLayersHandler TilemapLayersHandler;

        public void Initialize()
        {
            if (TilemapLayersHandler == null)
                throw new ArgumentException($"{nameof(TilemapLayersHandler)} must not be null.");

            gameObject.transform.position = Vector3.zero;

            InitializeTilemaps(TilemapLayersHandler);

            InitializeDoors();

            // Destroy the initializer
            DestroyImmediate(this);
        }
    }
}