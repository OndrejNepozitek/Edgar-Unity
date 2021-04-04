using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Obsolete("Please use TilemapLayersHandlerBaseGrid2D instead.")]
    public abstract class TilemapLayersHandlerBase : ScriptableObject, ITilemapLayersHandlerGrid2D
    {
        /// <inheritdoc />
        public abstract void InitializeTilemaps(GameObject gameObject);
    }
}