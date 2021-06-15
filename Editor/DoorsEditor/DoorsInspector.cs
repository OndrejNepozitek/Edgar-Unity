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
            hybridDoorModeInspector = new HybridDoorModeInspector(serializedObject, target as Doors, serializedObject.FindProperty(nameof(Doors.HybridDoorModeData)));
            simpleDoorModeInspector = new SimpleDoorModeInspector(serializedObject, target as Doors);
            manualDoorModeInspector = new ManualDoorModeInspector(serializedObject, target as Doors, serializedObject.FindProperty(nameof(Doors.ManualDoorModeData)));

            SceneView.RepaintAll();
        }

		public void OnSceneGUI()
		{
			var doors = target as Doors;
			
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
            }
        }

        public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var doors = target as Doors;
			
			var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid((int) doors.SelectedMode, new[] { "Simple mode", "Manual mode", "Hybrid mode"}, 3);

            EditorGUILayout.Space();

			if (doors.SelectedMode == Doors.DoorMode.Simple)
			{
                simpleDoorModeInspector.OnInspectorGUI();
			}

			if (doors.SelectedMode == Doors.DoorMode.Manual)
			{
                manualDoorModeInspector.OnInspectorGUI();
            }

            if (doors.SelectedMode == Doors.DoorMode.Hybrid)
            {
                hybridDoorModeInspector.OnInspectorGUI();
            }

            serializedObject.ApplyModifiedProperties();  
		}
    }
}