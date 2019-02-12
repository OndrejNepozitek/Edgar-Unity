namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LevelGraph", menuName = "Dungeon generator/Level graph")]
	public class LevelGraph : ScriptableObject
	{
		[HideInInspector]
		public List<Room> Rooms = new List<Room>();

		[HideInInspector]
		public List<Connection> Connections = new List<Connection>();

		public List<RoomsGroup> RoomsGroups = new List<RoomsGroup>();

		public List<RoomTemplatesSet> DefaultRoomTemplateSets = new List<RoomTemplatesSet>();

		public List<GameObject> DefaultIndividualRoomTemplates = new List<GameObject>();

		public List<RoomTemplatesSet> CorridorRoomTemplateSets = new List<RoomTemplatesSet>();

		public List<GameObject> CorridorIndividualRoomTemplates = new List<GameObject>();
	}
}