using System;
using Edgar.Geometry;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class SimpleDoorModeInspector : IDoorModeInspector
    {
        private readonly SerializedObject serializedObject;
        private readonly Doors doors;

        public SimpleDoorModeInspector(SerializedObject serializedObject, Doors doors)
        {
            this.serializedObject = serializedObject;
            this.doors = doors;
        }

        public void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeData.Mode)));

            if (doors.SimpleDoorModeData.Mode == SimpleDoorModeData.SettingsMode.Basic)
            {
                EditorGUILayout.IntSlider(FindProperty(nameof(SimpleDoorModeData.DoorLength)), 1, 10, "Door length");
                EditorGUILayout.IntSlider(FindProperty(nameof(SimpleDoorModeData.DistanceFromCorners)), 0, 10, "Margin");
            } 
            else if (doors.SimpleDoorModeData.Mode == SimpleDoorModeData.SettingsMode.DifferentHorizontalAndVertical)
            {
                EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeData.VerticalDoors)), true);
                EditorGUILayout.PropertyField(FindProperty(nameof(SimpleDoorModeData.HorizontalDoors)), true);
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