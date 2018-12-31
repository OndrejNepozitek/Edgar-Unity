namespace Assets.Scripts.Pipeline.POC
{
	using UnityEditor;
	using UnityEditorInternal;
	using UnityEngine;
	using Utils;

	[CustomEditor(typeof(PipelineScript))]
	public class PipelineScriptInspector : Editor
	{
		
		private ReorderableList list;

		private void OnEnable()
		{
			var script = target as PipelineScript;

			list = new ReorderableList(serializedObject,
				serializedObject.FindProperty(nameof(PipelineScript.PipelineScripts)),
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
			DrawDefaultInspector();

			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();

			var pipelineScript = (PipelineScript) target;

			if (GUILayout.Button("Execute"))
			{
				pipelineScript.Execute();
			}

			if (GUILayout.Button("Execute new"))
			{
				pipelineScript.Execute2();
			}
		}
	}
}