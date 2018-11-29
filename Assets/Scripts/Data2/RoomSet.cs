namespace Assets.Scripts.Data2
{
	using System.Collections.Generic;
	using UnityEngine;

	public class RoomSet : ScriptableObject
	{
		public string Name = "Room set";

		[HideInInspector]
		public Vector2 Position;

		public List<Room> Rooms = new List<Room>();
	}
}