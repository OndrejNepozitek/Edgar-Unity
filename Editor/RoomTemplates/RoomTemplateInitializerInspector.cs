using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(RoomTemplateInitializerBaseGrid2D), true)]
    public class RoomTemplateInitializerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var roomTemplateInitializer = (RoomTemplateInitializerBaseGrid2D) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Initialize room template"))
            {
                roomTemplateInitializer.Initialize();
                DestroyImmediate(roomTemplateInitializer);
            }
        }
    }
}