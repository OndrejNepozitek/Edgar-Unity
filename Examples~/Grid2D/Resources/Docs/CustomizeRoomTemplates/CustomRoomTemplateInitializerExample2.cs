using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    #region codeBlock:2d_roomTemplateCustomization_initializer_2

    public class CustomRoomTemplateInitializerExample2 : RoomTemplateInitializerBaseGrid2D
    {
        public override void Initialize()
        {
            // Call the default initialization
            base.Initialize();

            // Place your custom logic after initialization here
            // This script is attached to the room template game object that is being created (and this component is later removed)
            // So you can access the gameObject field and add e.g. additional game object

            // For example, we can add a game object that will hold lights
            var lightsGameObject = new GameObject("Lights");
            lightsGameObject.transform.SetParent(gameObject.transform);
        }

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
            RoomTemplateInitializerUtilsGrid2D.CreateRoomTemplatePrefab<CustomRoomTemplateInitializerExample2>();
        }
    }

    #endregion
}