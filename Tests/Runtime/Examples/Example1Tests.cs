using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ProceduralLevelGenerator.Unity.Tests.Runtime.Examples
{
    public class Example1Tests
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene(GetSceneFilePath("Example1"), LoadSceneMode.Single);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test()
        {
            yield return null;
            yield return new WaitForSeconds(1f);
        }

        // Helper to find a scene path
        static string GetSceneFilePath(string sceneName)
        {
            foreach (var scene in EditorBuildSettings.scenes) {
                if (scene.path.Contains(sceneName)) {
                    return scene.path;
                }
            }

            // We do not need to do anything fancy here. If the scene has not been found,
            // it will fail with the empty stirng and we know something is wrong.
            return "";
        }
    }
}