namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using UnityEngine;

	/// <summary>
	/// Represents a connection between two rooms.
	/// </summary>
	public class Connection : ScriptableObject, IConnection<Room>
	{
		[HideInInspector]
		public Room From;

        [HideInInspector]
		public Room To;

        Room IConnection<Room>.From => From;

        Room IConnection<Room>.To => To;
    }
}