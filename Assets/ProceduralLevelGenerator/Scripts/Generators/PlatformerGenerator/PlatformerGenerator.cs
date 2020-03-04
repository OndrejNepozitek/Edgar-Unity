using System.Collections.Generic;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using UnityEngine;
using Debug = UnityEngine.Debug;
using PostProcessConfig = Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs.PostProcessConfig;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator
{
    // PRO
    public class PlatformerGenerator : LevelGeneratorBase<PlatformerGeneratorPayload>
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
        public List<PlatformerGeneratorPostProcessBase> CustomPostProcessTasks;

        public void Start()
        {
            if (OtherConfig.GenerateOnStart)
            {
                Generate();
            }
        }

        protected (List<PipelineItem> pipelineItems, PlatformerGeneratorPayload payload) GetPipelineItemsAndPayload()
        {
            var payload = InitializePayload();
            var pipelineItems = new List<PipelineItem>();

            // Add input setup
            pipelineItems.Add(GetInputTask());

            // Add dungeon generator
            pipelineItems.Add(GetGeneratorTask());

            // Add post process
            var postProcessPipelineConfig = ScriptableObject.CreateInstance<PlatformerPostProcessPipelineConfig>();
            postProcessPipelineConfig.Config = PostProcessConfig;
            PostProcessConfig.CustomPostProcessTasks = CustomPostProcessTasks;
            pipelineItems.Add(postProcessPipelineConfig);

            return (pipelineItems, payload);
        }

        public override void Generate()
        {
            Debug.Log("--- Generator started ---");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            PipelineRunner.Run(pipelineItems, payload);

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");
        }

        private PipelineItem GetInputTask()
        {
            if (InputType == PlatformerGeneratorInputType.CustomInput)
            {
                // PRO
                return CustomInputTask;
            }
            else
            {
                var fixedInputPipelineConfig = ScriptableObject.CreateInstance<FixedLevelGraphPipelineConfig>();
                fixedInputPipelineConfig.Config = FixedLevelGraphConfig;
                return fixedInputPipelineConfig;
            }
        }

        private PipelineItem GetGeneratorTask()
        {
            var dungeonGeneratorPipelineConfig = ScriptableObject.CreateInstance<PlatformerGeneratorPipelineConfig>();
            dungeonGeneratorPipelineConfig.Config = GeneratorConfig;
            return dungeonGeneratorPipelineConfig;
        }

        public void ExportMapDescription()
        {
            ExportMapDescription(GetInputTask());
        }

        protected override PlatformerGeneratorPayload InitializePayload()
        {
            return new PlatformerGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(OtherConfig.UseRandomSeed, OtherConfig.RandomGeneratorSeed),
            };
        }
    }
}