namespace Assets.ProceduralLevelGenerator.Editor.RoomsEditor
{
	using System.Collections.Generic;
	using EditorNodes;
	using NodeBasedEditor;
	using Scripts.Data.Rooms;
	using UnityEngine;

	public class RoomsWindow : NodeBasedEditorBaseOld
	{
		public RoomTemplatesSet Data { get; set; }
		private GUIStyle roomNodeStyle;

		public RoomsWindow()
		{
			minSize = new Vector2(500, 500);
		}

		public void Initialize()
		{
			nodes = new List<IEditorNodeBase>();

			RemoveDestroyedTemplates();
			CreateNode(Data);
		}

		private void RemoveDestroyedTemplates()
		{
			if (Data == null)
				return;

			var toRemove = new List<RoomTemplate>();

			foreach (var roomTemplate in Data.Rooms)
			{
				if (roomTemplate.Tilemap == null)
				{
					toRemove.Add(roomTemplate);
				}
			}

			foreach (var roomTemplate in toRemove)
			{
				Data.Rooms.Remove(roomTemplate);
				Object.DestroyImmediate(roomTemplate, true);
			}
		}

		public override void OnEnable()
		{
			nodeStyle = new GUIStyle();
			nodeStyle.normal.background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f, 0.85f));
			nodeStyle.border = new RectOffset(12, 12, 12, 12);
			nodeStyle.normal.textColor = Color.white;
			nodeStyle.fontSize = 16;
			nodeStyle.alignment = TextAnchor.MiddleCenter;

			roomNodeStyle = new GUIStyle(nodeStyle);
			roomNodeStyle.alignment = TextAnchor.UpperCenter;
			roomNodeStyle.fontSize = 13;

			RemoveDestroyedTemplates();
		}

		private Texture2D MakeTex(int width, int height, Color col)
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

		protected override void ProcessContextMenu(Vector2 mousePosition)
		{ 
			base.ProcessContextMenu(mousePosition);

			//var genericMenu = new GenericMenu();
			//genericMenu.AddItem(new GUIContent("Add room set"), false, () => OnClickAddRoomSet(mousePosition));
			//genericMenu.ShowAsContext();
		}

		//protected void OnClickAddRoomSet(Vector2 mousePosition)
		//{
		//	if (nodes == null)
		//	{
		//		nodes = new List<IEditorNodeBase>();
		//	}

		//	var roomSet = CreateInstance<RoomTemplatesSet>();
		//	roomSet.Position = mousePosition;
		//	Data.RoomsSets.Add(roomSet);
		//	AssetDatabase.AddObjectToAsset(roomSet, Data);

		//	CreateNode(roomSet);
		//}

		protected RoomSetNode CreateNode(RoomTemplatesSet data)
		{
			var node = new RoomSetNode(data, 150, 50, nodeStyle, roomNodeStyle);
			// node.OnDeleted += OnDeleteNode;

			nodes.Add(node);

			return node;
		}

		//private void OnDeleteNode(RoomSetNode node)
		//{
		//	Data.RoomsSets.Remove(node.Data);
		//	DestroyImmediate(node.Data, true);
		//	nodes.Remove(node);
		//}

		protected override void ProcessEvents(Event e)
		{
			base.ProcessEvents(e);
		}
	}
}