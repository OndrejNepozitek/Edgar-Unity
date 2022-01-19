using System;
using Edgar.Geometry;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class SimpleDoorModeInspector : IDoorModeInspector
    {
        private readonly SerializedObject serializedObject;
        private readonly DoorsGrid2D doors;

        public SimpleDoorModeInspector(SerializedObject serializedObject, DoorsGrid2D doors)
        {
            this.serializedObject = serializedObject;
            this.doors = doors;
        }

        public void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeDataGrid2D.Mode)));

            if (doors.SimpleDoorModeData.Mode == SimpleDoorModeDataGrid2D.SettingsMode.Basic)
            {
                EditorGUILayout.IntSlider(FindProperty(nameof(SimpleDoorModeDataGrid2D.DoorLength)), 1, 10, "Door length");
                EditorGUILayout.IntSlider(FindProperty(nameof(SimpleDoorModeDataGrid2D.DistanceFromCorners)), 0, 10, "Margin");
            }
            else if (doors.SimpleDoorModeData.Mode == SimpleDoorModeDataGrid2D.SettingsMode.DifferentHorizontalAndVertical)
            {
                EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeDataGrid2D.VerticalDoors)), true);
                EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeDataGrid2D.HorizontalDoors)), true);
            }
        }

        private SerializedProperty FindProperty(string name)
        {
            return serializedObject.FindProperty($"{nameof(doors.SimpleDoorModeData)}.{name}");
        }

        public void OnSceneGUI()
        {
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();
            var doorLines = doors.SimpleDoorModeData.GetDoorLines(doors);

            var color = Color.red;

            foreach (var doorLine in doorLines)
            {
                DoorsInspectorUtils.DrawDoorLine(doorLine, grid, color);
            }
        }
    }
}