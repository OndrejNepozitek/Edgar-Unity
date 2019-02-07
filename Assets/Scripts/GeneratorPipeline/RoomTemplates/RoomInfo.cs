namespace Assets.Scripts.GeneratorPipeline.RoomTemplates
{
	using MapGeneration.Interfaces.Core.MapLayouts;
	using UnityEngine;

	public class RoomInfo<TNode>
	{
		/// <summary>
		/// Information from the dungeon generator library.
		/// </summary>
		public IRoom<TNode> GeneratorData { get; set; }

		/// <summary>
		/// Room template that was selected for a given room.
		/// </summary>
		/// <remarks>
		/// This is the original saved asset used in the input.
		/// </remarks>
		public GameObject RoomTemplate { get; set; }

		/// <summary>
		/// Instace of the RoomTemplate that is correctly positioned/transformed.
		/// </summary>
		/// <remarks>
		/// This is a new instance of a correspoding room template.
		/// It is moved to a correct position and transformed/rotated.
		/// It can be used to copy tiles from the template to the combined tilemaps.
		/// </remarks>
		public GameObject Room { get; set; }

		/// <summary>
		/// Position of the room relative to the generated layout.
		/// </summary>
		/// <remarks>
		/// To obtain a position in the combined tilemaps, this position
		/// must be added to relative positions of tile in Room's tilemaps.
		/// </remarks>
		public Vector3Int Position { get; set; }
	}
}