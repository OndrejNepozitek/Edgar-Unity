using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    /// <summary>
    /// Basic dungeon room template initializer.
    /// Uses DungeonTilemapLayersHandler to create tilemaps structure.
    /// </summary>
    public class DungeonRoomTemplateInitializer : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = new DungeonTilemapLayersHandler();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }

        [MenuItem("Assets/Create/Dungeon generator/Dungeon room template")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<DungeonRoomTemplateInitializer>();
        }
    }
}