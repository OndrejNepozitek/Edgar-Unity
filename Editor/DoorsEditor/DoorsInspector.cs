using System;
using MapGeneration.Core.Doors;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Editor.DoorsEditor
{
    [CustomEditor(typeof(Doors))]
	public class DoorsInspector : UnityEditor.Editor
    {
        private Mode currentMode;

        private Vector3Int firstPoint;

		private bool hasFirstTile;

		private bool hasSecondTile;

        public void OnEnable()
		{
			hasFirstTile = false;
			hasSecondTile = false;
            currentMode = Mode.Idle;

			SceneView.RepaintAll();
        }

		public void OnSceneGUI()
		{
			var doors = target as Doors;
			
			switch (doors.SelectedMode)
			{
				case Doors.DoorMode.Manual:
					HandleManualMode();
					break;

				case Doors.DoorMode.Simple:
					HandleSimpleMode();
					break;
			}
        }

		private void HandleSimpleMode()
		{
			var doors = target as Doors;
			var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

			try
			{
				var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(doors.gameObject);

                if (polygon == null)
                {
                    return;
                }

				foreach (var line in polygon.GetLines())
				{
					if (line.Length - 2 * doors.DistanceFromCorners < doors.DoorLength - 1)
						continue;

					var doorLine = line.Shrink(doors.DistanceFromCorners);
                    var from = doorLine.From;
                    var to = doorLine.To;

					GridUtils.DrawRectangleOutline(grid, from.ToUnityIntVector3(), to.ToUnityIntVector3(), Color.red, new Vector2(0.1f, 0.1f));
				}
			}
			catch (ArgumentException)
			{

			}
		}

		private void HandleManualMode()
		{
			var doors = target as Doors;
            var grid = doors.gameObject.GetComponentInChildren<Grid>();

            // Draw all doors
			foreach (var door in doors.DoorsList)
			{
                GridUtils.DrawRectangleOutline(grid, door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3(), Color.red, new Vector2(0.1f, 0.1f), true);
			}

            switch (currentMode)
            {
                case Mode.AddDoors:
                    HandleAddDoors();
                    break;

                case Mode.DeleteDoors:
                    HandleDeleteDoors();
                    break;
            }
		}

        private void HandleDeleteDoors()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
            var e = Event.current;

            var tilePosition = GetCurrentTilePosition();

            // Make sure that the current active object in the inspector is not deselected
            Selection.activeGameObject = gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
            HandleUtility.AddDefaultControl(controlId);

            if (e.type == EventType.MouseUp)
            {
                for (int i = doors.DoorsList.Count - 1; i >= 0; i--)
                {
                    var door = doors.DoorsList[i];
                    var orthogonalLine = new OrthogonalLine(door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3());

                    if (orthogonalLine.Contains(tilePosition) != -1)
                    {
                        Undo.RecordObject(target, "Deleted door position");
                        doors.DoorsList.RemoveAt(i);
                        EditorUtility.SetDirty(target);
                    }
                }

                Event.current.Use();
            }

            // Mouse down must be also used, otherwise there were some bugs after removing doors
            if (e.type == EventType.MouseDown)
            {
                Event.current.Use();
            }
        }

        private void HandleAddDoors()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
			var e = Event.current;
            var grid = gameObject.GetComponentInChildren<Grid>();


            // Make sure that the current active object in the inspector is not deselected
            Selection.activeGameObject = gameObject;
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
			HandleUtility.AddDefaultControl(controlId);

            // Compute tile position below the mouse cursor
            var tilePosition = GetCurrentTilePosition();

			switch (e.type)
			{
				case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        firstPoint = tilePosition;
                        hasFirstTile = true;
                        hasSecondTile = false;
                        e.Use();
                    }
                    
					break;

				case EventType.MouseUp:
                    if (e.button == 0)
                    {
                        if (hasFirstTile)
                        {
                            hasSecondTile = true;
                        }
                        e.Use();
                    }
                    
					break;
			}

            // If we have the first tile, we can show how would the door look like if we released the mouse button
			if (hasFirstTile)
			{
				var from = firstPoint;
				var to = tilePosition;

				if (from.x != to.x && from.y != to.y)
				{
					to.x = from.x;
				}

                GridUtils.DrawRectangleOutline(grid, from, to, Color.red, new Vector2(0.1f, 0.1f));
                
                // If we also have the second tile, we can complete the door
				if (hasSecondTile)
				{
					hasFirstTile = false;
					hasSecondTile = false;

					var newDoorInfo = new DoorInfoEditor()
					{
						From = from,
						To = to,
					};

					if (!doors.DoorsList.Contains(newDoorInfo))
					{
						Undo.RecordObject(target, "Added door position");

                        doors.DoorsList.Add(newDoorInfo);

						EditorUtility.SetDirty(target);
					}
				}

				SceneView.RepaintAll();
			}
        }

        private Vector3Int GetCurrentTilePosition()
        {
            var doors = target as Doors;
            var gameObject = doors.transform.gameObject;
            var grid = gameObject.GetComponentInChildren<Grid>();

            var mousePosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            mousePosition.z = 0;
            var tilePosition = grid.WorldToCell(mousePosition);
            tilePosition.z = 0;

            return tilePosition;
        }

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var doors = target as Doors;
			
			var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid((int) doors.SelectedMode, new[] { "Simple mode", "Manual mode"}, 2);

            EditorGUILayout.Space();

			if (doors.SelectedMode == Doors.DoorMode.Simple)
			{
                HandleSimpleModeInspector();
			}

			if (doors.SelectedMode == Doors.DoorMode.Manual)
			{
                HandleManualModeInspector();
            }

			serializedObject.ApplyModifiedProperties();  
		}

        private void HandleSimpleModeInspector()
        {
            EditorGUILayout.IntSlider(serializedObject.FindProperty(nameof(Doors.DoorLength)), 1, 10, "Door length");
            EditorGUILayout.IntSlider(serializedObject.FindProperty(nameof(Doors.DistanceFromCorners)), 0, 10, "Corner distance");
        }

        private void HandleManualModeInspector()
        {
            var doors = target as Doors;

            var addDoorsNew = GUILayout.Toggle(currentMode == Mode.AddDoors, "Add door positions", GUI.skin.button);
            var deleteDoorsNew = GUILayout.Toggle(currentMode == Mode.DeleteDoors, "Delete door positions", GUI.skin.button);

            if (addDoorsNew && currentMode != Mode.AddDoors)
            {
                currentMode = Mode.AddDoors;
            } 
            else if (deleteDoorsNew && currentMode != Mode.DeleteDoors)
            {
                currentMode = Mode.DeleteDoors;
            }
            else if (!addDoorsNew && !deleteDoorsNew)
            {
                currentMode = Mode.Idle;
            }

            if (GUILayout.Button("Delete all door positions"))
            {
                Undo.RecordObject(target, "Delete all door positions");

                doors.DoorsList.Clear();

                EditorUtility.SetDirty(target);
            }

            // DrawDoorsList();

            try
            {
                var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(doors.gameObject);
                var doorPositions = DoorHandler.DefaultHandler.GetDoorPositions(polygon, doors.GetDoorMode());

                if (doorPositions.Count != doors.DoorsList.Count)
                {
                    EditorGUILayout.HelpBox(
                        "There seems to be a door of length 1 that is at the corner of the outline, which is currently not supported. Either use outline override to change the outline or remove the door position.",
                        MessageType.Error);
                }
            }
            catch (ArgumentException)
            {

            }
        }

        private void DrawDoorsList()
        {
            var doors = target as Doors;

            GUILayout.BeginVertical();

            foreach (var doorInfo in doors.DoorsList)
            {
                EditorGUILayout.BeginVertical("Box");

                var fromRounded = doorInfo.From.RoundToUnityIntVector3();
                var toRounded = doorInfo.To.RoundToUnityIntVector3();
                
                EditorGUILayout.LabelField($"[{fromRounded.x},{fromRounded.y}]->[{toRounded.x},{toRounded.y}]");
                var newX = EditorGUILayout.IntField(fromRounded.x);

                if (fromRounded.x != newX)
                {
                    doorInfo.From.x = newX;
                    EditorUtility.SetDirty(target);
                }
                
                EditorGUILayout.EndVertical();
            }

            GUILayout.EndVertical();
        }

        private enum Mode
        {
            Idle, AddDoors, DeleteDoors
        }
    }
}