namespace ProceduralLevelGenerator.Unity.Examples.Resources.Docs.CustomizeRoomTemplates
{
    using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
    using UnityEngine;
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

    public class CustomRoomTemplateInitializerExample : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            // Create an instance of our custom tilemap layers handler
            var tilemapLayersHandler = ScriptableObject.CreateInstance<CustomTilemapLayersHandlerExample>();

            // Initialize tilemaps
            tilemapLayersHandler.InitializeTilemaps(tilemapsRoot);
        }

        #if UNITY_EDITOR
        // [MenuItem("Assets/Create/Dungeon generator/Custom room template")]
        public static void CreateRoomTemplatePrefab()
        {
            // Make sure to use the correct generic parameter - it should be the type of this class
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<CustomRoomTemplateInitializerExample>();
        }
        #endif
    }
}