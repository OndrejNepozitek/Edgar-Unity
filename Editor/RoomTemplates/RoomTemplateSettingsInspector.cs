using System;
using System.Text;
using Edgar.Unity.Diagnostics;
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(RoomTemplateSettingsGrid2D))]
    public class RoomTemplateSettingsInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            var roomTemplate = (RoomTemplateSettingsGrid2D) target;
            var validityCheck = RoomTemplateDiagnostics.CheckAll(roomTemplate.gameObject);

            if (!validityCheck.HasErrors)
            {
                EditorGUILayout.HelpBox("The room template is valid.", MessageType.Info);

                var wrongManualDoorsCheck =
                    RoomTemplateDiagnostics.CheckWrongManualDoors(roomTemplate.gameObject, out var _);

                if (wrongManualDoorsCheck.HasErrors)
                {
                    EditorGUILayout.HelpBox(string.Join("\n", wrongManualDoorsCheck.Errors).Trim(), MessageType.Warning);
                }

                var wrongPositionGameObjects = RoomTemplateDiagnostics.CheckWrongPositionGameObjects(roomTemplate.gameObject);
                if (wrongPositionGameObjects.HasErrors)
                {
                    EditorGUILayout.HelpBox(string.Join("\n", wrongPositionGameObjects.Errors).Trim(), MessageType.Warning);
                }
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("There are some problems with the room template:");

                var errors = string.Join("\n", validityCheck.Errors);
                sb.Append(errors);

                EditorGUILayout.HelpBox(sb.ToString(), MessageType.Error);
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

            var boundingBoxOutlineHandler = roomTemplate.GetComponent<BoundingBoxOutlineHandlerGrid2D>();
            var boundingBoxRemoved = false;

            if (boundingBoxOutlineHandler == null)
            {
                if (GUILayout.Button("Add bounding box outline handler", EditorStyles.miniButton))
                {
                    roomTemplate.gameObject.AddComponent<BoundingBoxOutlineHandlerGrid2D>();
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

        private void OnSceneGUI()
        {
            RemoveOnSceneGUIDelegate();
            AddOnSceneGUIDelegate();
        }

        private void OnSceneGUIPersistent(SceneView sceneView)
        {
            if (target == null || PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                RemoveOnSceneGUIDelegate();
                return;
            }

            ShowStatus();
            DrawOutline();
        }

        private void ShowStatus()
        {
            var roomTemplate = (RoomTemplateSettingsGrid2D) target;
            var originalBackground = GUI.backgroundColor;

            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(10, 10, 180, 100));
            GUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Room template status", EditorStyles.boldLabel);

            var isOutlineValid = roomTemplate.GetOutline() != null;
            var outlineText = isOutlineValid ? "valid" : "<color=#870526ff>invalid</color>";
            var areDoorsValid = false;
            var doorsText = "N/A";

            if (isOutlineValid)
            {
                var doorsCheck = RoomTemplateDiagnostics.CheckDoors(roomTemplate.gameObject);
                areDoorsValid = !doorsCheck.HasErrors;
                doorsText = !doorsCheck.HasErrors ? "valid" : "<color=#870526ff>invalid</color>";

                if (areDoorsValid)
                {
                    var wrongManualDoorsCheck = RoomTemplateDiagnostics.CheckWrongManualDoors(roomTemplate.gameObject, out var _);

                    if (wrongManualDoorsCheck.HasErrors)
                    {
                        areDoorsValid = false;
                        doorsText += $" <size=9><color=orange>(with warning)</color></size>";
                    }
                }
            }

            GUILayout.Label($"Outline: <b>{outlineText}</b>", new GUIStyle(EditorStyles.label) {richText = true});
            GUILayout.Label($"Doors: <b>{doorsText}</b>", new GUIStyle(EditorStyles.label) {richText = true});

            if (!isOutlineValid || !areDoorsValid)
            {
                GUILayout.Label($"<size=9>See the Room template settings component for details</size>", new GUIStyle(EditorStyles.label) {richText = true, wordWrap = true});
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
            Handles.EndGUI();

            GUI.backgroundColor = originalBackground;
        }

        private void DrawOutline()
        {
            try
            {
                var roomTemplate = (RoomTemplateSettingsGrid2D) target;
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

        private void AddOnSceneGUIDelegate()
        {
            #if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += OnSceneGUIPersistent;
            #else
            SceneView.onSceneGUIDelegate += OnSceneGUIPersistent;
            #endif
        }

        private void RemoveOnSceneGUIDelegate()
        {
            #if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -= OnSceneGUIPersistent;
            #else
            SceneView.onSceneGUIDelegate -= OnSceneGUIPersistent;
            #endif
        }
    }
}