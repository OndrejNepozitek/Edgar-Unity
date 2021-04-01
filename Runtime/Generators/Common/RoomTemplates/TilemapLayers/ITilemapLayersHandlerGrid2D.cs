using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Interface for handling tilemap layers.
    /// </summary>
    public interface ITilemapLayersHandlerGrid2D
    {
        /// <summary>
        ///     Initializes the structure of tilemaps of a given gameObject.
        /// </summary>
        /// <remarks>
        ///     Adds child GameObjects with tilemap components attached. Multiple tilemaps are
        ///     used to layer individual tiles over one another. This is also the place to add
        ///     colliders and setup sorting order.
        /// </remarks>
        /// <param name="gameObject">Parent GameObject of created tilemaps.</param>
        void InitializeTilemaps(GameObject gameObject);
    }
}