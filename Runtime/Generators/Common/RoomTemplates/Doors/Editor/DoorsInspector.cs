using System;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Core.Doors;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors.Editor
{
    [CustomEditor(typeof(Generators.Common.RoomTemplates.Doors.Doors))]
	public class DoorsInspector : UnityEditor.Editor
	{
		private DoorInfoEditor highlightInfo;

		private SerializedProperty doorsLength;

		private SerializedProperty distanceFromCorners;

		private SerializedProperty doorsList;

		private bool addSpecificDoorPositions;
		private bool deleteDoorPositions;

		private Vector3 firstPoint;

		private bool hasFirstPoint;

		private bool hasSecondPoint;

        public void OnEnable()
		{
			doorsLength = serializedObject.FindProperty(nameof(Doors.DoorLength));
			distanceFromCorners = serializedObject.FindProperty(nameof(Doors.DistanceFromCorners));
			doorsList = serializedObject.FindProperty(nameof(Doors.DoorsList));
			addSpecificDoorPositions = false;
			deleteDoorPositions = false;
			hasFirstPoint = false;
			hasSecondPoint = false;
			highlightInfo = null;
			SceneView.RepaintAll();
		}

		public void OnSceneGUI()
		{
			var doors = target as Doors;
			
			switch (doors.SelectedMode)
			{
				case 1:
					DrawSpecifPositions();
					break;

				case 0:
					DrawOverlap();
					break;
			}
        }

		private void DrawOverlap()
		{
			var doors = target as Doors;
			var go = doors.transform.gameObject;

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

					DrawOutline(new Vector3(Math.Min(from.X, to.X), Math.Min(from.Y, to.Y)), new Vector3(Math.Max(from.X, to.X), Math.Max(from.Y, to.Y)), Color.red);
				}
			}
			catch (ArgumentException)
			{

			}
		}

		private void DrawSpecifPositions()
		{
			var doors = target as Doors;

			foreach (var door in doors.DoorsList)
			{
				DrawOutline(door.From, door.To, Color.red);
			}

			if (highlightInfo != null)
			{
				DrawOutline(highlightInfo.From, highlightInfo.To, Color.yellow);
			}

            if (deleteDoorPositions)
            {
                var go = doors.transform.gameObject;
                var e = Event.current;

                Selection.activeGameObject = go;

                var mouseWorldPosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
                mouseWorldPosition -= doors.transform.position;
                mouseWorldPosition.z = 0;
                mouseWorldPosition.x = (float)Math.Floor(mouseWorldPosition.x);
                mouseWorldPosition.y = (float)Math.Floor(mouseWorldPosition.y);

                var controlId = GUIUtility.GetControlID(FocusType.Passive);
                HandleUtility.AddDefaultControl(controlId);

                if (e.type == EventType.MouseUp)
                {
                    for (int i = doors.DoorsList.Count - 1; i >= 0; i--)
                    {
                        var door = doors.DoorsList[i];
                        var orthogonalLine = new OrthogonalLine(door.From.RoundToUnityIntVector3(), door.To.RoundToUnityIntVector3());

                        if (orthogonalLine.Contains(mouseWorldPosition.RoundToUnityIntVector3()) != -1)
                        {
                            Undo.RecordObject(target, "Deleted door position");
                            doors.DoorsList.RemoveAt(i);
                            EditorUtility.SetDirty(target);
                        }
                    }

                    Event.current.Use();
                }
            }

			if (addSpecificDoorPositions)
			{
				var go = doors.transform.gameObject;
				var e = Event.current;

				Selection.activeGameObject = go;

				var mouseWorldPosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
				mouseWorldPosition -= doors.transform.position;
				mouseWorldPosition.z = 0;
				mouseWorldPosition.x = (float)Math.Floor(mouseWorldPosition.x);
				mouseWorldPosition.y = (float)Math.Floor(mouseWorldPosition.y);

				var controlId = GUIUtility.GetControlID(FocusType.Passive);
				HandleUtility.AddDefaultControl(controlId);

				switch (e.type)
				{
					case EventType.MouseDown:
                        if (e.button == 0)
                        {
                            firstPoint = mouseWorldPosition;
                            hasFirstPoint = true;
                            hasSecondPoint = false;

                            Event.current.Use();
                        }

						break;

					case EventType.MouseUp:
                        if (e.button == 0)
                        {
                            if (hasFirstPoint)
                            {
                                hasSecondPoint = true;
                            }

                            Event.current.Use();
                        }

						break;
				}

				if (hasFirstPoint)
				{
					highlightInfo = null;

					var from = firstPoint;
					var to = mouseWorldPosition;

					if (from.x != to.x && from.y != to.y)
					{
						to.x = from.x;
					}

					DrawOutline(from, to, Color.yellow, false);

					if (hasSecondPoint)
					{
						hasFirstPoint = false;
						hasSecondPoint = false;

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
		}

		private void DrawOutline(Vector3 from, Vector3 to, Color outlineColor, bool drawDiagonal = true)
        {
            var doors = target as Generators.Common.RoomTemplates.Doors.Doors;

			from = from + doors.transform.position;
			to = to + doors.transform.position;

			if (from.x == to.x || from.y == to.y)
			{
				if (to.x < from.x)
				{
					from.x += 1;
				}

				if (to.y < from.y)
				{
					from.y += 1;
				}

				if (to.x >= from.x)
				{
					to.x += 1;
				}

				if (to.y >= from.y)
				{
					to.y += 1;
				}
			}
			else
			{
				to = from + new Vector3(1, 1);
			}

            var offset = 0.05f; 
            if (from.x <= to.x - 1)
            {
                from += new Vector3(offset, offset);
                to += new Vector3(-offset, -offset);
            }
            else
            {
				from += new Vector3(offset, -offset);
				to += new Vector3(-offset, offset);
			}

			Handles.DrawSolidRectangleWithOutline(new Rect(from, to - from), Color.clear, outlineColor);

			if (drawDiagonal)
			{
                DrawDiagonal(from, to, outlineColor);
			}
		}

		protected void DrawDiagonal(Vector3 from, Vector3 to, Color color)
		{
			var smallestX = Math.Min(from.x, to.x);
			var smallestY = Math.Min(from.y, to.y);

			var largestX = Math.Max(from.x, to.x);
			var largestY = Math.Max(from.y, to.y);

			var oldColor = Handles.color;
			Handles.color = color;
			Handles.DrawLine(new Vector3(smallestX, smallestY), new Vector3(largestX, largestY));
			Handles.color = oldColor;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var doors = target as Doors;
			
			var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid(doors.SelectedMode, new[] { "Simple mode", "Specific positions"}, 2);

            EditorGUILayout.Space();

			if (selectedModeProp.intValue == 0)
			{
				EditorGUILayout.IntSlider(doorsLength, 1, 10, "Door length");
				EditorGUILayout.IntSlider(distanceFromCorners, 0, 10, "Corner distance");
			}

			if (selectedModeProp.intValue == 1)
			{
                var addDoorPositionsNew = GUILayout.Toggle(addSpecificDoorPositions, "Add door positions", GUI.skin.button);
				var deleteDoorPositionsNew = GUILayout.Toggle(deleteDoorPositions, "Delete door positions", GUI.skin.button);

                if (addDoorPositionsNew && !addSpecificDoorPositions)
                {
                    addSpecificDoorPositions = true;
                    deleteDoorPositions = false;
                } else if (deleteDoorPositionsNew && !deleteDoorPositions)
                {
                    deleteDoorPositions = true;
                    addSpecificDoorPositions = false;
                }

                if (addDoorPositionsNew == false)
                {
                    addSpecificDoorPositions = false;
                }

                if (deleteDoorPositionsNew == false)
                {
                    deleteDoorPositions = false;
                }

                if (GUILayout.Button("Delete all door positions"))
				{
					doorsList.ClearArray();
				}

                try
                {
                    var polygon = RoomTemplatesLoader.GetPolygonFromRoomTemplate(doors.gameObject);
                    var doorPositions = DoorHandler.DefaultHandler.GetDoorPositions(polygon, doors.GetDoorMode());

                    if (doorPositions.Count != doors.DoorsList.Count)
                    {
                        EditorGUILayout.HelpBox("There seems to be a door of length 1 that is at the corner of the outline, which is currently not supported. Either use outline override to change the outline or remove the door position.", MessageType.Error);
                    }
                }
                catch (Exception)
                {

                }
            }

			serializedObject.ApplyModifiedProperties();
		}
	}
}