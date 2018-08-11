namespace Assets.Editor
{
	using UnityEditor;
	using UnityEngine;

	public class MainWindow : EditorWindow
	{
		[MenuItem("Window/Procedural layout generator")]
		public static void Init()
		{
			GetWindow<MainWindow>(false, "Layout generator");
		}

		public void OnGUI()
		{
			GUILayout.BeginVertical();

			if (GUILayout.Button("Add room shape"))
			{
				GetWindow<RoomShapeWindow>().Show();
			}

			GUILayout.EndVertical();
		}
	}
}
