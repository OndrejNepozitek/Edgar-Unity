namespace Assets.Editor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneralAlgorithms.DataStructures.Polygons;
	using Newtonsoft.Json;
	using Scripts;
	using UnityEditor;
	using UnityEngine;

	public class RoomShapeWindow : EditorWindow
	{
		private const int LineOffset = 20;
		private Vector2 topLeftOffset = new Vector2(300, 0);

		private int selectedMode = 0;
		private int selectedDoorMode = 1;
		private string name;

		private RoomShape roomShape;
		private int id;

		private HashSet<IntVector2> usedTiles = new HashSet<IntVector2>();

		public RoomShapeWindow()
		{
			minSize = new Vector2(500, 500);
		}

		public void OnGUI()
		{
			var e = Event.current;

			if ((e.type == EventType.MouseUp || e.type == EventType.MouseDrag) && e.button == 0)
			{
				HandleGridClick(e.mousePosition);
			}

			DrawGrid();
			DrawTiles();

			if (e.type == EventType.MouseUp || e.type == EventType.MouseDrag)
			{
				Repaint();
			}

			GUILayout.BeginHorizontal();

			var style = new GUIStyle(GUI.skin.label)
			{
				padding = new RectOffset(20, 20, 20, 20)
			};

			GUILayout.BeginVertical(style, GUILayout.Width(topLeftOffset.x));

			EditorGUIUtility.labelWidth = 75;
			name = EditorGUILayout.TextField("Name:", name);
			EditorGUIUtility.labelWidth = 0;

			GUILayout.Space(15);

			GUILayout.Label("Doors");

			selectedDoorMode = GUILayout.SelectionGrid(selectedDoorMode, new string[] { "Simple", "Explicit" }, 2);

			GUILayout.Label("Mode");

			selectedMode = GUILayout.SelectionGrid(selectedMode, new string[] {"Insert", "Delete"}, 2);

			GUILayout.Space(30);

			if (GUILayout.Button("Validate"))
			{
				try
				{
					var polygon = RoomShapesLogic.GetPolygonFromGridPoints(usedTiles);

					EditorUtility.DisplayDialog("The polygon is valid", "The polygon is valid", "Ok");
				}
				catch (Exception exception)
				{
					EditorUtility.DisplayDialog("The polygon is invalid", exception.Message, "Ok");
				}
			}

			var data = MainWindow.Data;

			if (GUILayout.Button("Save and close"))
			{
				if (id == 0)
				{
					id = data.RoomShapes.GetNextId();
				}

				data.RoomShapes[id] = new RoomShape()
				{
					Name = name,
					GridPoints = usedTiles
				};

				Close();
			}

			if (id != 0 && GUILayout.Button("Delete"))
			{
				var mainWindow = GetWindow<MainWindow>();
				data.RoomShapes.Remove(id);

				Close();
			}

			GUILayout.EndVertical();
			

			GUILayout.EndHorizontal();
		}

		private void HandleGridClick(Vector2 mousePosition)
		{
			if (mousePosition.x < topLeftOffset.x)
				return;

			var center = GetGridCenter();
			var x = (int) (mousePosition.x - center.x);
			var y = (int) (mousePosition.y - center.y);

			x = x < 0 ? x / LineOffset - 1 : x / LineOffset;
			y = y < 0 ? y / LineOffset - 1 : y / LineOffset;

			var index = new IntVector2(x, y);

			if (selectedMode == 0)
			{
				if (!usedTiles.Contains(index))
				{
					usedTiles.Add(index);
				}
			}
			else
			{
				if (usedTiles.Contains(index))
				{
					usedTiles.Remove(index);
				}
			}
		}

		private void DrawGrid()
		{
			var centerX = (position.width - topLeftOffset.x) / 2 + topLeftOffset.x;
			var centerY = position.height / 2;

			var linesCountHorizontal = (int) (position.height / LineOffset) + 1;
			var linesCountVertical = (int) ((position.width - 200) / LineOffset) + 1;

			for (var i = 0; i < linesCountHorizontal; i++)
			{
				var centerOffset = i - linesCountHorizontal / 2;
				var computedOffset = centerY + centerOffset * LineOffset;

				Handles.color = centerOffset % 10 == 0 ? Color.black : Color.gray;

				Handles.DrawLine(new Vector3(topLeftOffset.x, computedOffset), new Vector3(position.width, computedOffset));
			}

			for (var i = 0; i < linesCountVertical; i++)
			{
				var centerOffset = i - linesCountVertical / 2;
				var computedOffset = centerX + centerOffset * LineOffset;

				if (computedOffset < topLeftOffset.x)
					continue;

				Handles.color = centerOffset % 10 == 0 ? Color.black : Color.gray;

				Handles.DrawLine(new Vector3(computedOffset, 0), new Vector3(computedOffset, position.height));
			}
		}

		private Vector2 GetGridCenter()
		{
			return new Vector2(
				(position.width - topLeftOffset.x) / 2 + topLeftOffset.x,
				position.height / 2
			);
		}

		private void DrawTiles()
		{
			Handles.color = Color.black;
			var center = GetGridCenter();

			foreach (var tile in usedTiles)
			{
				var x = center.x + tile.X * LineOffset;
				var y = center.y + tile.Y * LineOffset;

				Handles.DrawSolidRectangleWithOutline(new Rect(x, y, LineOffset, LineOffset), Color.blue, Color.blue);
			}
		}

		public void SetRoomShape(int id, RoomShape roomShape)
		{
			this.roomShape = roomShape;
			this.id = id;

			if (id != 0)
			{
				name = roomShape.Name;
				usedTiles = new HashSet<IntVector2>(roomShape.GridPoints);
			}
		}
	}
}
