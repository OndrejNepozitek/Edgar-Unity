using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    public class DungeonRoomTemplateInitializer : BaseRoomTemplateInitializer
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = new DungeonTilemapLayersHandler();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }
    }
}