namespace Assets.Scripts.DungeonGenerators.Editor
{
	using UnityEditor;
	using UnityEditorInternal;
	using UnityEngine;

	[CustomEditor(typeof(DungeonGeneratorPipeline))]
	public class DungeonGeneratorPipelineInspector : Editor
	{
		private ReorderableList list;
		private bool showAdvancedSettings;

		private void OnEnable()
		{
			var script = target as DungeonGeneratorPipeline;

			list = new ReorderableList(serializedObject,
				serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PipelineItems)),
				true, true, true, true);

			list.drawElementCallback =
				(Rect rect, int index, bool isActive, bool isFocused) =>
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

				return height;
			};
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();

			var dungeonGeneratorPipeline = (DungeonGeneratorPipeline) target;

			if (GUILayout.Button("Generate"))
			{
				dungeonGeneratorPipeline.Generate();
			}

			showAdvancedSettings = EditorGUILayout.Foldout(showAdvancedSettings, "Advanced settings");

			if (showAdvancedSettings)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PayloadGenerator)));
			}
		}
	}
}