using System;
using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs
{
    [Serializable]
    public class PostProcessConfig
    {
        public bool InitializeSharedTilemaps = true;

        public bool CopyTilesToSharedTilemaps = true;

        public bool CenterGrid = true;

        public bool DisableRoomTemplatesRenderers = true;

        public bool DisableRoomTemplatesColliders = true;

        public TilemapLayersHandlerBase TilemapLayersHandlerBase;

        [HideInInspector]
        public List<PlatformerGeneratorPostProcessBase> CustomPostProcessTasks;
    }
}