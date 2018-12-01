namespace Assets.Scripts.Doors
{
	using System;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(Doors))]
	public class DoorsInspector : Editor
	{
		private DoorInfo highlightInfo;

		public void OnSceneGUI()
		{
			

			var doors = target as Doors;
			var go = doors.transform.gameObject;
			var e = Event.current;
			var camera = Camera.main;

			Selection.activeGameObject = go;
			//var mouseWorldPosition = camera.ScreenToWorldPoint(new Vector3(e.mousePosition.x, e.mousePosition.y));
			//mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y);

			//if (!camera.rect.Contains(e.mousePosition))
			//{
			//	return;
			//}

			// Debug.Log($"Mouse: {e.mousePosition}, Screen: {Screen.width} {Screen.height}");

			//var mousePos = Input.mousePosition;
			//if (mousePos.x < 10 || mousePos.x >= Handles.GetMainGameViewSize().x - 100 || mousePos.y < 10 || mousePos.y >= Handles.GetMainGameViewSize().y - 100)
			//	return;

			var mouseWorldPosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
			mouseWorldPosition -= doors.transform.position;
			mouseWorldPosition.z = 0;
			mouseWorldPosition.x = (float) Math.Floor(mouseWorldPosition.x);
			mouseWorldPosition.y = (float) Math.Floor(mouseWorldPosition.y);
			
			// Debug.Log(mouseWorldPosition);


			// HandleUtility.AddDefaultControl(0);

			var controlId = GUIUtility.GetControlID(FocusType.Passive);
			// GUIUtility.hotControl = controlId;
			HandleUtility.AddDefaultControl(controlId);

			switch (e.type)
			{
				case EventType.MouseDown:

					//// Left click
					//if (e.button == 0)
					//{
					//	if (!doors.hasFirstPoint)
					//	{

					//	}

					//	if (!doors.hasSecondPoint)
					//	{

					//	}
					//}

					doors.firstPoint = mouseWorldPosition;
					doors.hasFirstPoint = true;
					doors.hasSecondPoint = false;

					//// Right click
					//if (e.button == 1)
					//{
					//	if (doors.hasFirstPoint)
					//	{
					//		doors.hasFirstPoint = false;
					//		doors.hasSecondPoint = false;
					//	}
					//}

					Event.current.Use();

					break;

				case EventType.MouseUp:
					if (doors.hasFirstPoint)
					{
						doors.secondPoint = mouseWorldPosition;
						doors.hasSecondPoint = true;
					}


					Event.current.Use();
					break;
			}

			if (doors.hasFirstPoint)
			{
				highlightInfo = null;

				var from = doors.firstPoint;
				var to = mouseWorldPosition;

				if (from.x != to.x && from.y != to.y)
				{
					to.x = from.x;
				}

				DrawOutline(from, to, Color.yellow);

				if (doors.hasSecondPoint)
				{
					if (from.Equals(to))
					{
						throw new NotSupportedException("Doors with lenght one not supported ATM");
					}

					doors.doors.Add(new DoorInfo()
					{
						From = from,
						To = to,
					});

					doors.hasFirstPoint = false;
					doors.hasSecondPoint = false;
				}
			}

			foreach (var door in doors.doors)
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
			DrawDefaultInspector();

			var doors = target as Doors;
			var toRemove = new List<DoorInfo>();
			var shouldRedraw = false;

			GUILayout.Label("Doors:", EditorStyles.boldLabel);

			for (int i = 0; i < doors.doors.Count; i++)
			{
				DoorInfo door = doors.doors[i];

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
				doors.doors.Remove(doorInfo);

				if (highlightInfo == doorInfo)
				{
					highlightInfo = null;
				}
			}

			if (shouldRedraw)
			{
				SceneView.RepaintAll();
			}
		}
	}
}