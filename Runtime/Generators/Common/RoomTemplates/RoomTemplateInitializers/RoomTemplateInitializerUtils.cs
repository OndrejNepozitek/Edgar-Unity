using System.IO;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers
{
    public static class RoomTemplateInitializerUtils
    {
        public static void CreateRoomTemplatePrefab<TRoomTemplateInitializer>() where TRoomTemplateInitializer : RoomTemplateInitializerBase
        {
            // Create empty game object
            var roomTemplate = new GameObject();

            // Add room template initializer, initialize room template, destroy initializer
            var roomTemplateInitializer = roomTemplate.AddComponent<TRoomTemplateInitializer>();
            roomTemplateInitializer.Initialize();
            Object.DestroyImmediate(roomTemplateInitializer);

            // Save prefab
            var currentPath = GetCurrentPath();
            PrefabUtility.SaveAsPrefabAsset(roomTemplate, AssetDatabase.GenerateUniqueAssetPath(currentPath + "/Room template.prefab"));

            // Remove game object from scene
            Object.DestroyImmediate(roomTemplate);
        }

        
        private static string GetCurrentPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }
    }
}