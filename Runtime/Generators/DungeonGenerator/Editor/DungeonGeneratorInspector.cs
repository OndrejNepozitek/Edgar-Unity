using ProceduralLevelGenerator.Unity.Utils;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Editor
{
    [CustomEditor(typeof(DungeonGenerator))]
    public class DungeonGeneratorInspector : UnityEditor.Editor
    {
        private ReorderableList customPostProcessTasksList;

        private static bool advancedFoldout = false;

        public void OnEnable()
        {
            customPostProcessTasksList = new ReorderableList(new UnityEditorInternal.ReorderableList(serializedObject,
                serializedObject.FindProperty(nameof(DungeonGenerator.CustomPostProcessTasks)),
                true, true, true, true), "Custom post process tasks");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var dungeonGenerator = (DungeonGenerator) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.FixedLevelGraphConfig)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.GeneratorConfig)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.PostProcessConfig)));
            customPostProcessTasksList.DoLayoutList(); 

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.OtherConfig)));

            EditorGUILayout.Space();

            advancedFoldout = EditorGUILayout.Foldout(advancedFoldout, "Advanced");
            if (advancedFoldout)
            {
                if (GUILayout.Button("Export map description"))
                {
                    dungeonGenerator.ExportMapDescription();
                }
            }

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