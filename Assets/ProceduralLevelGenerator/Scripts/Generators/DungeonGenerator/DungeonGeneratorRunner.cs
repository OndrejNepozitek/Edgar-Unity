using System.Collections.Generic;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator
{
    // TODO: is this name ok?
    public class DungeonGeneratorRunner : MonoBehaviour
    {
        [Expandable]
        public FixedLevelGraphConfig FixedLevelGraphConfig;

        [Expandable]
        public DungeonGeneratorConfig Config;

        [Expandable]
        public OtherConfig OtherConfig;

        [Expandable]
        public PostProcessConfig PostProcessConfig;

        private readonly Random seedsGenerator = new Random();

        public void Start()
        {
            if (OtherConfig.GenerateOnStart)
            {
                Generate();
            }
        }

        public DungeonGeneratorPayload Generate()
        {
            Debug.Log("--- Generator started ---");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var payload = new DungeonGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(),
            };

            var pipelineRunner = new PipelineRunner();
            var pipelineItems = new List<PipelineItem>();

            // Add input setup
            var fixedInputPipelineConfig = ScriptableObject.CreateInstance<FixedLevelGraphPipelineConfig>();
            fixedInputPipelineConfig.Config = FixedLevelGraphConfig;
            pipelineItems.Add(fixedInputPipelineConfig);

            // Add dungeon generator
            var dungeonGeneratorPipelineConfig = ScriptableObject.CreateInstance<DungeonGeneratorPipelineConfig>();
            dungeonGeneratorPipelineConfig.Config = Config;
            pipelineItems.Add(dungeonGeneratorPipelineConfig);

            // Add post process
            var postProcessPipelineConfig = ScriptableObject.CreateInstance<PostProcessPipelineConfig>();
            postProcessPipelineConfig.Config = PostProcessConfig;
            pipelineItems.Add(postProcessPipelineConfig);

            pipelineRunner.Run(pipelineItems, payload);

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");

            return payload;
        }

        protected virtual Random GetRandomNumbersGenerator()
        {
            var seed = OtherConfig.UseRandomSeed ? seedsGenerator.Next() : OtherConfig.RandomGeneratorSeed;
            Debug.Log($"Random generator seed: {seed}");

            return new Random(seed);
        }
    }
}