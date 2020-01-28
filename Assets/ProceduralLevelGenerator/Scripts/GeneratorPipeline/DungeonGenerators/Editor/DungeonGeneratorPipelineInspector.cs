namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.Editor
{
	using DungeonGenerators;
	using UnityEditor;
	using UnityEditorInternal;
	using UnityEngine;

	[CustomEditor(typeof(DungeonGeneratorPipeline))]
	public class DungeonGeneratorPipelineInspector : Editor
	{
		private ReorderableList list;
        private bool showBenchmarks = false;

		private void OnEnable()
		{
			list = new ReorderableList(serializedObject,
				serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PipelineItems)),
				true, true, true, true);

			list.drawElementCallback = (rect, index, isActive, isFocused) =>
			{
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				rect.y += 2;

				var height = EditorGUI.GetPropertyHeight(element);

				EditorGUI.PropertyField(
					new Rect(rect.x, rect.y, rect.width, height),
					element, true);
			};

			list.elementHeightCallback = (index) =>
			{
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				var height = EditorGUI.GetPropertyHeight(element);

				return height + 10;
			};

			list.drawHeaderCallback = rect =>
			{
				EditorGUI.LabelField(rect, "Generator pipeline");
			};
		}

		public override void OnInspectorGUI()
		{
            var pipeline = (DungeonGeneratorPipeline) target;

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.Space();

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PayloadInitializer)));

			EditorGUILayout.Space();

			if (GUILayout.Button("Generate"))
			{
                pipeline.Generate();
			}

            EditorGUILayout.Space();

            showBenchmarks = EditorGUILayout.Foldout(showBenchmarks, "Benchmarks");
            if (showBenchmarks)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.BenchmarkRuns)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.ScreenshotCamera)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.ScreenshotCameraSize)));

                EditorGUI.BeginDisabledGroup(pipeline.BenchmarkIsRunning);
                var buttonText = pipeline.BenchmarkIsRunning ? $"Running - {pipeline.BenchmarkProgress}/{pipeline.BenchmarkRuns}" : "Run benchmark";
                if (GUILayout.Button(buttonText))
                {
                    pipeline.RunBenchmark();
                }
				EditorGUI.EndDisabledGroup();

                if (pipeline.BenchmarkIsRunning)
                {
                    if (GUILayout.Button("Stop benchmark"))
                    {
                        pipeline.StopBenchmark();
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.LevelsToPrecompute)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PrecomputedLevelsHandler)));
            
            if (pipeline.IsPrecomputeRunning)
            {
                EditorGUILayout.LabelField($"State: Running - {pipeline.PrecomputeProgress}/{pipeline.LevelsToPrecompute}");
            }

            if (GUILayout.Button("Precompute levels"))
            {
                pipeline.PrecomputeLevels();
            }

			serializedObject.ApplyModifiedProperties();
		}
	}
}