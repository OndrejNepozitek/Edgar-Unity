using System;
using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Attributes;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    public class DungeonGenerator : LevelGeneratorBase<DungeonGeneratorPayload>
    {
        [Expandable]
        public FixedLevelGraphConfig FixedLevelGraphConfig;

        [Expandable]
        public DungeonGeneratorConfig GeneratorConfig;

        [Expandable]
        public PostProcessConfig PostProcessConfig;

        [ExpandableScriptableObject(CanFold = false)]
        public List<DungeonGeneratorPostProcessBase> CustomPostProcessTasks;

        [Expandable]
        [Obsolete("Please use directly the properties UseRandomSeed, RandomGeneratorSeed and GenerateOnStart")]
        public OtherConfig OtherConfig;

        [Expandable]
        [Obsolete("Please use directly the property ThrowExceptionsImmediately")]
        public AdvancedConfig AdvancedConfig;

        public bool UseRandomSeed = true;

        public int RandomGeneratorSeed;

        public bool GenerateOnStart = true;

        public bool ThrowExceptionsImmediately = false;

        public void Start()
        {
            if (GenerateOnStart)
            {
                Generate();
            }
        }

        protected override (List<IPipelineTask<DungeonGeneratorPayload>> pipelineItems, DungeonGeneratorPayload payload) GetPipelineItemsAndPayload()
        {
            var payload = InitializePayload();
            var pipelineItems = new List<IPipelineTask<DungeonGeneratorPayload>>();

            // Add input setup
            pipelineItems.Add(GetInputTask());

            // Add dungeon generator
            pipelineItems.Add(GetGeneratorTask());

            // Add post process
            var postProcessTask = new PostProcessTask<DungeonGeneratorPayload>(PostProcessConfig);
            pipelineItems.Add(postProcessTask);

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

        private PipelineTask<DungeonGeneratorPayload> GetInputTask()
        {
            return new FixedLevelGraphInputTask<DungeonGeneratorPayload>(FixedLevelGraphConfig);
        }

        private PipelineTask<DungeonGeneratorPayload> GetGeneratorTask()
        {
            return new DungeonGeneratorTask<DungeonGeneratorPayload>(GeneratorConfig);
        }

        protected override DungeonGeneratorPayload InitializePayload()
        {
            return new DungeonGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(UseRandomSeed, RandomGeneratorSeed),
            };
        }

        public void ExportMapDescription()
        {
            ExportMapDescription(GetInputTask());
        }

        protected override int OnUpgradeSerializedData(int version)
        {
#pragma warning disable 618
            if (OtherConfig != null)
            {
                UseRandomSeed = OtherConfig.UseRandomSeed;
                RandomGeneratorSeed = OtherConfig.RandomGeneratorSeed;
                GenerateOnStart = OtherConfig.GenerateOnStart;
            }

            if (AdvancedConfig != null)
            {
                ThrowExceptionsImmediately = AdvancedConfig.ThrowExceptionsImmediately;
            }
#pragma warning restore 618

            return 2;
        }
    }
}