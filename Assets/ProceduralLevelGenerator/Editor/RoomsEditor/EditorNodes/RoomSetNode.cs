namespace Assets.ProceduralLevelGenerator.Editor.RoomsEditor.EditorNodes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using NodeBasedEditor;
	using Scripts.Data.Rooms;
	using UnityEditor;
	using UnityEngine;
	using Object = UnityEngine.Object;

	public class RoomSetNode : IEditorNode<RoomTemplatesSet>
	{
		private readonly GUIStyle style;
		private readonly GUIStyle roomNodeStyle;

		public RoomTemplatesSet Data { get; set; }
		private Rect rect;
		private bool isDragged;

		public Action<RoomSetNode> OnDeleted;

		private readonly List<RoomNode> roomNodes = new List<RoomNode>();

		public RoomSetNode(RoomTemplatesSet data, float width, float height, GUIStyle style, GUIStyle roomNodeStyle)
		{
			Data = data;
			this.style = style;
			this.roomNodeStyle = roomNodeStyle;
			rect = new Rect(Data.Position.x, Data.Position.y, width, height);

			foreach (var room in data.Rooms)
			{
				CreateNode(room);
			}

			RecomputeRoomNodesPositions();
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
							Selection.activeObject = Data;
							e.Use();
							isDragged = true;
						}

						if (roomNodes.Any(x => x.Rect.Contains(e.mousePosition)))
						{
							isDragged = true;
						}
					}

					if (e.button == 1)
					{
						if (rect.Contains(e.mousePosition))
						{
							ProcessContextMenu();
							e.Use();
						}
					}

					break;

				case EventType.MouseUp:
					isDragged = false;
					break;

				case EventType.MouseDrag:
					if (e.button == 0 && isDragged)
					{
						Drag(e.delta);
						e.Use();
					}
					break;

				case EventType.DragUpdated:
				case EventType.DragPerform:

					if (!rect.Contains(e.mousePosition))
					{
						break;
					}

					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

					if (e.type == EventType.DragPerform)
					{
						DragAndDrop.AcceptDrag();
						var draggedObject = DragAndDrop.objectReferences.FirstOrDefault();

						if (draggedObject != null && draggedObject is GameObject)
						{
							AddRoom((GameObject)draggedObject);
						}
					}

					break;
			}

			foreach (var roomNode in roomNodes)
			{
				roomNode.ProcessEvents(e);
			}

			return false;
		}

		private void ProcessContextMenu()
		{
			var genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Add room"), false, () => AddRoom());
			genericMenu.AddItem(new GUIContent("Delete set"), false, OnClickDelete);
			genericMenu.ShowAsContext();
		}

		private void OnClickDelete()
		{
			OnDeleted?.Invoke(this);
		}

		private void OnDeleteRoom(RoomNode node)
		{
			Data.Rooms.Remove(node.Data);
			Object.DestroyImmediate(node.Data, true);
			roomNodes.Remove(node);

			RecomputeRoomNodesPositions();
		}

		private void AddRoom(GameObject tilemap = null)
		{
			var room = ScriptableObject.CreateInstance<RoomTemplate>();
			room.Tilemap = tilemap;

			Data.Rooms.Add(room);
			AssetDatabase.AddObjectToAsset(room, Data);

			CreateNode(room);

			RecomputeRoomNodesPositions();
		}

		private void RecomputeRoomNodesPositions()
		{
			var nodeWidth = 120;
			var nodesSpace = 20;
			var y = rect.yMax + 50;
			var totalWidth = roomNodes.Count * nodeWidth + (roomNodes.Count - 1) * nodesSpace;
			var startX = rect.center.x - (totalWidth / 2);

			for (var i = 0; i < roomNodes.Count; i++)
			{
				var roomNode = roomNodes[i];
				var x = startX + i * (nodeWidth + nodesSpace);

				roomNode.Rect = new Rect(x, y, nodeWidth, nodeWidth);
			}
		}

		protected RoomNode CreateNode(RoomTemplate data)
		{
			var node = new RoomNode(data, roomNodeStyle);

			roomNodes.Add(node);
			node.OnDeleted += OnDeleteRoom;

			return node;
		}

		public void Draw()
		{
			foreach (var roomNode in roomNodes)
			{
				if (roomNode.Data.Tilemap == null)
					continue;

				var startPoint = new Vector3(rect.center.x, rect.yMax);
				var endPoint = new Vector3(roomNode.Rect.center.x, roomNode.Rect.yMin);
				var midPoint1 = new Vector3(Vector3.Lerp(startPoint, endPoint, 0.1f).x, Vector3.Lerp(startPoint, endPoint, 0.48f).y);
				var midPoint2 = new Vector3(Vector3.Lerp(startPoint, endPoint, 0.9f).x, Vector3.Lerp(startPoint, endPoint, 0.52f).y);

				//Handles.DrawLine(new Vector3(rect.center.x, rect.yMax - 10), new Vector3(roomNode.Rect.center.x, roomNode.Rect.yMin + 5));
				Handles.DrawBezier(startPoint, endPoint, midPoint1, midPoint2, Color.gray, null, 2f);
			}
		
			GUI.Box(rect, Data.Name, style);

			foreach (var roomNode in roomNodes)
			{
				roomNode.Draw();
			}
		}

		public void Drag(Vector2 delta)
		{
			rect.position += delta;
			Data.Position += delta;

			foreach (var roomNode in roomNodes)
			{
				roomNode.Drag(delta);
			}
		}
	}
}