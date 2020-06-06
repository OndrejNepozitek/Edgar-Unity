using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.LevelGenerators
{
    [CustomEditor(typeof(DungeonGeneratorBase), true)] 
    public class DungeonGeneratorInspector : UnityEditor.Editor 
    {
        private ReorderableList customPostProcessTasksList;

        private static bool advancedFoldout = false;

        public void OnEnable()
        {
            customPostProcessTasksList = new ReorderableList(new UnityEditorInternal.ReorderableList(serializedObject,
                serializedObject.FindProperty(nameof(DungeonGeneratorBase.CustomPostProcessTasks)),
                true, true, true, true), "Custom post process tasks");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var levelGenerator = (DungeonGeneratorBase) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.FixedLevelGraphConfig)));
            
            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.GeneratorConfig)));

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.PostProcessConfig)));
            customPostProcessTasksList.DoLayoutList(); 

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.UseRandomSeed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.RandomGeneratorSeed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DungeonGenerator.GenerateOnStart)));

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

            if (levelGenerator is DungeonGenerator)
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