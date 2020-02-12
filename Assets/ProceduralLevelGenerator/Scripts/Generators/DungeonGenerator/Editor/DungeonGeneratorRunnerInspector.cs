using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Editor
{
    [CustomEditor(typeof(DungeonGeneratorRunner))]
    public class DungeonGeneratorRunnerInspector : UnityEditor.Editor
    {
        private ReorderableList customPostProcessTasksList;

        private static bool advancedFoldout = false;

        public void OnEnable()
        {
            customPostProcessTasksList = new ReorderableList(new UnityEditorInternal.ReorderableList(serializedObject,
                serializedObject.FindProperty(nameof(DungeonGeneratorRunner.CustomPostProcessTasks)),
                true, true, true, true), "Custom post process tasks");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var dungeonGenerator = (DungeonGeneratorRunner) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.InputType)));

            // PRO
            switch (dungeonGenerator.InputType)
            {
                case DungeonGeneratorInputType.CustomInput:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.CustomInputTask)));
                    break;
                case DungeonGeneratorInputType.FixedLevelGraph:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.FixedLevelGraphConfig)));
                    break;
            }
            

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.GeneratorConfig)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.PostProcessConfig)));
            customPostProcessTasksList.DoLayoutList(); 

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGeneratorRunner.OtherConfig)));

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