using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class PostProcessingConfigGrid2D
    {
        public bool InitializeSharedTilemaps = true;

        public TilemapLayersStructureModeGrid2D TilemapLayersStructure = TilemapLayersStructureModeGrid2D.Default;

        [ConditionalHide(nameof(IsTilemapsCustom))]
        public TilemapLayersHandlerBaseGrid2D TilemapLayersHandler;

        [ConditionalHide(nameof(IsTilemapsFromExample))]
        public GameObject TilemapLayersExample;

        public Material TilemapMaterial;

        public bool CopyTilesToSharedTilemaps = true;

        public bool CenterGrid = true;

        public bool DisableRoomTemplatesRenderers = true;

        public bool DisableRoomTemplatesColliders = true;

        private bool IsTilemapsFromExample => TilemapLayersStructure == TilemapLayersStructureModeGrid2D.FromExample;

        private bool IsTilemapsCustom => TilemapLayersStructure == TilemapLayersStructureModeGrid2D.Custom;
    }
}