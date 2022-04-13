using System.IO;
using UnityEditorInternal;
using UnityEngine;
#if UNITY_2020_1_OR_NEWER
using UnityEditor;
#endif

namespace Edgar.Unity.Editor
{
    #if UNITY_2020_1_OR_NEWER
    public class EdgarSettings : ScriptableSingleton<EdgarSettings>
    #else
    public class EdgarSettings : EdgarScriptableSingleton<EdgarSettings>
        #endif
    {
        internal const string FilePath = "EdgarSettings.asset";
        internal const string FullPath = "ProjectSettings/" + FilePath;

        private void OnEnable()
        {
            hideFlags &= ~HideFlags.NotEditable;
        }

        public void Save()
        {
            #if UNITY_2020_1_OR_NEWER
            Save(True);
            #else
            var path = FullPath;
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                if (directoryName != null) Directory.CreateDirectory(directoryName);
            }

            InternalEditorUtility.SaveToSerializedFileAndForget(new Object[]
            {
                instance
            }, path, true);
            #endif
        }
    }
}