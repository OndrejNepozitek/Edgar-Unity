using System;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    [CustomEditor(typeof(Doors))]
	public class DoorsInspector : UnityEditor.Editor
    {
        private HybridDoorModeInspector hybridDoorModeInspector;
        private SimpleDoorModeInspector simpleDoorModeInspector;
        private ManualDoorModeInspector manualDoorModeInspector;

        public void OnEnable()
        {
            var doors = (Doors) target;

            hybridDoorModeInspector = new HybridDoorModeInspector(
                serializedObject,
                doors,
                serializedObject.FindProperty(nameof(Doors.HybridDoorModeData)));
            simpleDoorModeInspector = new SimpleDoorModeInspector(
                serializedObject,
                doors);
            manualDoorModeInspector = new ManualDoorModeInspector(
                serializedObject,
                doors,
                serializedObject.FindProperty(nameof(Doors.ManualDoorModeData)));

            SceneView.RepaintAll();
        }

		public void OnSceneGUI()
		{
			var doors = (Doors) target;

            switch (doors.SelectedMode)
			{
				case Doors.DoorMode.Manual:
                    manualDoorModeInspector.OnSceneGUI();
                    break;

				case Doors.DoorMode.Simple:
                    simpleDoorModeInspector.OnSceneGUI();
                    break;

                case Doors.DoorMode.Hybrid:
                    hybridDoorModeInspector.OnSceneGUI();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnInspectorGUI()
		{
			serializedObject.Update();

            var doors = (Doors) target;

            var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid((int) doors.SelectedMode, new[]
            {
                "Simple mode",
                "Manual mode",
                "Hybrid mode"
            }, 3);

            EditorGUILayout.Space();

			switch (doors.SelectedMode)
            {
                case Doors.DoorMode.Simple:
                    simpleDoorModeInspector.OnInspectorGUI();
                    break;

                case Doors.DoorMode.Manual:
                    manualDoorModeInspector.OnInspectorGUI();
                    break;

                case Doors.DoorMode.Hybrid:
                    hybridDoorModeInspector.OnInspectorGUI();
                    break;
            }

            serializedObject.ApplyModifiedProperties();  
		}
    }
}