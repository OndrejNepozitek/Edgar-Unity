namespace Assets.Editor.RoomsEditor
{
	using Scripts.Data;
	using Scripts.Data.Rooms;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(RoomTemplatesWrapper))]
	public class RoomsInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Setup rooms"))
			{
				var window = EditorWindow.GetWindow<RoomsWindow>();
				window.Data = (RoomTemplatesWrapper) target;
				window.Initialize();
				window.Show();
			}
		}
	}
}