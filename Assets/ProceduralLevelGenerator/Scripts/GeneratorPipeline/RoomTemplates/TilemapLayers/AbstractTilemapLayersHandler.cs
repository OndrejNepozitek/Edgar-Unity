using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers
{
    public abstract class AbstractTilemapLayersHandler : ScriptableObject, ITilemapLayersHandler
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}