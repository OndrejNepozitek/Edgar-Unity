using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(DoorsGrid2D))]
    public class DoorsInspector : UnityEditor.Editor
    {
        private HybridDoorModeInspector hybridDoorModeInspector;
        private SimpleDoorModeInspector simpleDoorModeInspector;
        private ManualDoorModeInspector manualDoorModeInspector;

        public void OnEnable()
        {
            var doors = (DoorsGrid2D) target;

            hybridDoorModeInspector = new HybridDoorModeInspector(
                serializedObject,
                doors,
                serializedObject.FindProperty(nameof(DoorsGrid2D.HybridDoorModeData)));
            simpleDoorModeInspector = new SimpleDoorModeInspector(
                serializedObject,
                doors);
            manualDoorModeInspector = new ManualDoorModeInspector(
                serializedObject,
                doors,
                serializedObject.FindProperty(nameof(DoorsGrid2D.ManualDoorModeData)));

            SceneView.RepaintAll();
        }

        public void OnSceneGUI()
        {
            var doors = (DoorsGrid2D) target;

            switch (doors.SelectedMode)
            {
                case DoorsGrid2D.DoorMode.Manual:
                    manualDoorModeInspector.OnSceneGUI();
                    break;

                case DoorsGrid2D.DoorMode.Simple:
                    simpleDoorModeInspector.OnSceneGUI();
                    break;

                case DoorsGrid2D.DoorMode.Hybrid:
                    hybridDoorModeInspector.OnSceneGUI();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var doors = (DoorsGrid2D) target;

            var selectedModeProp = serializedObject.FindProperty(nameof(DoorsGrid2D.SelectedMode));
            selectedModeProp.intValue = GUILayout.SelectionGrid((int) doors.SelectedMode, new[]
            {
                "Simple mode",
                "Manual mode",
                "Hybrid mode"
            }, 3);

            EditorGUILayout.Space();

            switch (doors.SelectedMode)
            {
                case DoorsGrid2D.DoorMode.Simple:
                    simpleDoorModeInspector.OnInspectorGUI();
                    break;

                case DoorsGrid2D.DoorMode.Manual:
                    manualDoorModeInspector.OnInspectorGUI();
                    break;

                case DoorsGrid2D.DoorMode.Hybrid:
                    hybridDoorModeInspector.OnInspectorGUI();
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}