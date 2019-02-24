namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates
{
	using System.Collections.Generic;
	using Doors;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using UnityEngine;

	/// <summary>
	/// Class that holds information about a laid out room.
	/// </summary>
	/// <typeparam name="TRoom"></typeparam>
	public class RoomInfo<TRoom>
	{
		/// <summary>
		/// Room template that was selected for a given room.
		/// </summary>
		/// <remarks>
		/// This is the original saved asset used in the input.
		/// </remarks>
		public GameObject RoomTemplate { get; }

		/// <summary>
		/// Instace of the RoomTemplate that is correctly positioned/transformed.
		/// </summary>
		/// <remarks>
		/// This is a new instance of a correspoding room template.
		/// It is moved to a correct position and transformed/rotated.
		/// It can be used to copy tiles from the template to the combined tilemaps.
		/// </remarks>
		public GameObject Room { get; }

		/// <summary>
		/// Position of the room relative to the generated layout.
		/// </summary>
		/// <remarks>
		/// To obtain a position in the combined tilemaps, this position
		/// must be added to relative positions of tile in Room's tilemaps.
		/// </remarks>
		public Vector3Int Position { get; }

		/// <summary>
		/// List of doors.
		/// </summary>
		public List<DoorInfo<TRoom>> Doors { get; }

		/// <summary>
		/// Whether the room is a corridor room or not.
		/// </summary>
		public bool IsCorridor { get; }

		/// <summary>
		/// Information from the dungeon generator library.
		/// </summary>
		public IRoom<TRoom> GeneratorData { get; }

		public RoomInfo(GameObject roomTemplate, GameObject room, Vector3Int position, List<DoorInfo<TRoom>> doors, bool isCorridor, IRoom<TRoom> generatorData)
		{
			RoomTemplate = roomTemplate;
			Room = room;
			Position = position;
			Doors = doors;
			IsCorridor = isCorridor;
			GeneratorData = generatorData;
		}
	}
}