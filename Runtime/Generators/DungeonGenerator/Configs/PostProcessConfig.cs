using System;

namespace Edgar.Unity
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