namespace Assets.Scripts.DungeonGenerators.Editor
{
	using UnityEditor;
	using UnityEditorInternal;
	using UnityEngine;

	[CustomEditor(typeof(DungeonGeneratorPipeline))]
	public class DungeonGeneratorPipelineInspector : Editor
	{
		private ReorderableList list;
		private bool showPipelineSettings = true;
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
			//var foldoutStyle = EditorStyles.foldout;
			//var previousStyle = foldoutStyle.fontStyle;
			//foldoutStyle.fontStyle = FontStyle.Bold;

			EditorGUILayout.Space();
			showPipelineSettings = EditorGUILayout.Foldout(showPipelineSettings, "Generator pipeline settings");
			
			if (showPipelineSettings)
			{
				serializedObject.Update();
				list.DoLayoutList();
				serializedObject.ApplyModifiedProperties();
			}

			EditorGUILayout.Space();
			showAdvancedSettings = EditorGUILayout.Foldout(showAdvancedSettings, "Advanced settings");

			if (showAdvancedSettings)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorPipeline.PayloadGenerator)));
				EditorGUI.indentLevel--;
			}

			EditorGUILayout.Space();
			if (GUILayout.Button("Generate"))
			{
				var dungeonGeneratorPipeline = (DungeonGeneratorPipeline)target;
				dungeonGeneratorPipeline.Generate();
			}

			//foldoutStyle.fontStyle = previousStyle;
		}
	}
}