namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using UnityEngine;

	/// <summary>
	/// Represents a connection between two rooms.
	/// </summary>
	public class Connection : ScriptableObject
	{
		[HideInInspector]
		public Room From;

        [HideInInspector]
		public Room To;
	}
}