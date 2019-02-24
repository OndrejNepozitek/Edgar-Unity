namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using UnityEngine;

	/// <summary>
	/// Represents a connection between two rooms.
	/// </summary>
	public class Connection : ScriptableObject
	{
		public Room From;

		public Room To;
	}
}