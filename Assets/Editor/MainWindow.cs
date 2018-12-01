namespace Assets.Editor
{
	using System.Collections.Generic;
	using System.IO;
	using Newtonsoft.Json;
	using Scripts;
	using Scripts.Data;
	using UnityEditor;
	using UnityEngine;

	public class MainWindow : EditorWindow
	{
		public static DataWrapper Data;

		private bool showRoomShapes;
		private bool showRoomShapeSets;

		[MenuItem("Window/Procedural layout generator")]
		public static void Init()
		{
			GetWindow<MainWindow>(false, "Layout generator");
		}

		public void OnGUI()
		{
			GUILayout.BeginVertical();

			var myFoldoutStyle = new GUIStyle(EditorStyles.foldout) {fontStyle = FontStyle.Bold};

			showRoomShapes = EditorGUILayout.Foldout(showRoomShapes, "Room shapes", myFoldoutStyle);
			if (showRoomShapes)
			{
				if (GUILayout.Button("Add room shape"))  
				{
					var roomShapeWindow = GetWindow<RoomShapeWindow>();
					roomShapeWindow.SetRoomShape(0, null);
					roomShapeWindow.Show();
				}

				foreach (var pair in Data.RoomShapes)
				{
					var id = pair.Key;
					var roomShape = pair.Value;

					GUILayout.BeginHorizontal();

					GUILayout.Label(roomShape.Name);
					if (GUILayout.Button("Edit", GUILayout.Width(100)))
					{
						var roomShapeWindow = GetWindow<RoomShapeWindow>();
						roomShapeWindow.SetRoomShape(id, roomShape);
						roomShapeWindow.Show();
					}

					GUILayout.EndHorizontal();
				}

				GUILayout.Space(30);
			}

			showRoomShapeSets = EditorGUILayout.Foldout(showRoomShapeSets, "Room shape sets", myFoldoutStyle);
			if (showRoomShapeSets)
			{
				if (GUILayout.Button("Add room shapes set"))
				{
					var window = GetWindow<RoomShapesSetWindow>();
					window.Show();
				}

				foreach (var pair in Data.RoomShapeSets)
				{
					var id = pair.Key;
					var roomShapeSet = pair.Value;

					GUILayout.BeginHorizontal();

					GUILayout.Label(roomShapeSet.Name);
					if (GUILayout.Button("Edit", GUILayout.Width(100)))
					{
						var window = GetWindow<RoomShapesSetWindow>();
						window.SetRoomShapeSet(id, roomShapeSet);
						window.Show();
					}

					GUILayout.EndHorizontal();
				}

				GUILayout.Space(30);
			}

			if (GUILayout.Button("Add map description"))
			{
				var window = GetWindow<MapDescriptionWindow>();
				window.Show();
			}

			GUILayout.EndVertical();  
		}

		public void OnDestroy()
		{
			Debug.Log("Destroyed");
		}

		public void OnEnable()
		{
			// Debug.Log("OnEnable");
			var path = "Assets/Resources/roomShapes.json";

			using (var reader = new StreamReader(path))
			{
				var json = reader.ReadToEnd();
				var deserialized = JsonConvert.DeserializeObject<DataWrapper>(json);

				Data = deserialized ?? new DataWrapper();
			}
		}

		public void OnDisable()
		{
			//Debug.Log("OnDisable");

			var json = JsonConvert.SerializeObject(Data);
			var path = "Assets/Resources/roomShapes.json";

			using (var writer = new StreamWriter(path))
			{
				writer.Write(json);
			}
		}
	}
}
