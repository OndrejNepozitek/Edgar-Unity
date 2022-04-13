using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Edgar.Unity.Editor
{
    public class EdgarSettingsProvider : SettingsProvider
    {
        SerializedObject serializedObject;

        public EdgarSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            EdgarSettings.instance.Save();
            serializedObject = new SerializedObject(EdgarSettings.instance);
        }

        public override void OnGUI(string searchContext)
        {
            using (CreateSettingsWindowGUIScope())
            {
                serializedObject.Update();
                EditorGUI.BeginChangeCheck();

                //EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(EdgarSettings.Test3)));

                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    EdgarSettings.instance.Save();
                }
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateMySingletonProvider()
        {
            var provider = new EdgarSettingsProvider("Project/Edgar", SettingsScope.Project);
            return provider;
        }

        private IDisposable CreateSettingsWindowGUIScope()
        {
            var unityEditorAssembly = Assembly.GetAssembly(typeof(EditorWindow));
            var type = unityEditorAssembly.GetType("UnityEditor.SettingsWindow+GUIScope");
            return Activator.CreateInstance(type) as IDisposable;
        }
    }
}