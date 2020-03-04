using System;
using System.Collections;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Examples.Common;
using Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts
{
    public class DeadCellsGameManager : GameManagerBase<DeadCellsGameManager>
    {
        public DeadCellsLevelType LevelType;
        private long generatorElapsedMilliseconds;
        
        // To make sure that we do not start the generator multiple times
        private bool isGenerating;

        public void Update()
        {
            if (Input.GetKey(KeyCode.T) && !isGenerating)
            {
                SwitchLevel();
            }

            if (Input.GetKey(KeyCode.G) && !isGenerating)
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
            isGenerating = true;

            // Show loading screen
            ShowLoadingScreen($"Dead Cells - {LevelType}", "loading..");

            // Find the generator runner
            var generator = GameObject.Find($"Platformer Generator").GetComponent<PlatformerGenerator>();

            // Start the generator coroutine
            StartCoroutine(GeneratorCoroutine(generator));
        }


        private IEnumerator GeneratorCoroutine(PlatformerGenerator generator)
        {
            yield return null;

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var generatorCoroutine = this.StartCoroutineWithData<object>(generator.GenerateCoroutine());

            yield return generatorCoroutine.Coroutine;

            stopwatch.Stop();

            yield return null;

            isGenerating = false;

            generatorCoroutine.ThrowIfNotSuccessful();

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