using UnityEditor;

namespace Edgar.Unity.Editor
{
    internal abstract class EdgarSettingsInspectorBase
    {
        protected readonly SerializedObject SerializedObject;
        protected readonly string SectionName;
        protected bool Show = false;

        protected EdgarSettingsInspectorBase(SerializedObject serializedObject, string sectionName)
        {
            SerializedObject = serializedObject;
            SectionName = sectionName;
        }

        public abstract void OnGUI();

        protected SerializedProperty Property(string name)
        {
            return SerializedObject.FindProperty($"{SectionName}.{name}");
        }
    }
}