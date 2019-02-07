using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Editor.NodeBasedEditor
{
	using UnityEditor;

	public class Node
	{
		public Rect rect;
		public string title;

		public bool isDragged;
		public bool isSelected;

		public ConnectionPoint LeftConnectionPoint;
		public ConnectionPoint RightConnectionPoint;
		public ConnectionPoint TopConnectionPoint;
		public ConnectionPoint BottomConnectionPoint;

		public GUIStyle style;
		public GUIStyle defaultNodeStyle;
		public GUIStyle selectedNodeStyle;

		public Action<Node> OnRemoveNode;
		public Action<Node> OnClickNode;

		private Stopwatch stopwatch = new Stopwatch();

		public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, Dictionary<ConnectionPointType, GUIStyle> connectionStyles, Action<ConnectionPoint> onClickConnectionPoint, Action<Node> OnClickRemoveNode, Action<Node> onClickNode)
		{
			rect = new Rect(position.x, position.y, width, height);
			style = nodeStyle;
			LeftConnectionPoint = new ConnectionPoint(this, ConnectionPointType.Left, connectionStyles[ConnectionPointType.Left], onClickConnectionPoint);
			RightConnectionPoint = new ConnectionPoint(this, ConnectionPointType.Right, connectionStyles[ConnectionPointType.Right], onClickConnectionPoint);
			TopConnectionPoint = new ConnectionPoint(this, ConnectionPointType.Top, connectionStyles[ConnectionPointType.Top], onClickConnectionPoint);
			BottomConnectionPoint = new ConnectionPoint(this, ConnectionPointType.Bottom, connectionStyles[ConnectionPointType.Bottom], onClickConnectionPoint);
			defaultNodeStyle = nodeStyle;
			selectedNodeStyle = selectedStyle;
			OnRemoveNode = OnClickRemoveNode;
			OnClickNode = onClickNode;
		}

		public void Drag(Vector2 delta)
		{
			rect.position += delta;
		}

		public void Draw()
		{
			//LeftConnectionPoint.Draw();
			//RightConnectionPoint.Draw();
			//TopConnectionPoint.Draw();
			//BottomConnectionPoint.Draw();
			GUI.Box(rect, title, style);
		}

		public bool ProcessEvents(Event e)
		{
			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						if (rect.Contains(e.mousePosition))
						{
							stopwatch.Restart();
							isDragged = true;
						}
						/*else
					{
						isSelected = false;
						style = isSelected ? selectedNodeStyle : defaultNodeStyle;
						GUI.changed = true;
						OnClickNode(this);
					}*/
					}

					if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
					{
						ProcessContextMenu();
						e.Use();
					}
					break;

				case EventType.MouseUp:
					if (e.button == 0)
					{
						if (stopwatch.ElapsedMilliseconds < 250 && rect.Contains(e.mousePosition))
						{
							SetSelected(!isSelected);
							GUI.changed = true;
							OnClickNode(this);
						}
					}

					isDragged = false;
					break;

				case EventType.MouseDrag:
					if (e.button == 0 && isDragged)
					{
						Drag(e.delta);
						e.Use();
						return true;
					}
					break;
			}

			return false;
		}

		public void SetSelected(bool selected)
		{
			isSelected = selected;
			style = isSelected ? selectedNodeStyle : defaultNodeStyle;
		}

		private void ProcessContextMenu()
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
			genericMenu.ShowAsContext();
		}

		private void OnClickRemoveNode()
		{
			if (OnRemoveNode != null)
			{
				OnRemoveNode(this);
			}
		}
	}
}