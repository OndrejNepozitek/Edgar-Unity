using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Edgar.Unity.Tests
{
    public class TestBase : IPrebuildSetup, IPostBuildCleanup
    {
        private const string TestSceneFolder = "Assets/Edgar/Tests";
        private const string ExampleSceneFolder = "Assets/Edgar/Examples";

        public void Setup()
        {
            AddTestScenesToBuildSettings();
        }

        public void Cleanup()
        {
            RemoveTestScenesFromBuildSettings();
        }

        protected DungeonGeneratorGrid2D GetDungeonGenerator(string name = "Dungeon Generator")
        {
            var dungeonGeneratorGameObject = GameObject.Find(name);
            var dungeonGenerator = dungeonGeneratorGameObject.GetComponent<DungeonGeneratorGrid2D>();

            return dungeonGenerator;
        }

        protected GameObject GetGeneratedLevelRoot()
        {
            return GameObject.Find("Generated Level");
        }

        protected void LoadScene(string name)
        {
            SceneManager.LoadScene(GetSceneFilePath(name), LoadSceneMode.Single);
        }

        // Helper to find a scene path
        protected static string GetSceneFilePath(string sceneName)
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.path.EndsWith(sceneName + ".unity"))
                {
                    return scene.path;
                }
            }

            // We do not need to do anything fancy here. If the scene has not been found,
            // it will fail with the empty string and we know something is wrong.
            return "";
        }

        /// Add all scenes to the build settings that are in the test scene folder.
        private static void AddTestScenesToBuildSettings()
        {
            #if UNITY_EDITOR
            var scenes = new List<EditorBuildSettingsScene>();
            var guids = AssetDatabase.FindAssets("t:Scene", new[] {TestSceneFolder, ExampleSceneFolder});
            if (guids != null)
            {
                foreach (string guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        var scene = new EditorBuildSettingsScene(path, true);
                        scenes.Add(scene);
                    }
                }
            }

            Debug.Log("Adding test scenes to build settings:\n" + string.Join("\n", scenes.Select(scene => scene.path)));
            EditorBuildSettings.scenes = scenes.ToArray();
            #endif
        }

        /// Remove all scenes from the build settings that are in the test scene folder.
        private static void RemoveTestScenesFromBuildSettings()
        {
            #if UNITY_EDITOR
            EditorBuildSettings.scenes = EditorBuildSettings.scenes
                .Where(scene => !scene.path.StartsWith(TestSceneFolder)).ToArray();
            #endif
        }
    }
}