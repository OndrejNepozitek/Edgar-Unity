namespace Assets.Editor.LayoutGraphEditor
{
	using System;
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(LayoutGraph))]
	public class LayoutGraphInspector : Editor
	{
		private bool defaultRoomTemplatesFoldout;
		private bool corridorRoomTemplatesFoldout;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			defaultRoomTemplatesFoldout = EditorGUILayout.Foldout(defaultRoomTemplatesFoldout, "Default room templates");

			if (defaultRoomTemplatesFoldout)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LayoutGraph.DefaultRoomTemplateSets)), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LayoutGraph.DefaultIndividualRoomTemplates)), true);
				EditorGUI.indentLevel--;
			}

			corridorRoomTemplatesFoldout = EditorGUILayout.Foldout(corridorRoomTemplatesFoldout, "Corridor room templates");

			if (corridorRoomTemplatesFoldout)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LayoutGraph.CorridorRoomTemplateSets)), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LayoutGraph.CorridorIndividualRoomTemplate)), true);
				EditorGUI.indentLevel--;
			}

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LayoutGraph.RoomsGroups)), true);

			if (GUILayout.Button("Open graph editor"))
			{
				var type = Type.GetType("UnityEditor.GameView,UnityEditor");
				var window = EditorWindow.GetWindow<LayoutGraphWindow>("Graph editor", type);
				window.Data = (LayoutGraph) target;
				window.Initialize();
				window.Show();
			}

			serializedObject.ApplyModifiedProperties(); 
		}
	}
}