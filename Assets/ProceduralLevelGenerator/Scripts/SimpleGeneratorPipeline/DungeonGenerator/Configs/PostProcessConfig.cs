using System;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.TilemapLayers;

namespace Assets.ProceduralLevelGenerator.Scripts.SimpleGeneratorPipeline.DungeonGenerator.Configs
{
    [Serializable]
    public class PostProcessConfig
    {
        public bool CombineTilemaps = true;

        public bool CenterGrid = true;

        public bool DisableRoomTemplatesRenderers = true;

        public bool DisableRoomTemplatesColliders = true;

        public AbstractTilemapLayersHandler TilemapLayersHandler;
    }
}