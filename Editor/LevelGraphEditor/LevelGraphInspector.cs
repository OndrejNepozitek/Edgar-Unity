using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
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
                OpenWindow((LevelGraph) target);
            }

            serializedObject.ApplyModifiedProperties();
        }
        
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        #if UNITY_6000_4_OR_NEWER
        public static bool OnOpenAsset(EntityId instanceID, int line)
        #else
        public static bool OnOpenAsset(int instanceID, int line)
        #endif
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            var assetPath = AssetDatabase.GetAssetPath(instanceID);
            #pragma warning restore CS0618 // Type or member is obsolete
            var levelGraph = AssetDatabase.LoadAssetAtPath<LevelGraph>(assetPath);
        
            if (levelGraph != null)
            {
                OpenWindow(levelGraph);
        
                return true;
            }
        
            return false;
        }

        private static void OpenWindow(LevelGraph levelGraph)
        {
            var type = Type.GetType("UnityEditor.GameView,UnityEditor");
            var window = EditorWindow.GetWindow<LevelGraphEditor>("Graph editor", type);
            window.Initialize(levelGraph);
            window.Show();
        }
    }
}