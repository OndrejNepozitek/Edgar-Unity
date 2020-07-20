using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers
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