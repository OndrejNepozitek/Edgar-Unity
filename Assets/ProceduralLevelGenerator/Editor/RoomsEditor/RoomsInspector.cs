namespace Assets.ProceduralLevelGenerator.Editor.RoomsEditor
{
	using Scripts.Data.Rooms;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(RoomTemplatesSet))]
	public class RoomsInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Setup rooms"))
			{
				var window = EditorWindow.GetWindow<RoomsWindow>("Room templates");
				window.Data = (RoomTemplatesSet) target;
				window.Initialize();
				window.Show();
			}
		}
	}
}