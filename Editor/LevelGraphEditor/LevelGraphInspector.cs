using System;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGraphEditor
{
    [CustomEditor(typeof(LevelGraph))]
	public class LevelGraphInspector : UnityEditor.Editor
	{
		private bool defaultRoomTemplatesFoldout;
		private bool corridorRoomTemplatesFoldout;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

            var foldoutStyle = new GUIStyle(EditorStyles.foldout) {fontStyle = FontStyle.Bold};

            defaultRoomTemplatesFoldout = EditorGUILayout.Foldout(defaultRoomTemplatesFoldout, "Default room templates", foldoutStyle);

			if (defaultRoomTemplatesFoldout)
			{
				EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty(nameof(LevelGraph.DefaultIndividualRoomTemplates)),
                    new GUIContent("Room Templates"),
                    true);
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty(nameof(LevelGraph.DefaultRoomTemplateSets)),
                    new GUIContent("Room Templates Sets"),
                    true);
				EditorGUI.indentLevel--;
			}

			corridorRoomTemplatesFoldout = EditorGUILayout.Foldout(corridorRoomTemplatesFoldout, "Corridor room templates", foldoutStyle);

			if (corridorRoomTemplatesFoldout)
			{
				EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty(
                    nameof(LevelGraph.CorridorIndividualRoomTemplates)),
                    new GUIContent("Room Templates"),
                    true);
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty(nameof(LevelGraph.CorridorRoomTemplateSets)),
                    new GUIContent("Room Templates Sets"),
                    true);
				EditorGUI.indentLevel--;
			}

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