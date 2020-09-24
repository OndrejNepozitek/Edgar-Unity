using UnityEngine;

namespace Edgar.Unity
{
    public abstract class TilemapLayersHandlerBase : ScriptableObject, ITilemapLayersHandler
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}