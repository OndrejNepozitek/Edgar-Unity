namespace Assets.ProceduralLevelGenerator.Scripts.Data.Rooms
{
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Currently not used.
	/// </summary>
	// [CreateAssetMenu(fileName = "RoomTemplates", menuName = "Dungeon generator/Room templates")]
	public class RoomTemplatesSet : ScriptableObject
	{
		public string Name = "Room set";

		[HideInInspector]
		public Vector2 Position;

		public List<RoomTemplate> Rooms = new List<RoomTemplate>();
	}
}