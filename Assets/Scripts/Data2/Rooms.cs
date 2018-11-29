namespace Assets.Scripts.Data2
{
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu()]
	public class Rooms : ScriptableObject
	{
		public List<RoomSet> RoomsSets = new List<RoomSet>();
	}
}