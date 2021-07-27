using System.Linq;
using Edgar.Geometry;
using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class HybridDoorModeInspector : ManualDoorModeInspectorBase
    {
        public HybridDoorModeInspector(SerializedObject serializedObject, Doors doors, SerializedProperty serializedProperty) : base(serializedObject, doors, serializedProperty)
        {
        }

//        public override void OnInspectorGUI()
//        {
//            base.OnInspectorGUI();

//            var text = @"
//<size=12>How does the <b>Hybrid mode</b> work?</size>
//1. Select the length of doors in the 'New door length' field.
//2. Click the 'Add door positions' button.
//3. Click on the tile where the doors can start.
//4. Drag the mouse to last possible tile of the door line.

//<b>What if the door disappears?</b> The door line you drew is probably too short.
//<b>What is the solid outline?</b> It shows the length of a single door.
//<b>What is the dashed outline?</b> It shows the all the possible positions where the doors can be.
//";

//            var style = new GUIStyle(EditorStyles.helpBox) {richText = true};
//            EditorGUILayout.TextArea(text.Trim(), style);
//            //EditorGUILayout.HelpBox("How does the <b>Hybrid mode</b> work?", MessageType.Info);
//        }

        protected override void DrawAllDoors()
        {
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

            var color = Color.red;

            foreach (var doorLine in doors.HybridDoorModeData.DoorLines)
            {
                DoorsInspectorUtils.DrawDoorLine(doorLine, grid, color);
            }
        }

        protected override void RemoveDoor(Vector3Int position)
        {
            for (int i = doors.HybridDoorModeData.DoorLines.Count - 1; i >= 0; i--)
            {
                var door = doors.HybridDoorModeData.DoorLines[i];
                var orthogonalLine = new OrthogonalLine(door.From, door.To);

                if (orthogonalLine.Contains(position) != -1)
                {
                    Undo.RecordObject(doors, "Deleted door position");
                    doors.HybridDoorModeData.DoorLines.RemoveAt(i);
                    EditorUtility.SetDirty(doors);
                }
            }
        }

        protected override void DrawPreview(Vector3Int from, Vector3Int to)
        {
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();
            var length = doors.HybridDoorModeData.DefaultLength;
            var doorLine = new DoorLine()
            {
                From = from,
                To = to,
                Length = length,
            };

            var color = Color.red;

            DoorsInspectorUtils.DrawDoorLine(doorLine, grid, color);
        }

        protected override void AddDoor(Vector3Int from, Vector3Int to)
        {
            var length = doors.HybridDoorModeData.DefaultLength;
            var doorLine = new DoorLine()
            {
                From = from,
                To = to,
                Length = length,
            };
            var line = new OrthogonalLineGrid2D(from.ToCustomIntVector2(), to.ToCustomIntVector2());

            if (doors.HybridDoorModeData.DoorLines.Any(x => x == doorLine))
            {
                return;
            }

            if (line.Length >= length - 1)
            {
                Undo.RecordObject(doors, "Added door positions");

                doors.HybridDoorModeData.DoorLines.Add(doorLine);

                EditorUtility.SetDirty(doors);
            }
        }

        protected override void DeleteAllDoors()
        {
            Undo.RecordObject(doors, "Delete all door positions");

            doors.HybridDoorModeData.DoorLines.Clear();

            EditorUtility.SetDirty(doors);
        }

        protected override void ShowAdditionalFields()
        {
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative(nameof(HybridDoorModeData.DefaultLength)), new GUIContent("New door length"));
        }
    }
}