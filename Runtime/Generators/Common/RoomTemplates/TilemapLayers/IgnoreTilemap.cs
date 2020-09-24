using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Component that can be used as a flag to ignore a tilemap layer to which it is attached.
    /// </summary>
    public class IgnoreTilemap : MonoBehaviour
    {
        public bool IgnoreWhenCopyingTiles = true;

        public bool IgnoreWhenComputingOutline = true;
    }
}