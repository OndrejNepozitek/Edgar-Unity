using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class PostProcessConfig
    {
        public bool InitializeSharedTilemaps = true;

        public TilemapLayersStructureMode TilemapLayersStructure = TilemapLayersStructureMode.Default;

        [ConditionalHide(nameof(IsTilemapsCustom))]
        public TilemapLayersHandlerBase TilemapLayersHandler;

        [ConditionalHide(nameof(IsTilemapsFromExample))]
        public GameObject TilemapLayersExample;

        public Material TilemapMaterial;

        public bool CopyTilesToSharedTilemaps = true;

        public bool CenterGrid = true;

        public bool DisableRoomTemplatesRenderers = true;

        public bool DisableRoomTemplatesColliders = true;

        private bool IsTilemapsFromExample => TilemapLayersStructure == TilemapLayersStructureMode.FromExample;

        private bool IsTilemapsCustom => TilemapLayersStructure == TilemapLayersStructureMode.Custom;
    }
}