using System.Collections.Generic;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;
using Debug = UnityEngine.Debug;
using PostProcessConfig = Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs.PostProcessConfig;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator
{
    // PRO
    public class PlatformerGeneratorRunner : MonoBehaviour, IGeneratorRunner
    {
        public PlatformerGeneratorInputType InputType;

        [ExpandableScriptableObject]
        public PipelineItem CustomInputTask;

        [Expandable]
        public FixedLevelGraphConfig FixedLevelGraphConfig;

        [Expandable]
        public PlatformerGeneratorConfig GeneratorConfig;

        [Expandable]
        public OtherConfig OtherConfig;

        [Expandable]
        public PostProcessConfig PostProcessConfig;

        [ExpandableScriptableObject(CanFold = false)]
        public List<PipelineItem> CustomPostProcessTasks;

        private readonly Random seedsGenerator = new Random();

        public void Start()
        {
            if (OtherConfig.GenerateOnStart)
            {
                Generate();
            }
        }

        public PlatformerGeneratorPayload Generate()
        {
            Debug.Log("--- Generator started ---");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var payload = new PlatformerGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(),
            };

            var pipelineRunner = new PipelineRunner();
            var pipelineItems = new List<PipelineItem>();

            // Add input setup
            if (InputType == PlatformerGeneratorInputType.CustomInput)
            {
                // PRO
                pipelineItems.Add(CustomInputTask);
            }
            else
            {
                var fixedInputPipelineConfig = ScriptableObject.CreateInstance<FixedLevelGraphPipelineConfig>();
                fixedInputPipelineConfig.Config = FixedLevelGraphConfig;
                pipelineItems.Add(fixedInputPipelineConfig);
            }

            // Add dungeon generator
            var platformerGeneratorPipelineConfig = ScriptableObject.CreateInstance<PlatformerGeneratorPipelineConfig>();
            platformerGeneratorPipelineConfig.Config = GeneratorConfig;
            pipelineItems.Add(platformerGeneratorPipelineConfig);

            // Add post process
            var postProcessPipelineConfig = ScriptableObject.CreateInstance<PlatformerPostProcessPipelineConfig>();
            postProcessPipelineConfig.Config = PostProcessConfig;
            pipelineItems.Add(postProcessPipelineConfig);

            // Add custom post process tasks
            if (CustomPostProcessTasks != null)
            {
                foreach (var customPostProcessTask in CustomPostProcessTasks)
                {
                    pipelineItems.Add(customPostProcessTask);
                }
            }

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

        object IGeneratorRunner.Generate()
        {
            return Generate();
        }
    }
}