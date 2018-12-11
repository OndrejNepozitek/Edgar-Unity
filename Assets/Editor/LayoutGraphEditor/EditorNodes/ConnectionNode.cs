namespace Assets.Editor.LayoutGraphEditor.EditorNodes
{
	using NodeBasedEditor;
	using Scripts.Data.Graphs;
	using UnityEditor;
	using UnityEngine;

	public class ConnectionNode : IEditorNode<Connection>
	{
		public Connection Data { get; set; }

		public RoomNode From { get; set; }

		public RoomNode To { get; set; }

		public ConnectionNode(Connection data, RoomNode from, RoomNode to)
		{
			Data = data;
			From = from;
			To = to;
		}

		public bool ProcessEvents(Event e)
		{
			return false;
		}

		public void Draw()
		{
			Handles.DrawLine(From.Rect.center, To.Rect.center);
		}

		public void Drag(Vector2 delta)
		{
			
		}
	}
}