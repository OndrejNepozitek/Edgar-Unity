using System.IO;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Utils
{
    public static class MenuItems
    {
        [MenuItem("Assets/Create/Dungeon generator/Dungeon room template")]
        public static void CreateDungeonRoomTemplate()
        {
            // Create empty game object
            var roomTemplate = new GameObject();

            // Add room template initializer, initialize room template, destroy initializer
            var roomTemplateInitializer = roomTemplate.AddComponent<DungeonRoomTemplateInitializer>();
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
                path = path.Replace(Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
            }

            return path;
        }
    }
}