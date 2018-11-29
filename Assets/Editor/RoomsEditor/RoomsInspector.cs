namespace Assets.Editor.RoomsEditor
{
	using Scripts.Data2;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(Rooms))]
	public class RoomsInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Setup rooms"))
			{
				var window = EditorWindow.GetWindow<RoomsWindow>();
				window.Data = (Rooms) target;
				window.Initialize();
				window.Show();
			}
		}
	}
}