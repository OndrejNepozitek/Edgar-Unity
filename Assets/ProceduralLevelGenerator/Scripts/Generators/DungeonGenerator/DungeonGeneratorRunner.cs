using System.Collections.Generic;
using System.Diagnostics;
using Assets.ProceduralLevelGenerator.Scripts.Attributes;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs;
using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.PipelineTasks;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator
{
    // TODO: is this name ok?
    public class DungeonGeneratorRunner : MonoBehaviour
    {
        public DungeonGeneratorInputType InputType = DungeonGeneratorInputType.FixedLevelGraph;

        [ExpandableNotFoldable]
        public PipelineItem CustomInputTask;

        [ExpandableAttributeNew]
        public FixedLevelGraphConfig FixedLevelGraphConfig;

        [ExpandableAttributeNew]
        public DungeonGeneratorConfig Config;

        [ExpandableAttributeNew]
        public OtherConfig OtherConfig;

        [ExpandableAttributeNew]
        public PostProcessConfig PostProcessConfig;

        private readonly Random seedsGenerator = new Random();

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
            if (InputType == DungeonGeneratorInputType.CustomInput)
            {
                pipelineItems.Add(CustomInputTask);
            }
            else
            {
                var fixedInputPipelineConfig = ScriptableObject.CreateInstance<FixedLevelGraphPipelineConfig>();
                fixedInputPipelineConfig.Config = FixedLevelGraphConfig;
                pipelineItems.Add(fixedInputPipelineConfig);
            }

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

    // TODO: handle UnityEditor references
    [CustomEditor(typeof(DungeonGeneratorRunner))]
    public class DungeonGeneratorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var dungeonGenerator = (DungeonGeneratorRunner) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.InputType)));

            switch (dungeonGenerator.InputType)
            {
                case DungeonGeneratorInputType.FixedLevelGraph:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.FixedLevelGraphConfig)));
                    break;

                case DungeonGeneratorInputType.CustomInput:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.CustomInputTask)));
                    break;
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.Config)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.PostProcessConfig)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.OtherConfig)));

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate dungeon"))
            {
                dungeonGenerator.Generate();
            }

            EditorGUIUtility.labelWidth = 0;

            serializedObject.ApplyModifiedProperties();
        }
    }
}