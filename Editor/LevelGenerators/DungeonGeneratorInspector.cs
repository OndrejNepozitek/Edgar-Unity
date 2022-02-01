using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(DungeonGeneratorBaseGrid2D), true)]
    public class DungeonGeneratorInspector : UnityEditor.Editor
    {
        private ReorderableList customPostProcessTasksList;

        private static bool advancedFoldout = false;

        public void OnEnable()
        {
            customPostProcessTasksList = new ReorderableList(new UnityEditorInternal.ReorderableList(serializedObject,
                serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.CustomPostProcessTasks)),
                true, true, true, true), "Custom post process tasks");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var levelGenerator = (DungeonGeneratorBaseGrid2D) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.FixedLevelGraphConfig)));

            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.GeneratorConfig)));

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.PostProcessConfig)));

            if (levelGenerator.DisableCustomPostProcessing)
            {
                EditorGUILayout.HelpBox($"Custom post-processing tasks are temporarily disabled. Uncheck the \"{nameof(DungeonGeneratorBaseGrid2D.DisableCustomPostProcessing)}\" checkbox to enable them again.", MessageType.Warning);
            }
            else
            {
                customPostProcessTasksList.DoLayoutList();
            }

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.UseRandomSeed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.RandomGeneratorSeed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.GenerateOnStart)));

            EditorGUILayout.HelpBox("If you have problems with the performance of the generator, you can enable diagnostics what will run after a level is generated and print results to the console. Do not use in production.", MessageType.Info);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.EnableDiagnostics)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorBaseGrid2D.DisableCustomPostProcessing)));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            advancedFoldout = EditorGUILayout.Foldout(advancedFoldout, "Advanced");
            if (advancedFoldout)
            {
                if (GUILayout.Button("Export map description"))
                {
                    levelGenerator.ExportMapDescription();
                }
            }

            EditorGUILayout.Space();

            if (levelGenerator is DungeonGeneratorGrid2D)
            {
                if (GUILayout.Button("Generate level"))
                {
                    levelGenerator.Generate();
                }
            }

            EditorGUIUtility.labelWidth = 0;

            serializedObject.ApplyModifiedProperties();
        }
    }
}