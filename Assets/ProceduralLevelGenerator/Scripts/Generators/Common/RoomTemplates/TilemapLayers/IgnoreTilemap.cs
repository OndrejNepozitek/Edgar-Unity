using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers
{
    public class IgnoreTilemap : MonoBehaviour
    {
        public bool IgnoreWhenCopyingTiles = true;

        public bool IgnoreWhenComputingOutline = true;
    }
}