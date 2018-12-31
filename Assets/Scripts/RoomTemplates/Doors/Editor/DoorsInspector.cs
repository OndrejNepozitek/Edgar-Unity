namespace Assets.Scripts.RoomTemplates.Doors
{
	using System;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	[CustomEditor(typeof(Doors))]
	public class DoorsInspector : Editor
	{
		private DoorInfo highlightInfo;

		public void OnSceneGUI()
		{
			var doors = target as Doors;

			switch (doors.SelectedMode)
			{
				case 0:
					DrawSpecifPositions();
					break;

				case 1:
					DrawOverlap();
					break;
			}
		}

		private void DrawOverlap()
		{
			var doors = target as Doors;
			var go = doors.transform.gameObject;
			var tilemap = go.GetComponentInChildren<Tilemap>();
			var polygon = RoomShapesLogic.GetPolygonFromTilemap(tilemap);

			foreach (var line in polygon.GetLines())
			{
				if (line.Length - 2 * doors.DistanceFromCorners < doors.DoorLength - 1)
					continue;

				var doorLine = line.Shrink(doors.DistanceFromCorners);

				DrawOutline(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3(), Color.red);
			}
		}

		private void DrawSpecifPositions()
		{
			var doors = target as Doors;
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

					doors.FirstPoint = mouseWorldPosition;
					doors.HasFirstPoint = true;
					doors.HasSecondPoint = false;

					Event.current.Use();

					break;

				case EventType.MouseUp:
					if (doors.HasFirstPoint)
					{
						doors.SecondPoint = mouseWorldPosition;
						doors.HasSecondPoint = true;
					}


					Event.current.Use();
					break;
			}

			if (doors.HasFirstPoint)
			{
				highlightInfo = null;

				var from = doors.FirstPoint;
				var to = mouseWorldPosition;

				if (from.x != to.x && from.y != to.y)
				{
					to.x = from.x;
				}

				DrawOutline(from, to, Color.yellow);

				if (doors.HasSecondPoint)
				{
					doors.DoorsList.Add(new DoorInfo()
					{
						From = from,
						To = to,
					});

					doors.HasFirstPoint = false;
					doors.HasSecondPoint = false;
				}
			}

			foreach (var door in doors.DoorsList)
			{
				DrawOutline(door.From, door.To, Color.red);
			}

			if (highlightInfo != null)
			{
				DrawOutline(highlightInfo.From, highlightInfo.To, Color.yellow);
			}
		}

		private void DrawOutline(Vector3 from, Vector3 to, Color outlineColor)
		{
			var doors = target as Doors;

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

				Handles.DrawSolidRectangleWithOutline(new Rect(from, to - from), Color.clear, outlineColor);
			}
			else
			{
				to = from + new Vector3(1, 1);

				Handles.DrawSolidRectangleWithOutline(new Rect(from, to - from), Color.clear, outlineColor);
			}
		}

		public override void OnInspectorGUI()
		{
			var doors = target as Doors;

			doors.SelectedMode = GUILayout.SelectionGrid(doors.SelectedMode, new[] {"Specific positions", "Overlap mode"}, 2);
			var shouldRedraw = false;

			if (doors.SelectedMode == 1)
			{
				doors.DoorLength = EditorGUILayout.IntSlider(new GUIContent("Door length"), doors.DoorLength, 1, 10);
				doors.DistanceFromCorners = EditorGUILayout.IntSlider(new GUIContent("Corner distance"), doors.DistanceFromCorners, 0, 10);
			}

			if (doors.SelectedMode == 0)
			{
				var toRemove = new List<DoorInfo>();

				GUILayout.Label("Doors:", EditorStyles.boldLabel);

				for (int i = 0; i < doors.DoorsList.Count; i++)
				{
					DoorInfo door = doors.DoorsList[i];

					GUILayout.BeginHorizontal();

					if (highlightInfo == door)
					{
						GUILayout.Label($"Door {i}", new GUIStyle(EditorStyles.label)
						{
							normal =
							{
								textColor = Color.yellow
							}
						});
					}
					else
					{
						GUILayout.Label($"Door {i}");
					}

					if (GUILayout.Button("Highlight"))
					{
						if (highlightInfo == door)
						{
							highlightInfo = null;
							shouldRedraw = true;
						}
						else
						{
							highlightInfo = door;
							shouldRedraw = true;
						}
					}

					if (GUILayout.Button("Remove"))
					{
						toRemove.Add(door);
						shouldRedraw = true;
					}

					GUILayout.EndHorizontal();
				}

				foreach (var doorInfo in toRemove)
				{
					doors.DoorsList.Remove(doorInfo);

					if (highlightInfo == doorInfo)
					{
						highlightInfo = null;
					}
				}
			}

			// if (shouldRedraw)
				SceneView.RepaintAll();
		}
	}
}