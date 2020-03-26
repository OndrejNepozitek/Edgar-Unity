using System.Collections.Generic;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Attributes;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    public class DungeonGenerator : LevelGeneratorBase<DungeonGeneratorPayload>
    {
        [Expandable]
        public FixedLevelGraphConfig FixedLevelGraphConfig;

        [Expandable]
        public DungeonGeneratorConfig GeneratorConfig;

        [Expandable]
        public OtherConfig OtherConfig;

        [Expandable]
        public PostProcessConfig PostProcessConfig;

        [ExpandableScriptableObject(CanFold = false)]
        public List<PipelineItem> CustomPostProcessTasks;

        [Expandable]
        public AdvancedConfig AdvancedConfig;

        public void Start()
        {
            if (OtherConfig.GenerateOnStart)
            {
                Generate();
            }
        }

        protected (List<PipelineItem> pipelineItems, DungeonGeneratorPayload payload) GetPipelineItemsAndPayload()
        {
            var payload = InitializePayload();
            var pipelineItems = new List<PipelineItem>();

            // Add input setup
            pipelineItems.Add(GetInputTask());

            // Add dungeon generator
            pipelineItems.Add(GetGeneratorTask());

            // Add post process
            var postProcessPipelineConfig = ScriptableObject.CreateInstance<PostProcessPipelineConfig>();
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

            return (pipelineItems, payload);
        }

        public override object Generate()
        {
            Debug.Log("--- Generator started ---");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            PipelineRunner.Run(pipelineItems, payload);

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");

            return payload;
        }

        private PipelineItem GetInputTask()
        {
            var fixedInputPipelineConfig = ScriptableObject.CreateInstance<FixedLevelGraphPipelineConfig>();
            fixedInputPipelineConfig.Config = FixedLevelGraphConfig;

            return fixedInputPipelineConfig;
        }

        private PipelineItem GetGeneratorTask()
        {
            var dungeonGeneratorPipelineConfig = ScriptableObject.CreateInstance<DungeonGeneratorPipelineConfig>();
            dungeonGeneratorPipelineConfig.Config = GeneratorConfig;
            return dungeonGeneratorPipelineConfig;
        }

        public void ExportMapDescription()
        {
            ExportMapDescription(GetInputTask());
        }

        protected override DungeonGeneratorPayload InitializePayload()
        {
            return new DungeonGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(OtherConfig.UseRandomSeed, OtherConfig.RandomGeneratorSeed),
            };
        }
    }
}