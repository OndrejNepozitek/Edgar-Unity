using System.Collections;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Examples.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.Example1.Scripts
{
    /// <summary>
    /// Example of a simple game manager that uses the DungeonGeneratorRunner to generate levels.
    /// </summary>
    public class Example1GameManager : GameManagerBase<Example1GameManager>
    {
        public void Update()
        {
            if (Input.GetKey(KeyCode.G))
            {
                LoadNextLevel();
            }
        }

        public override void LoadNextLevel()
        {
            // Show loading screen
            ShowLoadingScreen("Example 1", "loading..");

            // Find the generator runner
            var generator = GameObject.Find("Dungeon Generator").GetComponent<DungeonGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }

        /// <summary>
        /// Coroutine that generates the level.
        /// We need to yield return before the generator starts because we want to show the loading screen
        /// and it cannot happen in the same frame.
        /// It is also sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
        private IEnumerator GeneratorCoroutine(DungeonGenerator generator)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            yield return null;

            generator.Generate();

            yield return null;

            stopwatch.Stop();

            SetLevelInfo($"Generated in {stopwatch.ElapsedMilliseconds/1000d:F}s");
            HideLoadingScreen();
        }
    }
}