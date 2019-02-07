namespace Assets.ProceduralLevelGenerator.Editor
{
	using System.Collections.Generic;
	using NodeBasedEditor;
	using UnityEditor;
	using UnityEngine;

	public class MapDescriptionWindow : EditorWindow
	{
		public Rect windowRect = new Rect(20, 20, 120, 50);

		private List<Node> nodes;
		private List<ConnectionLegacy> connections;

		private GUIStyle nodeStyle;
		private GUIStyle selectedNodeStyle;
		private readonly Dictionary<ConnectionPointType, GUIStyle> connectionStyles = new Dictionary<ConnectionPointType, GUIStyle>();

		private Node selectedToNode;
		private Node selectedFromNode;

		private Vector2 offset;
		private Vector2 drag;

		public MapDescriptionWindow()
		{
			minSize = new Vector2(500, 500);
		}

		private void OnEnable()
		{
			nodeStyle = new GUIStyle();
			nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
			nodeStyle.border = new RectOffset(12, 12, 12, 12);

			selectedNodeStyle = new GUIStyle();
			selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
			selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

			connectionStyles.Add(ConnectionPointType.Left, new GUIStyle
			{
				normal = {background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D},
				active = {background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D},
				border = new RectOffset(4, 4, 12, 12)
			});

			connectionStyles.Add(ConnectionPointType.Right, new GUIStyle
			{
				normal = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D },
				active = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D },
				border = new RectOffset(4, 4, 12, 12)
			});

			connectionStyles.Add(ConnectionPointType.Top, new GUIStyle
			{
				normal = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D},
				active = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D},
				border = new RectOffset(4, 4, 12, 12)
			});

			connectionStyles.Add(ConnectionPointType.Bottom, new GUIStyle
			{
				normal = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D },
				active = { background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D },
				border = new RectOffset(4, 4, 12, 12)
			});
		}

		private void OnGUI()
		{
			DrawGrid(20, 0.2f, Color.gray);
			DrawGrid(100, 0.4f, Color.gray);

			DrawConnections();
			DrawNodes();

			// DrawConnectionLine(Event.current);


			ProcessNodeEvents(Event.current);
			ProcessEvents(Event.current);

			if (GUI.changed) Repaint();
		}

		private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
		{
			int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
			int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

			Handles.BeginGUI();
			Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

			offset += drag * 0.5f;
			Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

			for (int i = 0; i < widthDivs; i++)
			{
				Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
			}

			for (int j = 0; j < heightDivs; j++)
			{
				Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
			}

			Handles.color = Color.white;
			Handles.EndGUI();
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

		private void DrawConnections()
		{
			if (connections != null)
			{
				for (int i = 0; i < connections.Count; i++)
				{
					connections[i].Draw();
				}
			}
		}

		private void ProcessEvents(Event e)
		{
			drag = Vector2.zero;

			switch (e.type)
			{
				case EventType.MouseDown:
					if (e.button == 0)
					{
						// ClearConnectionSelection();
					}

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

		private void ProcessNodeEvents(Event e)
		{
			if (nodes != null)
			{
				for (int i = nodes.Count - 1; i >= 0; i--)
				{
					bool guiChanged = nodes[i].ProcessEvents(e);

					if (guiChanged)
					{
						GUI.changed = true;
					}
				}
			}
		}

		private void DrawConnectionLine(Event e)
		{
			if (selectedToNode != null && selectedFromNode == null)
			{
				Handles.DrawLine(
					selectedToNode.rect.center,
					e.mousePosition
				);

				GUI.changed = true;
			}

			if (selectedFromNode != null && selectedToNode == null)
			{
				Handles.DrawBezier(
					selectedFromNode.rect.center,
					e.mousePosition,
					selectedFromNode.rect.center - Vector2.left * 50f,
					e.mousePosition + Vector2.left * 50f,
					Color.white,
					null,
					2f
				);

				GUI.changed = true;
			}
		}

		private void ProcessContextMenu(Vector2 mousePosition)
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
			genericMenu.ShowAsContext();
		}

		private void OnDrag(Vector2 delta)
		{
			drag = delta;

			if (nodes != null)
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					nodes[i].Drag(delta);
				}
			}

			GUI.changed = true;
		}

		private void OnClickAddNode(Vector2 mousePosition)
		{
			if (nodes == null)
			{
				nodes = new List<Node>();
			}

			nodes.Add(new Node(mousePosition, 50, 50, nodeStyle, selectedNodeStyle, connectionStyles, OnClickConnectionPoint, OnClickRemoveNode, OnClickNode));
		}

		private void OnClickNode(Node node)
		{
			if (node.isSelected)
			{
				if (selectedFromNode == null)
				{
					Debug.Log("select from");
					selectedFromNode = node;
				}
				else
				{
					selectedToNode = node;
					CreateConnection();
					ClearConnectionSelection();
				}
			}
			else if (selectedFromNode == node)
			{
				Debug.Log("reset from");
				selectedFromNode = null;
			}
		}

		private void OnClickConnectionPoint(ConnectionPoint point)
		{
			//if (selectedToNode == null)
			//{
			//	selectedToNode = point;
			//}
			//else
			//{
			//	selectedFromNode = point;

			//	if (selectedFromNode.node != selectedToNode.node)
			//	{
			//		CreateConnection();
			//		ClearConnectionSelection();
			//	}
			//}
		}

		private void OnClickRemoveNode(Node node)
		{
			//if (connections != null)
			//{
			//	List<Connection> connectionsToRemove = new List<Connection>();

			//	for (int i = 0; i < connections.Count; i++)
			//	{
			//		if (connections[i].inPoint == node.LeftConnectionPoint || connections[i].outPoint == node.RightConnectionPoint)
			//		{
			//			connectionsToRemove.Add(connections[i]);
			//		}
			//	}

			//	for (int i = 0; i < connectionsToRemove.Count; i++)
			//	{
			//		connections.Remove(connectionsToRemove[i]);
			//	}

			//	connectionsToRemove = null;
			//}

			//nodes.Remove(node);
		}

		private void OnClickRemoveConnection(ConnectionLegacy connection)
		{
			connections.Remove(connection);
		}

		private void CreateConnection()
		{
			if (connections == null)
			{
				connections = new List<ConnectionLegacy>();
			}

			connections.Add(new ConnectionLegacy(selectedToNode, selectedFromNode, OnClickRemoveConnection));
		}

		private void ClearConnectionSelection()
		{
			selectedFromNode?.SetSelected(false);
			selectedToNode?.SetSelected(false);
			
			selectedToNode = null;
			selectedFromNode = null;
		}
	}
}