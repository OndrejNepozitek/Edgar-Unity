namespace Assets.Scripts.Data.Graphs
{
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LayoutGraph", menuName = "Layout graph")]
	public class LayoutGraph : ScriptableObject
	{
		public List<Room> Rooms = new List<Room>();

		public List<Connection> Connections = new List<Connection>();
	}
}