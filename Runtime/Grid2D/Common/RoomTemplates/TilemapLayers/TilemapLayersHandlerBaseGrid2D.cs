using UnityEngine;

namespace Edgar.Unity
{
    public abstract class TilemapLayersHandlerBaseGrid2D : ScriptableObject, ITilemapLayersHandlerGrid2D
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}