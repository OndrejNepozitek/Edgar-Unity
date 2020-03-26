namespace Assets.ProceduralLevelGenerator.Editor.NodeBasedEditor
{
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	public abstract class NodeBasedEditorBase : EditorWindow
	{
		protected Vector2 Offset;
		protected Vector2 Drag;

		protected abstract IEnumerable<IEditorNodeBase> GetAllNodes();

		public virtual void OnGUI()
		{
			DrawGrid(20, 0.2f, Color.gray);
			DrawGrid(100, 0.4f, Color.gray);

			foreach (var node in GetAllNodes())
			{
				node.Draw();
				node.ProcessEvents(Event.current);
			}

			ProcessEvents(Event.current);

			if (GUI.changed) Repaint();
		}

		public virtual void OnEnable()
		{

		}

		private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
		{
			var widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
			var heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			Offset += Drag * 0.5f;
			var newOffset = new Vector3(Offset.x % gridSpacing, Offset.y % gridSpacing, 0);

			for (var i = 0; i < widthDivs; i++)
			{
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
			}

			for (var j = 0; j < heightDivs; j++)
			{
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
			}

			Handles.color = Color.white;
			Handles.EndGUI();
		}

		protected virtual void ProcessEvents(Event e)
		{
			Drag = Vector2.zero;

			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 1)
					{
						ProcessContextMenu(e.mousePosition);
					}
					break;

				case EventType.MouseDrag:
					if (e.button == 0)
					{
						OnDrag(e.delta);
					}
					break;
			}
		}

		protected virtual void ProcessContextMenu(Vector2 mousePosition)
		{

		}

		protected void OnDrag(Vector2 delta)
		{
			Drag = delta;

			foreach (var node in GetAllNodes())
			{
				node.Drag(delta);
			}

			GUI.changed = true;
		}

		protected Texture2D MakeTex(int width, int height, Color col)
		{
			Color[] pix = new Color[width * height];
			for (int i = 0; i < pix.Length; ++i)
			{
				pix[i] = col;
			}
			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();
			return result;
		}
	}
}