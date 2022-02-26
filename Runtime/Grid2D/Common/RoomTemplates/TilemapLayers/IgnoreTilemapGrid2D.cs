using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Component that can be used as a flag to ignore a tilemap layer to which it is attached in various situations.
    /// </summary>
    [AddComponentMenu("Edgar/Grid2D/Ignore Tilemap (Grid2D)")]
    public class IgnoreTilemapGrid2D : MonoBehaviour
    {
        public bool IgnoreWhenCopyingTiles = false;

        public bool IgnoreWhenComputingOutline = false;

        public bool IgnoreWhenDisablingColliders = false;

        public bool IgnoreWhenDisablingRenderers = false;
    }
}