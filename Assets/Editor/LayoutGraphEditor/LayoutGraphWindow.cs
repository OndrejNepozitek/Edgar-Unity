namespace Assets.Editor.LayoutGraphEditor
{
	using System.Collections.Generic;
	using EditorNodes;
	using NodeBasedEditor;
	using RoomsEditor.EditorNodes;
	using Scripts.Data.Graphs;
	using Scripts.Data.Rooms;
	using UnityEditor;
	using UnityEngine;
	using RoomNode = EditorNodes.RoomNode;

	public class LayoutGraphWindow : NodeBasedEditorBase
	{
		public LayoutGraph Data { get; set; }

		private List<RoomNode> roomNodes = new List<RoomNode>();

		private List<ConnectionNode> connectionNodes = new List<ConnectionNode>();

		private GUIStyle roomNodeStyle;

		private RoomNode connectionFrom;

		private ConnectionProgressNode connectionProgress;

		private EditorMode editorMode = EditorMode.MakeConnections;

		public void Initialize()
		{
			var roomToRoomNodes = new Dictionary<Room, RoomNode>();

			roomNodes = new List<RoomNode>();
			foreach (var data in Data.Rooms)
			{
				var roomNode = CreateNode(data);
				roomToRoomNodes.Add(data, roomNode);
			}

			connectionNodes = new List<ConnectionNode>();
			foreach (var data in Data.Connections)
			{
				CreateConnection(data, roomToRoomNodes[data.From], roomToRoomNodes[data.To]); 
			}

			editorMode = EditorMode.Drag;
			connectionFrom = null;
			connectionProgress = null;
		}

		public override void OnEnable()
		{
			roomNodeStyle = new GUIStyle();
			roomNodeStyle.normal.background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f, 0.85f));
			roomNodeStyle.border = new RectOffset(12, 12, 12, 12);
			roomNodeStyle.normal.textColor = Color.white;
			roomNodeStyle.fontSize = 12;
			roomNodeStyle.alignment = TextAnchor.MiddleCenter;

			//roomNodeStyle = new GUIStyle(nodeStyle);
			//roomNodeStyle.alignment = TextAnchor.UpperCenter;
			//roomNodeStyle.fontSize = 13;
		}

		public override void OnGUI()
		{
			var e = Event.current;
			var modeChanged = false;

			if (e.control && editorMode == EditorMode.Drag)
			{
				editorMode = EditorMode.MakeConnections;
				modeChanged = true;
			}
			else if (!e.control && editorMode == EditorMode.MakeConnections)
			{
				modeChanged = true;
				editorMode = EditorMode.Drag;
			}

			if (modeChanged)
			{
				connectionProgress = null;
				connectionFrom = null;
				roomNodes.ForEach(x => x.Mode = editorMode);
			}

			base.OnGUI();
		}

		protected override void ProcessEvents(Event e)
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
					if (e.button == 0 && editorMode != EditorMode.MakeConnections)
					{
						OnDrag(e.delta);
					}
					break;
			}
		}

		protected override IEnumerable<IEditorNodeBase> GetAllNodes()
		{
			foreach (var node in connectionNodes)
			{
				yield return node;
			}

			if (connectionProgress != null)
			{
				yield return connectionProgress;
			}

			foreach (var node in roomNodes)
			{
				yield return node;
			}
		}

		protected override void ProcessContextMenu(Vector2 mousePosition)
		{
			base.ProcessContextMenu(mousePosition);

			var genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Add room"), false, () => OnClickAddRoom(mousePosition));
			genericMenu.ShowAsContext();
		}

		protected void OnClickAddRoom(Vector2 mousePosition)
		{
			var room = CreateInstance<Room>();

			room.Position = mousePosition;
			Data.Rooms.Add(room);
			AssetDatabase.AddObjectToAsset(room, Data);

			CreateNode(room);
		}

		protected void OnStartConnection(RoomNode roomNode, Event e)
		{
			if (connectionFrom == null)
			{
				Debug.Log("OnStartConnection");
				connectionFrom = roomNode;
				connectionProgress = new ConnectionProgressNode();
				connectionProgress.From = roomNode.Rect.center;
				connectionProgress.To = e.mousePosition;
			}
		}

		protected void OnEndConnection(RoomNode roomNode, Event e)
		{
			if (connectionFrom != null)
			{
				Debug.Log("OnEndConnection");

				var from = connectionFrom;
				var to = roomNode;

				foreach (var dataConnection in Data.Connections)
				{
					if ((dataConnection.From == from.Data && dataConnection.To == to.Data)
					    || (dataConnection.From == to.Data && dataConnection.To == from.Data))
					{
						return;
					}
				}

				var connection = CreateInstance<Connection>();
				connection.From = connectionFrom.Data;
				connection.To = roomNode.Data;

				Data.Connections.Add(connection);
				AssetDatabase.AddObjectToAsset(connection, Data);

				CreateConnection(connection, from, to);

				connectionFrom = null;
				connectionProgress = null;
				GUI.changed = true;
			}
		}

		protected RoomNode CreateNode(Room data)
		{
			var node = new RoomNode(data, 50, 50, roomNodeStyle, editorMode);

			node.OnDelete += OnDeleteRoomNode;
			node.OnStartConnection += OnStartConnection;
			node.OnEndConnection += OnEndConnection;
			roomNodes.Add(node);

			return node;
		}

		protected ConnectionNode CreateConnection(Connection data, RoomNode from, RoomNode to)
		{
			var node = new ConnectionNode(data, from, to);

			connectionNodes.Add(node);

			return node;
		}

		private void OnDeleteRoomNode(RoomNode node)
		{
			Data.Rooms.Remove(node.Data);
			DestroyImmediate(node.Data, true);
			roomNodes.Remove(node);

			var connectionsToRemove = new List<ConnectionNode>();
			foreach (var connectionNode in connectionNodes)
			{
				if (connectionNode.From == node || connectionNode.To == node)
				{
					connectionsToRemove.Add(connectionNode);
				}
			}

			foreach (var connectionNode in connectionsToRemove)
			{
				OnDeleteConnectionNode(connectionNode);
			}
		}

		private void OnDeleteConnectionNode(ConnectionNode node)
		{
			Data.Connections.Remove(node.Data);
			DestroyImmediate(node.Data, true);
			connectionNodes.Remove(node);
		}

		private class ConnectionProgressNode : IEditorNodeBase
		{
			public Vector3 From;

			public Vector3 To;

			public bool ProcessEvents(Event e)
			{
				GUI.changed = true;
				return false;
			}

			public void Draw()
			{
				Handles.DrawLine(From, Event.current.mousePosition);
			}

			public void Drag(Vector2 delta)
			{
				
			}
		}
	}
}