using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class PostProcessConfigGrid2D
    {
        public bool InitializeSharedTilemaps = true;

        public TilemapLayersStructureMode TilemapLayersStructure = TilemapLayersStructureMode.Default;

        [ConditionalHide(nameof(IsTilemapsCustom))]
#pragma warning disable 618
        public TilemapLayersHandlerBase TilemapLayersHandler;
#pragma warning restore 618

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