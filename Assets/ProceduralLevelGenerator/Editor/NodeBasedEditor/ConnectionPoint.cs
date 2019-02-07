namespace Assets.ProceduralLevelGenerator.Editor.NodeBasedEditor
{
	using System;
	using UnityEngine;

	public enum ConnectionPointType { Left, Top, Right, Bottom }

	public class ConnectionPoint
	{
		public Rect rect;

		public ConnectionPointType type;

		public Node node;

		public GUIStyle style;

		public Action<ConnectionPoint> OnClickConnectionPoint;

		public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
		{
			this.node = node;
			this.type = type;
			this.style = style;
			this.OnClickConnectionPoint = OnClickConnectionPoint;
			rect = new Rect(0, 0, 10f, 20f);
		}

		public void Draw()
		{
		

			switch (type)
			{
				case ConnectionPointType.Left:
					rect.x = node.rect.x - rect.width + 8f;
					rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;
					break;

				case ConnectionPointType.Right:
					rect.x = node.rect.x + node.rect.width - 8f;
					rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;
					break;

				case ConnectionPointType.Top:
					rect.y = node.rect.y + rect.height + 8f;
					rect.x = node.rect.x + (node.rect.width * 0.5f) - rect.width * 0.5f;
					break;

				case ConnectionPointType.Bottom:
					rect.y = node.rect.y - rect.height + 8f;
					rect.x = node.rect.x + (node.rect.width * 0.5f) - rect.width * 0.5f;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			if (GUI.Button(rect, "", style))
			{
				if (OnClickConnectionPoint != null)
				{
					OnClickConnectionPoint(this);
				}
			}
		}
	}
}