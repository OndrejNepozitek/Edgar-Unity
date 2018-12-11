namespace Assets.Scripts.Data.Graphs
{
	using UnityEngine;

	public class Room : ScriptableObject
	{
		public string Name = "Room";

		[HideInInspector]
		public Vector2 Position;
	}
}