namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;

	/// <summary>
	/// Represents a level graph.
	/// </summary>
	[CreateAssetMenu(fileName = "LevelGraph", menuName = "Dungeon generator/Level graph")]
	public class LevelGraph : ScriptableObject
	{
		/// <summary>
		/// List of rooms in the graph.
		/// </summary>
		[HideInInspector]
		public List<Room> Rooms = new List<Room>();

		/// <summary>
		/// List of connections in the graph.
		/// </summary>
		[HideInInspector]
		public List<Connection> Connections = new List<Connection>();

		/// <summary>
		/// List of rooms groups.
		/// </summary>
		public List<RoomsGroup> RoomsGroups = new List<RoomsGroup>();

		/// <summary>
		/// Sets of default room templates.
		/// </summary>
		/// <remarks>
		/// This functionality is not included in GUI because it is not ready.
		/// </remarks>
		public List<RoomTemplatesSet> DefaultRoomTemplateSets = new List<RoomTemplatesSet>();

		/// <summary>
		/// Set of room templates that is used for room thah do not have any room templates assigned.
		/// </summary>
		public List<GameObject> DefaultIndividualRoomTemplates = new List<GameObject>();

		/// <summary>
		/// Sets of corridor room templates.
		/// </summary>
		/// <remarks>
		/// This functionality is not included in GUI because it is not ready.
		/// </remarks>
		public List<RoomTemplatesSet> CorridorRoomTemplateSets = new List<RoomTemplatesSet>();

		/// <summary>
		/// Set of room templates that are used for corridor rooms.
		/// </summary>
		public List<GameObject> CorridorIndividualRoomTemplates = new List<GameObject>();
	}
}