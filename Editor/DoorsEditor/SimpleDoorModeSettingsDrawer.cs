using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomPropertyDrawer(typeof(SimpleDoorModeSettings))]
    public class SimpleDoorModeSettingsDrawer : PropertyDrawer
    {
        private const float SubLabelSpacing = 4;
        private const float BottomSpacing = 2;
        private string[] keywords = new[] {"Top", "Bottom", "Vertical"};

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            // backup gui settings
            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 130;

            pos.height -= BottomSpacing;
            label = EditorGUI.BeginProperty(pos, label, prop);
            var contentRect = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
            var labels = new[] { new GUIContent("Length:"), new GUIContent("Padding left:"), new GUIContent("Padding right:") };

            if (keywords.Any(x => prop.propertyPath.Contains(x)))
            {
                labels = new[] { new GUIContent("Length:"), new GUIContent("Padding bottom:"), new GUIContent("Padding top:") };
            }

            var properties = new[] { prop.FindPropertyRelative("Length"), prop.FindPropertyRelative("Padding1"), prop.FindPropertyRelative("Padding2") };
            DrawMultiplePropertyFields(contentRect, labels, properties);

            EditorGUI.EndProperty();

            // restore gui settings
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + BottomSpacing;
        }

        private static void DrawMultiplePropertyFields(Rect pos, GUIContent[] subLabels, SerializedProperty[] props)
        {
            // backup gui settings
            var indent = EditorGUI.indentLevel;
            var labelWidth = EditorGUIUtility.labelWidth;
            var labelFontSize = EditorStyles.label.fontSize;

            // draw properties
            var propsCount = props.Length;
            var width = (pos.width - (propsCount - 1) * SubLabelSpacing) / propsCount;
            var contentPos = new Rect(pos.x, pos.y, width, pos.height);
            EditorGUI.indentLevel = 0;

            if (pos.width < 350)
            {
                EditorStyles.label.fontSize = 7;
            }

            var widthDiff = 0.3f;

            for (var i = 0; i < propsCount; i++)
            {
                var currentWidth = width;

                if (i == 0)
                {
                    currentWidth = currentWidth * (1 - widthDiff);
                    contentPos = new Rect(pos.x, pos.y, currentWidth, pos.height);
                }
                else
                {
                    currentWidth = currentWidth * (1 + widthDiff/2);
                    contentPos = new Rect(contentPos.x, pos.y, currentWidth, pos.height);
                }

                EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(subLabels[i]).x;

                if (i != 0)
                {
                    if (pos.width < 350)
                    {
                        EditorGUIUtility.labelWidth = 70;
                    }
                    else
                    {
                        EditorGUIUtility.labelWidth = 105;
                    }
                }

                EditorGUI.PropertyField(contentPos, props[i], subLabels[i]);

                contentPos.x += currentWidth + SubLabelSpacing;
            }

            // restore gui settings
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.indentLevel = indent;
            EditorStyles.label.fontSize = labelFontSize;
        }
    }
}