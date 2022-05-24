using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [Serializable]
    public class EdgarSettingsGrid2D
    {
        internal class Inspector : EdgarSettingsInspectorBase
        {
            public Inspector(SerializedObject serializedObject) : base(serializedObject, nameof(EdgarSettings.Grid2D))
            {
            }

            public override void OnGUI()
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                Show = EditorGUILayout.Foldout(Show, "Grid2D settings (empty for now)");
                if (Show)
                {

                }
                GUILayout.EndVertical();
            }
        }
    }
}