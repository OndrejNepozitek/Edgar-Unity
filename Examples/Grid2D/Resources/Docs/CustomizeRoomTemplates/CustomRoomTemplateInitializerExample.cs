using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    #region codeBlock:2d_roomTemplateCustomization_initializer_1

    public class CustomRoomTemplateInitializerExample : RoomTemplateInitializerBaseGrid2D
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            // Create an instance of our custom tilemap layers handler
            var tilemapLayersHandler = ScriptableObject.CreateInstance<CustomTilemapLayersHandlerExample>();

            // Initialize tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        // Change the attribute below to anything you want. (And uncomment it)
        // [MenuItem("Assets/Create/Dungeon generator/Custom room template")]
        public static void CreateRoomTemplatePrefab()
        {
            // Make sure to use the correct generic parameter - it should be the type of this class
            RoomTemplateInitializerUtilsGrid2D.CreateRoomTemplatePrefab<CustomRoomTemplateInitializerExample>();
        }
    }

    #endregion
}