using Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Editor
{
    [CustomEditor(typeof(PlatformerGeneratorRunner))]
    public class PlatformerGeneratorRunnerInspector : UnityEditor.Editor
    {
        private ReorderableList customPostProcessTasksList;

        public void OnEnable()
        {
            customPostProcessTasksList = new ReorderableList(new UnityEditorInternal.ReorderableList(serializedObject,
                serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.CustomPostProcessTasks)),
                true, true, true, true), "Custom post process tasks");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var generator = (PlatformerGeneratorRunner) target;

            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2f;

            EditorGUILayout.LabelField("Input config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.InputType)));

            // PRO
            switch (generator.InputType)
            {
                case PlatformerGeneratorInputType.CustomInput:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.CustomInputTask)));
                    break;
                case PlatformerGeneratorInputType.FixedLevelGraph:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.FixedLevelGraphConfig)));
                    break;
            }
            

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Generator config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.GeneratorConfig)));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Post processing config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.PostProcessConfig)));
            customPostProcessTasksList.DoLayoutList(); 

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(PlatformerGeneratorRunner.OtherConfig)));

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate platformer"))
            {
                generator.Generate();
            }

            EditorGUIUtility.labelWidth = 0;

            serializedObject.ApplyModifiedProperties();
        }
    }
}