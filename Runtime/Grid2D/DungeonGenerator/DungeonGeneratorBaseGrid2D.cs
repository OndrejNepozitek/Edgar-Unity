using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Base class for various dungeon generators.
    /// </summary>
    public abstract class DungeonGeneratorBaseGrid2D : LevelGeneratorBase<DungeonGeneratorPayloadGrid2D>
    {
        [Expandable]
        public FixedLevelGraphConfigGrid2D FixedLevelGraphConfig;

        [Expandable]
        public DungeonGeneratorConfigGrid2D GeneratorConfig;

        [Expandable]
        public PostProcessingConfigGrid2D PostProcessConfig;

        // TODO(rename):
        [ExpandableScriptableObject(CanFold = false)]
        public List<DungeonGeneratorPostProcessingGrid2D> CustomPostProcessTasks;

        [Expandable]
        [Obsolete("Please use directly the properties UseRandomSeed, RandomGeneratorSeed and GenerateOnStart")]
        public OtherConfigGrid2D OtherConfig;

        [Expandable]
        [Obsolete("Please use directly the property ThrowExceptionsImmediately")]
        public AdvancedConfigGrid2D AdvancedConfig;

        /// <summary>
        /// Whether to use a random seed.
        /// </summary>
        public bool UseRandomSeed = true;

        /// <summary>
        /// Which seed should be used for the random numbers generator.
        /// Is used only when UseRandomSeed is false.
        /// </summary>
        public int RandomGeneratorSeed;

        /// <summary>
        /// Whether to generate a level on enter play mode.
        /// </summary>
        public bool GenerateOnStart = true;

        public bool ThrowExceptionsImmediately = false;

        /// <summary>
        /// Disable all custom post-processing tasks.
        /// </summary>
        public bool DisableCustomPostProcessing = false;

        public void Start()
        {
            if (GenerateOnStart)
            {
                Generate();
            }
        }

        protected override (List<IPipelineTask<DungeonGeneratorPayloadGrid2D>> pipelineItems, DungeonGeneratorPayloadGrid2D payload) GetPipelineItemsAndPayload()
        {
            var payload = InitializePayload();
            var pipelineItems = new List<IPipelineTask<DungeonGeneratorPayloadGrid2D>>();

            // Add input setup
            pipelineItems.Add(GetInputTask());

            // Add dungeon generator
            pipelineItems.Add(GetGeneratorTask());

            // Add post process
            pipelineItems.Add(GetPostProcessingTask());

            return (pipelineItems, payload);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayloadGrid2D> GetInputTask()
        {
            return new FixedLevelGraphInputTaskGrid2D(FixedLevelGraphConfig);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayloadGrid2D> GetGeneratorTask()
        {
            return new DungeonGeneratorTaskGrid2D(GeneratorConfig);
        }

        protected virtual IPipelineTask<DungeonGeneratorPayloadGrid2D> GetPostProcessingTask()
        {
            var postProcessingTasks = !DisableCustomPostProcessing
                ? CustomPostProcessTasks
                : new List<DungeonGeneratorPostProcessingGrid2D>();

            var postProcessingComponents = !DisableCustomPostProcessing
                ? GetComponents<DungeonGeneratorPostProcessingComponentGrid2D>().ToList()
                : new List<DungeonGeneratorPostProcessingComponentGrid2D>();

            return new PostProcessingTaskGrid2D(
                PostProcessConfig,
                GetTilemapLayersHandler(),
                postProcessingTasks,
                postProcessingComponents);
        }

        protected virtual Func<ITilemapLayersHandlerGrid2D> GetTilemapLayersHandler()
        {
            return () => new DungeonTilemapLayersHandlerGrid2D();
        }

        protected virtual DungeonGeneratorPayloadGrid2D InitializePayload()
        {
            return new DungeonGeneratorPayloadGrid2D()
            {
                Random = GetRandomNumbersGenerator(UseRandomSeed, RandomGeneratorSeed),
                DungeonGenerator = this,
            };
        }

        public void ExportMapDescription()
        {
            var payload = InitializePayload();
            var inputSetup = GetInputTask();

            var pipelineItems = new List<IPipelineTask<DungeonGeneratorPayloadGrid2D>> {inputSetup};

            PipelineRunner.Run(pipelineItems, payload);

            var levelDescription = payload.LevelDescription.GetLevelDescription();
            levelDescription.Name = "Test";
            var wrappedLevelDescription = GetWrappedLevelDescription(levelDescription);

            var filename = "exportedMapDescription.json";
            wrappedLevelDescription.SaveToJson(filename);
            Debug.Log($"Map description exported to {filename}");
        }

        private LevelDescriptionGrid2D<RoomWrapper> GetWrappedLevelDescription(LevelDescriptionGrid2D<RoomBase> originalLevelDescription)
        {
            var levelDescription = new LevelDescriptionGrid2D<RoomWrapper>();

            var srcProperties = originalLevelDescription.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            var dstProperties = levelDescription.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            foreach (var srcProperty in srcProperties)
            {
                var dstProperty = dstProperties.First(x => x.Name == srcProperty.Name);

                if (dstProperty.CanWrite)
                {
                    dstProperty.SetValue(levelDescription, srcProperty.GetValue(originalLevelDescription));
                }
            }

            var id = 0;
            var mapping = originalLevelDescription
                .GetGraphWithoutCorridors()
                .Vertices
                .Select(x => (x, new RoomWrapper(id++, x.GetDisplayName())))
                .ToDictionary(x => x.x, x => x.Item2);

            foreach (var pair in mapping)
            {
                levelDescription.AddRoom(pair.Value, originalLevelDescription.GetRoomDescription(pair.Key));
            }

            foreach (var edge in originalLevelDescription.GetGraphWithoutCorridors().Edges)
            {
                var from = mapping[edge.From];
                var to = mapping[edge.To];

                levelDescription.AddConnection(from, to);
            }

            return levelDescription;
        }

        private struct RoomWrapper
        {
            public int Id { get; }

            public string Name { get; }

            public RoomWrapper(int id, string name)
            {
                Name = name;
                Id = id;
            }

            public bool Equals(RoomWrapper other)
            {
                return Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                return obj is RoomWrapper other && Equals(other);
            }

            public override int GetHashCode()
            {
                return Id;
            }

            public static bool operator ==(RoomWrapper left, RoomWrapper right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(RoomWrapper left, RoomWrapper right)
            {
                return !left.Equals(right);
            }
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

            if (version < 3)
            {
                if (version <= 1)
                {
                    PostProcessConfig.TilemapLayersStructure = TilemapLayersStructureModeGrid2D.Default;
                }
                else
                {
                    if (PostProcessConfig.TilemapLayersHandler != null)
                    {
                        PostProcessConfig.TilemapLayersStructure = TilemapLayersStructureModeGrid2D.Custom;
                    }
                    else
                    {
                        PostProcessConfig.TilemapLayersStructure = TilemapLayersStructureModeGrid2D.Default;
                    }
                }
            }
            #pragma warning restore 618

            return 3;
        }
    }
}