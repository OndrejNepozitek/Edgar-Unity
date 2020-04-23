using System;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateOutline;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.RoomTemplates
{
    [CustomEditor(typeof(RoomTemplateSettings))]
    public class RoomTemplateInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI() 
        {
            serializedObject.Update();

            DrawDefaultInspector();

            var roomTemplate = (RoomTemplateSettings) target;

            if (roomTemplate.IsOutlineValid())
            {
                EditorGUILayout.HelpBox("The outline of the room template is valid.", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("The outline of the room template is not valid. Please make sure to follow the rules from the documentation.", MessageType.Error);
            }

            var doors = roomTemplate.GetComponent<Generators.Common.RoomTemplates.Doors.Doors>();

            if (doors == null)
            {
                EditorGUILayout.HelpBox("The Doors component is missing. Please add it to this game object.", MessageType.Error);
            }

            var hasOutlineOverride = roomTemplate.HasOutlineOverride();

            EditorGUILayout.HelpBox($"Using outline override: {hasOutlineOverride}", MessageType.None);

            if (hasOutlineOverride)
            {
                if (GUILayout.Button("Remove outline override", EditorStyles.miniButton))
                {
                    roomTemplate.RemoveOutlineOverride();
                }
            }
            else
            {
                if (GUILayout.Button("Add outline override", EditorStyles.miniButton))
                {
                    roomTemplate.AddOutlineOverride();
                }
            }

            var boundingBoxOutlineHandler = roomTemplate.GetComponent<BoundingBoxOutlineHandler>();
            var boundingBoxRemoved = false;

            if (boundingBoxOutlineHandler == null)
            {
                if (GUILayout.Button("Add bounding box outline handler", EditorStyles.miniButton))
                {
                    roomTemplate.gameObject.AddComponent<BoundingBoxOutlineHandler>();
                    EditorUtility.SetDirty(roomTemplate);
                }
            }
            else
            {
                if (GUILayout.Button("Remove bounding box outline handler", EditorStyles.miniButton))
                {
                    DestroyImmediate(boundingBoxOutlineHandler, true);
                    boundingBoxRemoved = true;
                    EditorUtility.SetDirty(roomTemplate);
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (boundingBoxRemoved)
            {
                GUIUtility.ExitGUI();
            }
        }

        public void OnSceneGUI()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -= OnSceneGUITest;
#else
            SceneView.onSceneGUIDelegate -= OnSceneGUITest;
#endif

            if (PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                
#if UNITY_2019_1_OR_NEWER
                SceneView.duringSceneGui += OnSceneGUITest;
#else
                SceneView.onSceneGUIDelegate += OnSceneGUITest;
#endif
            }
        }

        public void OnSceneGUITest(SceneView sceneView)
        {
            if (target == null)
            {
#if UNITY_2019_1_OR_NEWER
                SceneView.duringSceneGui -= OnSceneGUITest;
#else
                SceneView.onSceneGUIDelegate -= OnSceneGUITest;
#endif
                return;
            }

            if (PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
#if UNITY_2019_1_OR_NEWER
                SceneView.duringSceneGui -= OnSceneGUITest;
#else
                SceneView.onSceneGUIDelegate -= OnSceneGUITest;
#endif
                return;
            }
            
            try
            {
                var roomTemplate = (RoomTemplateSettings) target;
                var outline = roomTemplate.GetOutline();

                if (outline == null)
                {
                    return;
                }

                var points = outline.GetPoints();
                var grid = roomTemplate.GetComponentInChildren<Grid>();

                for (int i = 0; i < points.Count; i++)
                {
                    GridUtils.DrawRectangleOutline(grid, points[i].ToUnityIntVector3(), points[(i + 1) % points.Count].ToUnityIntVector3(), Color.yellow);
                }
            }
            catch (ArgumentException)
            {

            }
        }
    }
}