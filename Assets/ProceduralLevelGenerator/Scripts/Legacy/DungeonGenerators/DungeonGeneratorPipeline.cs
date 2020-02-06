using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.PayloadInitializers;
using Assets.ProceduralLevelGenerator.Scripts.Legacy.PrecomputedLevels;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using MapGeneration.Benchmarks;
using MapGeneration.Benchmarks.ResultSaving;
using MapGeneration.Interfaces.Benchmarks;
using MapGeneration.MetaOptimization.Evolution.DungeonGeneratorEvolution;
using MapGeneration.Utils;
using MapGeneration.Utils.MapDrawing;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators
{
    /// <summary>
    ///     MonoBehaviour script that holds the whole generator pipeline.
    /// </summary>
    public class DungeonGeneratorPipeline : MonoBehaviour
    {
        /// <summary>
        /// Number of benchmark runs.
        /// </summary>
        public int BenchmarkRuns = 20;

        /// <summary>
        /// The camera to take the screenshot.
        /// </summary>
        public Camera ScreenshotCamera;

        /// <summary>
        /// Orthographic size of the camera used for the screenshot. Should fit the whole generated level.
        /// </summary>
        public float ScreenshotCameraSize = 60;

        /// <summary>
        /// Whether the benchmark is running or not.
        /// </summary>
        public bool BenchmarkIsRunning;

        /// <summary>
        /// The number of completed runs of the benchmark.
        /// </summary>
        public int BenchmarkProgress;

        private IEnumerator benchmarkCoroutine;

        [HideInInspector]
        public bool IsPrecomputeRunning;

        public int LevelsToPrecompute = 20;

        [ExpandableNotFoldable]
        public AbstractPayloadInitializer PayloadInitializer;

        [HideInInspector]
        [ExpandableNotFoldable]
        public List<PipelineItem> PipelineItems;

        public AbstractPrecomputedLevelsHandler PrecomputedLevelsHandler;

        [HideInInspector]
        public int PrecomputeProgress;

        public void Start()
        {
            BenchmarkIsRunning = false;
        }

        public void Generate()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.Log("--- Pipeline started ---");

            var pipelineRunner = new PipelineRunner();
            pipelineRunner.Run(PipelineItems, PayloadInitializer.InitializePayload());

            Debug.Log($"--- Pipeline completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
        }

        public void RunBenchmark()
        {
            benchmarkCoroutine = RunBenchmarkCoroutine();
            StartCoroutine(benchmarkCoroutine);
        }

        public void StopBenchmark()
        {
            if (benchmarkCoroutine != null)
            {
                StopCoroutine(benchmarkCoroutine);
            }
            
            BenchmarkIsRunning = false;
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

            for (var i = 0; i < LevelsToPrecompute; i++)
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

        public IEnumerator RunBenchmarkCoroutine()
        {
            BenchmarkIsRunning = true;
            var path = Path.Combine("Benchmarks", FileNamesHelper.PrefixWithTimestamp("benchmark.json"));
            var layoutDrawer = new SVGLayoutDrawer<Room>();
            var pipelineRunner = new PipelineRunner();
            var runs = new List<GeneratorRun<AdditionalRunData>>();

            BenchmarkProgress = 0;

            for (var i = 0; i < BenchmarkRuns; i++)
            {
                BenchmarkProgress = i + 1;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var payload = PayloadInitializer.InitializePayload();
                pipelineRunner.Run(PipelineItems, payload);

                if (payload is IBenchmarkInfoPayload benchmarkInfoPayload)
                {
                    var screenshot = ProUtils.TakeScreenshot(ScreenshotCamera, ScreenshotCameraSize, 1000, 1000);
                    var png = screenshot.EncodeToPNG();
                    var base64 = Convert.ToBase64String(png);

                    var additionalData = new AdditionalUnityData
                    {
                        GeneratedLayoutSvg = layoutDrawer.DrawLayout(benchmarkInfoPayload.GeneratedLevel.GetInternalLayoutRepresentation(), 800,
                            forceSquare: true),
                        ImageBase64 = base64
                    };

                    var generatorRun = new GeneratorRun<AdditionalRunData>(benchmarkInfoPayload.GeneratedLevel.GetInternalLayoutRepresentation() != null,
                        stopwatch.ElapsedMilliseconds, benchmarkInfoPayload.Iterations, additionalData);

                    runs.Add(generatorRun);
                }
                else
                {
                    throw new InvalidOperationException($"Payload must implement {nameof(IBenchmarkInfoPayload)}");
                }

                yield return null; 
            }

            var scenarioResult = new BenchmarkScenarioResult(name, new List<BenchmarkResult>
            {
                new BenchmarkResult(name, runs.Cast<IGeneratorRun>().ToList())
            });
            var resultSaver = new BenchmarkResultSaver();
            resultSaver.SaveResult(scenarioResult, path);
            BenchmarkIsRunning = false;
        }

        private class AdditionalUnityData : AdditionalRunData
        {
            public string ImageBase64 { get; set; }
        }
    }
}