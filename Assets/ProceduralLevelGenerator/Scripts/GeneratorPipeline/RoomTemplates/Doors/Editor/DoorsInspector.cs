namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Doors.Editor
{
	using System;
	using RoomTemplates.Doors;
	using Scripts;
	using UnityEditor;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	[CustomEditor(typeof(Doors))]
	public class DoorsInspector : Editor
	{
		private DoorInfo highlightInfo;

		private SerializedProperty doorsLength;

		private SerializedProperty distanceFromCorners;

		private SerializedProperty doorsList;

		private bool addSpecificDoorPositions;

		private Vector3 firstPoint;

		private bool hasFirstPoint;

		private bool hasSecondPoint;

		private RoomShapesLoader roomShapesLoader = new RoomShapesLoader();

		public void OnEnable()
		{
			doorsLength = serializedObject.FindProperty(nameof(Doors.DoorLength));
			distanceFromCorners = serializedObject.FindProperty(nameof(Doors.DistanceFromCorners));
			doorsList = serializedObject.FindProperty(nameof(Doors.DoorsList));
			addSpecificDoorPositions = false;
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
			var polygon = roomShapesLoader.GetPolygonFromTilemap(go.GetComponentsInChildren<Tilemap>());

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

						firstPoint = mouseWorldPosition;
						hasFirstPoint = true;
						hasSecondPoint = false;

						Event.current.Use();

						break;

					case EventType.MouseUp:
						if (hasFirstPoint)
						{
							hasSecondPoint = true;
						}

						Event.current.Use();
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

					DrawOutline(from, to, Color.yellow);

					if (hasSecondPoint)
					{
						hasFirstPoint = false;
						hasSecondPoint = false;

						var newDoorInfo = new DoorInfo()
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
			serializedObject.Update();

			var doors = target as Doors;

			var selectedModeProp = serializedObject.FindProperty(nameof(Doors.SelectedMode));
			selectedModeProp.intValue = GUILayout.SelectionGrid(doors.SelectedMode, new[] {"Specific positions", "Overlap mode"}, 2);
			var shouldRedraw = false;

			EditorGUILayout.Space();

			if (selectedModeProp.intValue == 1)
			{
				EditorGUILayout.IntSlider(doorsLength, 1, 10, "Door length");
				EditorGUILayout.IntSlider(distanceFromCorners, 0, 10, "Corner distance");
			}

			if (selectedModeProp.intValue == 0)
			{
				addSpecificDoorPositions = GUILayout.Toggle(addSpecificDoorPositions, "Add door positions", GUI.skin.button);

				if (GUILayout.Button("Delete all door positions"))
				{
					doorsList.ClearArray();
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}