using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace ProceduralLevelGenerator.Unity.Tests.Runtime.Examples
{
    public abstract class ExampleTestsBase
    {
        protected void LoadScene(string name)
        {
            SceneManager.LoadScene(GetSceneFilePath(name), LoadSceneMode.Single);
        }

        // Helper to find a scene path
        protected static string GetSceneFilePath(string sceneName)
        {
            foreach (var scene in EditorBuildSettings.scenes) {
                if (scene.path.Contains(sceneName)) {
                    return scene.path;
                }
            }

            // We do not need to do anything fancy here. If the scene has not been found,
            // it will fail with the empty string and we know something is wrong.
            return "";
        }
    }
}