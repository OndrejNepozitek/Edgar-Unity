using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [Serializable]
    public class EdgarSettingsGeneral
    {
        public bool SnapLevelGraphToGrid = true;

        public bool DoubleClickToConfigureRoom = true;

        internal class Inspector : EdgarSettingsInspectorBase
        {
            public Inspector(SerializedObject serializedObject) : base(serializedObject, nameof(EdgarSettings.General))
            {
            }

            public override void OnGUI()
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                Show = EditorGUILayout.Foldout(Show, "General settings");
                if (Show)
                {
                    EditorGUILayout.PropertyField(Property(nameof(SnapLevelGraphToGrid)));
                    EditorGUILayout.PropertyField(Property(nameof(DoubleClickToConfigureRoom)));
                }
                GUILayout.EndVertical();
            }
        }
    }
}