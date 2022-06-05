using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public static class EditorUtils
    {
        #if OndrejNepozitekEdgar
        [MenuItem("Edgar debug/Sync build scenes")]
        public static void SyncBuildScenes()
        {
            #if UNITY_EDITOR
            var scenes = new List<EditorBuildSettingsScene>();
            var guids = AssetDatabase.FindAssets("t:Scene", new[] {"Assets/Edgar/Examples"});
            if (guids != null)
            {
                foreach (string guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        var scene = new EditorBuildSettingsScene(path, true);
                        scenes.Add(scene);
                    }
                }
            }

            Debug.Log("Adding scenes to build settings:\n" + string.Join("\n", scenes.Select(scene => scene.path)));
            EditorBuildSettings.scenes = scenes.ToArray();
            #endif
        }
        #endif
    }
}