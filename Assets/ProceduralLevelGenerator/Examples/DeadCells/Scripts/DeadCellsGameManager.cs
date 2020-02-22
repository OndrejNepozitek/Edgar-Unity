using System;
using System.Collections;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Examples.Common;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts
{
    public class DeadCellsGameManager : GameManagerBase<DeadCellsGameManager>
    {
        public DeadCellsLevelType LevelType;
        private long generatorElapsedMilliseconds;
        
        public void Update()
        {
            if (Input.GetKey(KeyCode.T))
            {
                SwitchLevel();
            }

            if (Input.GetKey(KeyCode.G))
            {
                LoadNextLevel();
            }
        }

        public void SwitchLevel()
        {
            switch (LevelType)
            {
                case DeadCellsLevelType.Rooftop:
                    LevelType = DeadCellsLevelType.Underground;
                    SceneManager.LoadScene("DeadCellsUnderground");
                    break;

                case DeadCellsLevelType.Underground:
                    LevelType = DeadCellsLevelType.Rooftop;
                    SceneManager.LoadScene("DeadCellsRamparts");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void LoadNextLevel()
        {
            // Show loading screen
            ShowLoadingScreen($"Dead Cells - {LevelType}", "loading..");

            // Find the generator runner
            var generator = GameObject.Find($"Platformer Generator").GetComponent<PlatformerGeneratorRunner>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }


        private IEnumerator GeneratorCoroutine(PlatformerGeneratorRunner generator)
        {
            yield return null;

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            generator.Generate();

            yield return null;

            stopwatch.Stop();
            generatorElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            RefreshLevelInfo();
            HideLoadingScreen();
        }

        private void RefreshLevelInfo()
        {
            SetLevelInfo($"Generated in {generatorElapsedMilliseconds / 1000d:F}s\nLevel type: {LevelType}");
        }
    }
}