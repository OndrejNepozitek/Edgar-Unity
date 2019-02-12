namespace Assets.ProceduralLevelGenerator.Editor.LevelGraphEditor
{
	using System;
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(LevelGraph))]
	public class LevelGraphInspector : Editor
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
				// EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.DefaultRoomTemplateSets)), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.DefaultIndividualRoomTemplates)), true);
				EditorGUI.indentLevel--;
			}

			corridorRoomTemplatesFoldout = EditorGUILayout.Foldout(corridorRoomTemplatesFoldout, "Corridor room templates");

			if (corridorRoomTemplatesFoldout)
			{
				EditorGUI.indentLevel++;
				// EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.CorridorRoomTemplateSets)), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.CorridorIndividualRoomTemplates)), true);
				EditorGUI.indentLevel--;
			}

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.RoomsGroups)), true);

			if (GUILayout.Button("Open graph editor"))
			{
				var type = Type.GetType("UnityEditor.GameView,UnityEditor");
				var window = EditorWindow.GetWindow<LevelGraphWindow>("Graph editor", type);
				window.Data = (LevelGraph) target;
				window.Initialize();
				window.Show();
			}

			serializedObject.ApplyModifiedProperties(); 
		}
	}
}