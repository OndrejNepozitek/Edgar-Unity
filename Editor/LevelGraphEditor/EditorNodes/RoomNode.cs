using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;

namespace Assets.ProceduralLevelGenerator.Editor.LevelGraphEditor.EditorNodes
{
	using System;
	using NodeBasedEditor;
    using UnityEditor;
	using UnityEngine;

	public class RoomNode : IEditorNode<Room>
	{
		public Room Data { get; set; }

		public Action OnDelete;

		public Action<Event> OnStartConnection;

		public Action<Event> OnEndConnection;

		public Rect Rect;

		public bool IsConnectionMade;

		public EditorMode Mode;

		private readonly GUIStyle style;

        private readonly GUIStyle activeStyle;

        private bool isDragged;

		private bool isClickAfterContextMenu;


		public RoomNode(Room data, float width, float height, GUIStyle style, GUIStyle activeStyle, EditorMode mode)
		{
			Data = data;
			this.style = style;
            this.activeStyle = activeStyle;
            Rect = new Rect(Data.Position.x, Data.Position.y, width, height);
			this.Mode = mode;
		}

		// TODO: refactor
		public bool ProcessEvents(Event e)
		{
            switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 1)
					{
						if (Rect.Contains(e.mousePosition))
						{
							ProcessContextMenu();
							e.Use();
							isClickAfterContextMenu = true;
						}
					}
					else if (e.button == 0 && Mode == EditorMode.MakeConnections && Rect.Contains(e.mousePosition))
					{
						OnStartConnection?.Invoke(e);
					} else if (e.button == 0 && Mode == EditorMode.Drag && Rect.Contains(e.mousePosition) && e.clickCount > 1)
                    {
                        OnClickConfigure();
                        e.Use();
                        GUI.changed = true;
                    }
					else if (Mode == EditorMode.Drag && Rect.Contains(e.mousePosition) && e.button == 0)
					{
						isDragged = true;
					}

                    if (Rect.Contains(e.mousePosition))
                    {
                        e.Use();
                    }

					break;

				case EventType.MouseUp:
					if (Rect.Contains(e.mousePosition) && e.button == 0 && Mode == EditorMode.MakeConnections)
					{
						OnEndConnection?.Invoke(e);
					}

					if (e.button == 0)
					{
						isDragged = false;
					}

					break;
				case EventType.MouseDrag:
					if (e.button == 0)
					{
						if (isClickAfterContextMenu)
						{
							e.Use();
							isClickAfterContextMenu = false;
						}

						switch (Mode)
						{
							case EditorMode.Drag:
								if (isDragged)
								{
									Drag(e.delta);
									e.Use();
								}

								break;
						}
					}
					break;

			}


			return false;
		}

		private void ProcessContextMenu()
		{
			var genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Configure room"), false, OnClickConfigure);
			genericMenu.AddSeparator("");
			genericMenu.AddItem(new GUIContent("Delete room"), false, OnClickDelete);
			genericMenu.ShowAsContext();
		}

		private void OnClickDelete()
		{
			OnDelete?.Invoke();
		}

		private void OnClickConfigure()
		{
			Selection.activeObject = Data;
		}

		public void Draw()
		{
			GUI.Box(Rect, Data.ToString(), Selection.activeObject == Data ? activeStyle : style);
		}

		public void Drag(Vector2 delta)
		{
			Rect.position += delta;
			Data.Position += delta;
		}
	}
}