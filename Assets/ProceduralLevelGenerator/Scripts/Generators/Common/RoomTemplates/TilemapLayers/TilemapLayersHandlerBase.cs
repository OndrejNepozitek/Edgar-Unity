using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers
{
    public abstract class TilemapLayersHandlerBase : ScriptableObject, ITilemapLayersHandler
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}