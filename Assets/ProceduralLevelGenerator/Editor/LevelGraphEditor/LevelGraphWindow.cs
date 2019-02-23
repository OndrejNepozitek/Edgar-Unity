namespace Assets.ProceduralLevelGenerator.Editor.LevelGraphEditor
{
	using System.Collections.Generic;
	using System.Linq;
	using EditorNodes;
	using NodeBasedEditor;
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;
	using RoomNode = EditorNodes.RoomNode;

	public class LevelGraphWindow : NodeBasedEditorBase
	{
		public LevelGraph Data { get; set; }

		private List<RoomNode> roomNodes = new List<RoomNode>();

		private List<ConnectionNode> connectionNodes = new List<ConnectionNode>();

		private GUIStyle roomNodeStyle;

		private GUIStyle connectionHandleStyle;

		private RoomNode connectionFrom;

		private ConnectionProgressNode connectionProgress;

		private EditorMode editorMode = EditorMode.Drag;

		private int selectedToolbar = 0;

		private int currentPickerWindow;

		private bool doNotDrag;

		public static LevelGraph StaticData { get; set; }

		public void Initialize()
		{
			SetupStyles();

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
			StaticData = Data;
		}

		public override void OnEnable()
		{
			SetupStyles();

			if (Data != null)
			{
				Initialize();
			}
		}

		protected void SetupStyles()
		{
			roomNodeStyle = new GUIStyle();
			roomNodeStyle.normal.background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f, 0.85f));
			roomNodeStyle.border = new RectOffset(12, 12, 12, 12);
			roomNodeStyle.normal.textColor = Color.white;
			roomNodeStyle.fontSize = 12;
			roomNodeStyle.alignment = TextAnchor.MiddleCenter;

			connectionHandleStyle = new GUIStyle();
			connectionHandleStyle.normal.background = MakeTex(1, 1, new Color(0.3f, 0.3f, 0.3f, 0.6f));
			connectionHandleStyle.border = new RectOffset(12, 12, 12, 12);
			connectionHandleStyle.normal.textColor = Color.white;
			connectionHandleStyle.fontSize = 12;
			connectionHandleStyle.alignment = TextAnchor.MiddleCenter;
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

			DrawMenuBar();
		}

		private void DrawMenuBar()
		{
			var menuBar = new Rect(0, 0, position.width, 20);

			GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
			GUILayout.BeginHorizontal();

			if (Data != null)
			{
				GUILayout.Label($"Selected graph: {Data.name}"); 
			}
			else
			{
				GUILayout.Label($"No graph selected");
			}

			if (GUILayout.Button(new GUIContent("Select in inspector"), EditorStyles.toolbarButton, GUILayout.Width(150)))
			{
				if (Data != null)
				{
					Selection.activeObject = Data;
				}
			}

			if (GUILayout.Button(new GUIContent("Select level graph"), EditorStyles.toolbarButton, GUILayout.Width(150)))
			{
				// Create a window picker control ID
				currentPickerWindow = GUIUtility.GetControlID(FocusType.Passive) + 100;

				// Use the ID you just created
				EditorGUIUtility.ShowObjectPicker<LevelGraph>(null, false, string.Empty, currentPickerWindow);
			}

			if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == currentPickerWindow)
			{
				currentPickerWindow = -1;
				Data = EditorGUIUtility.GetObjectPickerObject() as LevelGraph;

				if (Data != null)
				{
					Initialize();
				}
				else
				{
					ClearWindow();
				}

				doNotDrag = true;
			}

			GUILayout.EndHorizontal();
			GUILayout.EndArea();
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
						doNotDrag = true;
					}
					break;

				case EventType.MouseDrag:
					if (doNotDrag)
					{
						doNotDrag = false;
						break;
					}

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
				connectionFrom = roomNode;
				connectionProgress = new ConnectionProgressNode();
				connectionProgress.From = roomNode.Rect.center;
			}
		}

		protected void OnEndConnection(RoomNode roomNode, Event e)
		{
			if (connectionFrom == null)
				return;

			var from = connectionFrom;
			var to = roomNode;

			var connection = CreateInstance<Connection>();
			connection.From = connectionFrom.Data;
			connection.To = roomNode.Data;

			if (from != to && !connectionNodes.Any(x => (x.From == @from && x.To == to) || (x.To == @from && x.From == to)))
			{
				Data.Connections.Add(connection);
				AssetDatabase.AddObjectToAsset(connection, Data);

				CreateConnection(connection, @from, to);
			}
				
			connectionFrom = null;
			connectionProgress = null;
			GUI.changed = true;
		}

		protected RoomNode CreateNode(Room data)
		{
			var node = new RoomNode(data, 40, 40, roomNodeStyle, editorMode);

			node.OnDelete += () => OnDeleteRoomNode(node);
			node.OnStartConnection += (e) => OnStartConnection(node, e);
			node.OnEndConnection += (e) => OnEndConnection(node, e);
			roomNodes.Add(node);

			return node;
		}

		protected ConnectionNode CreateConnection(Connection data, RoomNode from, RoomNode to)
		{
			var node = new ConnectionNode(data, from, to, connectionHandleStyle, 12);

			node.OnDelete += () => OnDeleteConnectionNode(node);
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

		protected void ClearWindow()
		{
			connectionNodes.Clear();
			roomNodes.Clear();
		}

		private class ConnectionProgressNode : IEditorNodeBase
		{
			public Vector3 From;

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