using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edgar.Unity
{
    #if UNITY_EDITOR
    public class ReorderableList
    {
        private readonly UnityEditorInternal.ReorderableList list;
        private readonly string label;

        public ReorderableList(UnityEditorInternal.ReorderableList list, string label = null)
        {
            this.list = list;
            this.label = label;

            list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                var height = EditorGUI.GetPropertyHeight(element);

                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width, height),
                    element, true);
            };

            list.elementHeightCallback = (index) =>
            {
                if (list.serializedProperty.arraySize == 0)
                {
                    return 0;
                }

                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var height = EditorGUI.GetPropertyHeight(element);

                return height + 10;
            };

            if (label != null)
            {
                list.drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, label);
                };
            }
        }

        public void DoLayoutList()
        {
            list.DoLayoutList();
        }
    }
    #endif
}