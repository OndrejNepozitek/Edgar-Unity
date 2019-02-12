namespace Assets.ProceduralLevelGenerator.Editor.LevelGraphEditor.EditorNodes
{
	using System;
	using NodeBasedEditor;
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;

	public class ConnectionNode : IEditorNode<Connection>
	{
		public Connection Data { get; set; }

		public RoomNode From { get; set; }

		public RoomNode To { get; set; }

		public Action OnDelete;

		private GUIStyle handleStyle;

		private int handleWidth;

		private bool isClickAfterContextMenu;

		public ConnectionNode(Connection data, RoomNode from, RoomNode to, GUIStyle handleStyle, int handleWidth)
		{
			Data = data;
			From = from;
			To = to;
			this.handleStyle = handleStyle;
			this.handleWidth = handleWidth;
		}

		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 1)
					{
						if (GetHandleRect().Contains(e.mousePosition))
						{
							ProcessContextMenu();
							e.Use();
							isClickAfterContextMenu = true;
						}
					}

					break;

				case EventType.MouseDrag:
					if (isClickAfterContextMenu)
					{
						isClickAfterContextMenu = false;
						e.Use();
					}

					break;
			}


			return false;
		}

		private void ProcessContextMenu()
		{
			var genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Delete connection"), false, OnClickDelete);
			genericMenu.ShowAsContext();
		}

		private void OnClickDelete()
		{
			OnDelete?.Invoke();
		}

		public void Draw()
		{
			Handles.DrawLine(From.Rect.center, To.Rect.center);
			GUI.Box(GetHandleRect(), string.Empty, handleStyle);
		}

		public void Drag(Vector2 delta)
		{
			
		}

		private Rect GetHandleRect()
		{
			var handleCenter = Vector2.Lerp(From.Rect.center, To.Rect.center, 0.5f);
			var rect = new Rect(handleCenter.x - handleWidth / 2.0f, handleCenter.y - handleWidth / 2.0f, handleWidth, handleWidth);

			return rect;
		}
	}
}