using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Editor
{
    [CustomEditor(typeof(DungeonGeneratorRunner))]
    public class DungeonGeneratorRunnerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var dungeonGenerator = (DungeonGeneratorRunner) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.FixedLevelGraphConfig)));

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