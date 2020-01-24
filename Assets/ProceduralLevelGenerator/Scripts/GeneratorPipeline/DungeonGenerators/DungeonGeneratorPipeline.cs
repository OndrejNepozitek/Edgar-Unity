using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.PrecomputedLevels;
using MapGeneration.Benchmarks;
using MapGeneration.Benchmarks.ResultSaving;
using MapGeneration.Interfaces.Benchmarks;
using MapGeneration.MetaOptimization.Evolution.DungeonGeneratorEvolution;
using MapGeneration.Utils.MapDrawing;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System.Collections.Generic;
	using Payloads.PayloadInitializers;
	using Pipeline;
	using UnityEngine;
	using Utils;

	/// <summary>
	/// MonoBehaviour script that holds the whole generator pipeline.
	/// </summary>
	public class DungeonGeneratorPipeline : MonoBehaviour
	{
		[ExpandableNotFoldable]
		public AbstractPayloadInitializer PayloadInitializer;

		[HideInInspector]
		[ExpandableNotFoldable]
		public List<PipelineItem> PipelineItems;

        public int BenchmarkRuns = 20;

        public Camera ScreenshotCamera;

        public AbstractPrecomputedLevelsHandler PrecomputedLevelsHandler;

        public int LevelsToPrecompute = 20;

        [HideInInspector]
        public bool IsPrecomputeRunning = false;

        [HideInInspector]
        public int PrecomputeProgress;

        public void Generate()
		{
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.Log("--- Pipeline started ---"); 

			var pipelineRunner = new PipelineRunner();
			pipelineRunner.Run(PipelineItems, PayloadInitializer.InitializePayload());

            Debug.Log($"--- Pipeline completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
		}

        // TODO: maybe make this with live preview? Would it need to do the benchmark manually?
        public void RunBenchmark()
        {
            StartCoroutine(RunBenchmarkCoroutine());
        }

        // TODO: maybe make this with live preview? Would it need to do the benchmark manually?
        public IEnumerator RunBenchmarkCoroutine()
        {
			Debug.Log("Run Benchmark");

            var layoutDrawer = new SVGLayoutDrawer<int>();
            var pipelineRunner = new PipelineRunner();
            var runs = new List<GeneratorRun<AdditionalRunData>>();

            for (int i = 0; i < BenchmarkRuns; i++)
            {
                var payload = PayloadInitializer.InitializePayload();
                pipelineRunner.Run(PipelineItems, payload);

                if (payload is IBenchmarkInfoPayload benchmarkInfoPayload)
                {
                    var screenshot = RTImage(ScreenshotCamera);
                    var png = screenshot.EncodeToPNG();
                    var base64 = Convert.ToBase64String(png);

                    var additionalData = new AdditionalUnityData()
                    {
                        GeneratedLayoutSvg = layoutDrawer.DrawLayout(benchmarkInfoPayload.GeneratedLayout, 800, forceSquare: true),
                        ImageBase64 = base64,
                    };

                    var generatorRun = new GeneratorRun<AdditionalRunData>(benchmarkInfoPayload.GeneratedLayout != null, benchmarkInfoPayload.TimeTotal, benchmarkInfoPayload.Iterations, additionalData);

                    runs.Add(generatorRun);
                }
                else
                {
                    throw new InvalidOperationException($"Payload must implement {nameof(IBenchmarkInfoPayload)}");
                }

                yield return null;
            }

            var scenarioResult = new BenchmarkScenarioResult("Test", new List<BenchmarkResult>()
            {
                new BenchmarkResult("Test", runs.Cast<IGeneratorRun>().ToList())
            });
            var resultSaver = new BenchmarkResultSaver();
            resultSaver.SaveResult(scenarioResult, directory: "Benchmarks/");
        }

        // Take a "screenshot" of a camera's Render Texture.
        Texture2D RTImage(Camera camera)
        {
            Debug.Log(Screen.width);
            Debug.Log(Screen.height);

            var width = 500;
            var height = 500;

            Rect rect = new Rect(0, 0, width, height);
            RenderTexture renderTexture = new RenderTexture(width, height, 24);
            Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGBA32, false);
 
            ScreenshotCamera.targetTexture = renderTexture;
            ScreenshotCamera.Render();

            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);
 
            ScreenshotCamera.targetTexture = null;
            RenderTexture.active = null;
 
            DestroyImmediate(renderTexture);
            renderTexture = null;
            return screenShot;
        }

        private class AdditionalUnityData : AdditionalRunData
        {
            public string ImageBase64 { get; set; }
        }

        public void PrecomputeLevels()
        {
            Debug.Log("Precompute levels");
            StartCoroutine(PrecomputeLevelsCoroutine());
        }

        private IEnumerator PrecomputeLevelsCoroutine()
        {
            var payloads = new List<object>();
            var pipelineRunner = new PipelineRunner();
            IsPrecomputeRunning = true;

            PrecomputedLevelsHandler.OnComputationStarted();

            for (int i = 0; i < LevelsToPrecompute; i++)
            {
                var payload = PayloadInitializer.InitializePayload();
                pipelineRunner.Run(PipelineItems, payload);
                
                PrecomputedLevelsHandler.SaveLevel(payload);
                PrecomputeProgress = i + 1;
                yield return null;
            }

            // TODO: check if not null
            PrecomputedLevelsHandler.OnComputationEnded();
        }
	}
}