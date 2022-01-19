using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomPropertyDrawer(typeof(SimpleDoorModeSettingsGrid2D))]
    public class SimpleDoorModeSettingsDrawer : PropertyDrawer
    {
        private const float BottomSpacing = 2;
        private const float PropertyHeight = 14;
        private readonly string[] keywords = new[] {"Top", "Bottom", "Vertical"};
        private const float CheckboxOffset = 18;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // backup gui settings
            var labelFontSize = EditorStyles.label.fontSize;

            label = EditorGUI.BeginProperty(position, label, property);

            var labelPosition = position;
            labelPosition.x += CheckboxOffset;
            EditorGUI.PrefixLabel(labelPosition, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.indentLevel++;

            var labels = new[]
            {
                new GUIContent("Length:"),
                new GUIContent("Margin left:"),
                new GUIContent("Margin right:")
            };

            if (keywords.Any(x => property.propertyPath.Contains(x)))
            {
                labels = new[]
                {
                    new GUIContent("Length:"),
                    new GUIContent("Margin bottom:"),
                    new GUIContent("Margin top:")
                };
            }

            var properties = new[]
            {
                property.FindPropertyRelative(nameof(SimpleDoorModeSettingsGrid2D.Length)),
                property.FindPropertyRelative(nameof(SimpleDoorModeSettingsGrid2D.Margin1)),
                property.FindPropertyRelative(nameof(SimpleDoorModeSettingsGrid2D.Margin2)),
            };

            var checkboxRect = new Rect(position.x - CheckboxOffset, position.y, position.width, PropertyHeight);
            var enabledField = property.FindPropertyRelative(nameof(SimpleDoorModeSettingsGrid2D.Enabled));
            EditorGUI.PropertyField(checkboxRect, enabledField, new GUIContent());

            if (enabledField.boolValue)
            {
                var y = position.y;
                EditorStyles.label.fontSize = 10;

                for (int i = 0; i < 3; i++)
                {
                    y += PropertyHeight + BottomSpacing;
                    var contentRect = new Rect(position.x, y, position.width, PropertyHeight);
                    var indentedRect = EditorGUI.IndentedRect(contentRect);

                    EditorGUI.PropertyField(indentedRect, properties[i], labels[i]);
                }
            }

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();

            // restore gui settings
            EditorStyles.label.fontSize = labelFontSize;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var enabledField = property.FindPropertyRelative(nameof(SimpleDoorModeSettingsGrid2D.Enabled));

            if (enabledField.boolValue)
            {
                return base.GetPropertyHeight(property, label) + 3 * (PropertyHeight + BottomSpacing);
            }

            return base.GetPropertyHeight(property, label);
        }
    }
}