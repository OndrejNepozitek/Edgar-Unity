using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers
{
    public abstract class TilemapLayersHandlerBase : ScriptableObject, ITilemapLayersHandler
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}