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
        private SerializedObject serializedObject;
        private EdgarSettingsGrid2D.Inspector inspectorGrid2D;
        private EdgarSettingsGeneral.Inspector inspectorGeneral;

        public EdgarSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            EdgarSettings.instance.Save();
            serializedObject = new SerializedObject(EdgarSettings.instance);
            inspectorGrid2D = new EdgarSettingsGrid2D.Inspector(serializedObject);
            inspectorGeneral = new EdgarSettingsGeneral.Inspector(serializedObject);
        }

        public override void OnGUI(string searchContext)
        {
            using (CreateSettingsWindowGUIScope())
            {
                serializedObject.Update();
                EditorGUI.BeginChangeCheck();

                inspectorGeneral.OnGUI();
                inspectorGrid2D.OnGUI();

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
            var provider = new EdgarSettingsProvider("Preferences/Edgar", SettingsScope.User);
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