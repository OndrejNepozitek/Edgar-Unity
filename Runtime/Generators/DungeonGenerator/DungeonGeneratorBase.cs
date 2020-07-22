using System;
using System.Collections.Generic;
using System.IO;
using MapGeneration.Core.MapDescriptions;
using MapGeneration.Utils;
using Newtonsoft.Json;
using ProceduralLevelGenerator.Unity.Attributes;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator
{
    /// <summary>
    /// Base class for various dungeon generators.
    /// </summary>
    public abstract class DungeonGeneratorBase : LevelGeneratorBase<DungeonGeneratorPayload>
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
            pipelineItems.Add(GetPostProcessingTask());

            return (pipelineItems, payload);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayload> GetInputTask()
        {
            return new FixedLevelGraphInputTask<DungeonGeneratorPayload>(FixedLevelGraphConfig);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayload> GetGeneratorTask()
        {
            return new DungeonGeneratorTask<DungeonGeneratorPayload>(GeneratorConfig);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayload> GetPostProcessingTask()
        {
            return new PostProcessTask<DungeonGeneratorPayload>(PostProcessConfig, () => new DungeonTilemapLayersHandler(), CustomPostProcessTasks);
        }

        protected virtual DungeonGeneratorPayload InitializePayload()
        {
            return new DungeonGeneratorPayload()
            {
                Random = GetRandomNumbersGenerator(UseRandomSeed, RandomGeneratorSeed),
            };
        }

        public void ExportMapDescription()
        {
            var payload = InitializePayload();
            var inputSetup = GetInputTask();

            var pipelineItems = new List<IPipelineTask<DungeonGeneratorPayload>> {inputSetup};

            PipelineRunner.Run(pipelineItems, payload);

            var levelDescription = payload.LevelDescription;
            var mapDescription = levelDescription.GetMapDescription();
            var intMapDescription = GetIntMapDescription(mapDescription);
            var json = JsonConvert.SerializeObject(intMapDescription, Formatting.Indented, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.Auto,
            });

            var filename = "exportedMapDescription.json";
            File.WriteAllText(filename, json);
            Debug.Log($"Map description exported to {filename}");
        }

        private MapDescription<int> GetIntMapDescription(MapDescription<RoomBase> mapDescription)
        {
            var newMapDescription = new MapDescription<int>();
            var mapping = mapDescription.GetGraph().Vertices.CreateIntMapping();

            foreach (var vertex in mapDescription.GetGraph().Vertices)
            {
                newMapDescription.AddRoom(mapping[vertex], mapDescription.GetRoomDescription(vertex));
            }

            foreach (var edge in mapDescription.GetGraph().Edges)
            {
                newMapDescription.AddConnection(mapping[edge.From], mapping[edge.To]);
            }

            return newMapDescription;
        }

        protected override int OnUpgradeSerializedData(int version)
        {
#pragma warning disable 618
            if (version < 2)
            {
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
            }
#pragma warning restore 618

            return 2;
        }
    }
}