namespace Assets.Scripts.Data.Rooms
{
	using System.Collections.Generic;
	using UnityEngine;

	public class RoomTemplatesSet : ScriptableObject
	{
		public string Name = "Room set";

		[HideInInspector]
		public Vector2 Position;

		public List<RoomTemplate> Rooms = new List<RoomTemplate>();
	}
}