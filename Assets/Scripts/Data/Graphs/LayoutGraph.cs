namespace Assets.Scripts.Data.Graphs
{
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LayoutGraph", menuName = "Layout graph")]
	public class LayoutGraph : ScriptableObject
	{
		[HideInInspector]
		public List<Room> Rooms = new List<Room>();

		[HideInInspector]
		public List<Connection> Connections = new List<Connection>();

		public List<RoomsGroup> RoomsGroups = new List<RoomsGroup>();

		public List<RoomTemplatesSet> DefaultRoomTemplateSets = new List<RoomTemplatesSet>();

		public List<GameObject> DefaultIndividualRoomTemplates = new List<GameObject>();

		public List<RoomTemplatesSet> CorridorRoomTemplateSets = new List<RoomTemplatesSet>();

		public List<GameObject> CorridorIndividualRoomTemplate = new List<GameObject>();
	}
}