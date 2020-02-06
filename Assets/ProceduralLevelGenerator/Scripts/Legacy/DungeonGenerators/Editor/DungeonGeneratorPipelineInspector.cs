using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators.Editor
{
    [CustomEditor(typeof(DungeonGeneratorPipeline))]
	public class DungeonGeneratorPipelineInspector : UnityEditor.Editor
	{
		private ReorderableList list;
		private bool showPipelineSettings = true;
		private bool showAdvancedSettings;

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