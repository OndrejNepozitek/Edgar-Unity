namespace Assets.ProceduralLevelGenerator.Editor
{
	using System.Collections.Generic;
	using NodeBasedEditor;
	using UnityEditor;
	using UnityEngine;

	public class NodeBasedEditorWindow : EditorWindow
	{
		private List<Node> nodes;

		public void OnGUI()
		{
			DrawNodes();
			ProcessEvents(Event.current);
		}

		private void DrawNodes()
		{
			if (nodes != null)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					nodes[i].Draw();
				}
			}
		}

		private void ProcessEvents(Event e)
		{
		}
	}
}