namespace Assets.Editor.LayoutGraphEditor
{
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(LayoutGraph))]
	public class LayoutGraphInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Setup layout graph"))
			{
				var window = EditorWindow.GetWindow<LayoutGraphWindow>();
				window.Data = (LayoutGraph) target;
				window.Initialize();
				window.Show();
			}
		}
	}
}