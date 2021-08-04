using UnityEditor;
using UnityEngine;

namespace Edgar.Unity.Editor
{
    public class ManualDoorModeInspector : ManualDoorModeInspectorBase
    {
        public ManualDoorModeInspector(SerializedObject serializedObject, DoorsGrid2D doors, SerializedProperty serializedProperty) : base(serializedObject, doors, serializedProperty)
        {
        }

        protected override void DeleteAllDoors()
        {
            Undo.RecordObject(doors, "Delete all door positions");

            doors.ManualDoorModeData.DoorsList.Clear();

            EditorUtility.SetDirty(doors);
        }

        protected override void DrawAllDoors()
        {
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

            foreach (var door in doors.ManualDoorModeData.DoorsList)
            {
                DrawDoor(grid, door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3());
            }
        }

        protected override void RemoveDoor(Vector3Int position)
        {
            for (int i = doors.ManualDoorModeData.DoorsList.Count - 1; i >= 0; i--)
            {
                var door = doors.ManualDoorModeData.DoorsList[i];
                var orthogonalLine = new OrthogonalLine(door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3());

                if (orthogonalLine.Contains(position) != -1)
                {
                    Undo.RecordObject(doors, "Deleted door position");
                    doors.ManualDoorModeData.DoorsList.RemoveAt(i);
                    EditorUtility.SetDirty(doors);
                }
            }
        }

        protected override void DrawPreview(Vector3Int from, Vector3Int to)
        {
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();
            DrawDoor(grid, from, to);
        }

        private void DrawDoor(Grid grid, Vector3Int from, Vector3Int to)
        {
            var length = new OrthogonalLine(from, to).Length;
            var doorLine = new DoorLineGrid2D()
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
            var newDoor = new DoorGrid2D()
            {
                From = from,
                To = to,
            };

            if (!doors.ManualDoorModeData.DoorsList.Contains(newDoor))
            {
                Undo.RecordObject(doors, "Added door position");

                doors.ManualDoorModeData.DoorsList.Add(newDoor);

                EditorUtility.SetDirty(doors);
            }
        }
    }
}