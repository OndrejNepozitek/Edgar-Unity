namespace Assets.Editor
{
	using System.Collections.Generic;
	using System.IO;
	using Newtonsoft.Json;
	using Scripts;
	using UnityEditor;
	using UnityEngine;

	public class MainWindow : EditorWindow
	{
		public Dictionary<int, RoomShape> RoomShapes { get; set; } = new Dictionary<int, RoomShape>();

		private bool showRoomShapes = false;

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

				foreach (var pair in RoomShapes)
				{
					var id = pair.Key;
					var roomShape = pair.Value;

					GUILayout.BeginHorizontal();

					GUILayout.Label(roomShape.Name);
					if (GUILayout.Button("Edit"))
					{
						var roomShapeWindow = GetWindow<RoomShapeWindow>();
						roomShapeWindow.SetRoomShape(id, roomShape);
						roomShapeWindow.Show();
					}

					GUILayout.EndHorizontal();
				}
			}

			GUILayout.EndVertical();  
		}

		public void OnDestroy()
		{
			Debug.Log("Destroyed");
		}

		public void OnEnable()
		{
			Debug.Log("OnEnable");
			var path = "Assets/Resources/roomShapes.json";

			using (var reader = new StreamReader(path))
			{
				var json = reader.ReadToEnd();
				var deserialized = JsonConvert.DeserializeObject<Dictionary<int, RoomShape>>(json);

				RoomShapes = deserialized ?? new Dictionary<int, RoomShape>();
			}
		}

		void OnDisable()
		{
			Debug.Log("OnDisable");

			var json = JsonConvert.SerializeObject(RoomShapes);
			var path = "Assets/Resources/roomShapes.json";

			using (var writer = new StreamWriter(path))
			{
				writer.Write(json); 
			}
		}
	}
}
