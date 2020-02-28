using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class PostProcessConfig
    {
        public bool InitializeSharedTilemaps = true;

        public TilemapLayersHandlerBase TilemapLayersHandler;

        public bool CopyTilesToSharedTilemaps = true;

        public bool CenterGrid = true;

        public bool DisableRoomTemplatesRenderers = true;

        public bool DisableRoomTemplatesColliders = true;
    }
}