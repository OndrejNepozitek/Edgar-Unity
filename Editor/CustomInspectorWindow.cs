using System;
using UnityEditor;

namespace Edgar.Unity.Editor
{
    public class CustomInspectorWindow : EditorWindow
    {
        private UnityEditor.Editor editor;

        public void OnGUI()
        {
            if (editor != null)
            {
                editor.DrawHeader();
                editor.OnInspectorGUI();
                // editor.serializedObject.ApplyModifiedProperties();
            }
        }

        public static void OpenWindow(UnityEngine.Object data)
        {
            var type = Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");
            var window = GetWindow<CustomInspectorWindow>("Custom inspector", type);
            window.editor = UnityEditor.Editor.CreateEditor(data);
            window.Show();
        }
    }
}