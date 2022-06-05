using UnityEngine;
#if UNITY_2020_1_OR_NEWER
using UnityEditor;
#endif

namespace Edgar.Unity.Editor
{
#if UNITY_2020_1_OR_NEWER
    [FilePath(EdgarSettings.FilePath, FilePathAttribute.Location.ProjectFolder)]
    public class EdgarSettings : ScriptableSingleton<EdgarSettings>
#else
    public class EdgarSettings : EdgarScriptableSingleton<EdgarSettings>
        #endif
    {
        internal const string FilePath = "ProjectSettings/EdgarSettings.asset";

        public EdgarSettingsGeneral General;

        public EdgarSettingsGrid2D Grid2D;

        private void OnEnable()
        {
            hideFlags &= ~HideFlags.NotEditable;
        }

        public void Save()
        {
            Save(true);
        }
    }
}