using System.IO;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity
{
    public static class RoomTemplateInitializerUtilsGrid2D
    {
        /// <summary>
        /// Creates a room template prefab using a given room template initializer.
        /// </summary>
        /// <typeparam name="TRoomTemplateInitializer"></typeparam>
        public static void CreateRoomTemplatePrefab<TRoomTemplateInitializer>() where TRoomTemplateInitializer : RoomTemplateInitializerBaseGrid2D
        {
            #if UNITY_EDITOR
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
            #endif
        }

        #if UNITY_EDITOR
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
        #endif
    }
}