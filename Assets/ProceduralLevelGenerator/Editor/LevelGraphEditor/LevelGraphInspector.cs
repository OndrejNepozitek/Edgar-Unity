using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

namespace Assets.ProceduralLevelGenerator.Editor.LevelGraphEditor
{
	using System;
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
            
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.RoomType)), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(LevelGraph.ConnectionType)), true);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Custom room and connection types", EditorStyles.boldLabel);

            var derivedRoomTypes = ProUtils.FindDerivedTypes(typeof(Room));
            var currentRoomType = serializedObject.FindProperty(nameof(LevelGraph.RoomType)).stringValue;
            var selectedRoomIndex = derivedRoomTypes.FindIndex(x => x.FullName == currentRoomType);
            selectedRoomIndex = selectedRoomIndex == -1 ? derivedRoomTypes.IndexOf(typeof(Room)) : selectedRoomIndex;
            var roomOptions = derivedRoomTypes.Select(x => $"{x.Name} ({x.Namespace})").ToArray();
            selectedRoomIndex = EditorGUILayout.Popup("Room type", selectedRoomIndex, roomOptions);
            serializedObject.FindProperty(nameof(LevelGraph.RoomType)).stringValue = derivedRoomTypes[selectedRoomIndex].FullName;

            var derivedConnectionTypes = ProUtils.FindDerivedTypes(typeof(Connection));
            var currentConnectionType = serializedObject.FindProperty(nameof(LevelGraph.ConnectionType)).stringValue;
            var selectedConnectionIndex = derivedConnectionTypes.FindIndex(x => x.FullName == currentConnectionType);
            selectedConnectionIndex = selectedConnectionIndex == -1 ? derivedConnectionTypes.IndexOf(typeof(Connection)) : selectedConnectionIndex;
            var connectionOptions = derivedConnectionTypes.Select(x => $"{x.Name} ({x.Namespace})").ToArray();
            selectedConnectionIndex = EditorGUILayout.Popup("Connection type", selectedConnectionIndex, connectionOptions);
            serializedObject.FindProperty(nameof(LevelGraph.ConnectionType)).stringValue = derivedConnectionTypes[selectedConnectionIndex].FullName;

            if (derivedRoomTypes[selectedRoomIndex] == typeof(Room) && derivedConnectionTypes[selectedConnectionIndex] == typeof(Connection))
            {
                var warningStyle = new GUIStyle(EditorStyles.boldLabel) {wordWrap = true};
                EditorGUILayout.LabelField("Warning! Default room or connection types are selected. It's not possible to change this easily after the level graph is created", warningStyle);
            }

            EditorGUILayout.Space();

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