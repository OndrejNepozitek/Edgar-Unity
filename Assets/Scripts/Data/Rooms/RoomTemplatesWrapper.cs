namespace Assets.Scripts.Data.Rooms
{
	using System.Collections.Generic;
	using UnityEngine;

	public class RoomTemplatesWrapper : ScriptableObject
	{
		public List<RoomTemplatesSet> RoomsSets = new List<RoomTemplatesSet>();
	}
}