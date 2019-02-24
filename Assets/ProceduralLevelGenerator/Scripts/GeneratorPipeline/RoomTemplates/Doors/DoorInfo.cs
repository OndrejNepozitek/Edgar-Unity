namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Doors
{
	using UnityEngine;
	using Utils;

	/// <summary>
	/// Class containing information about a door of a room.
	/// </summary>
	/// <typeparam name="TRoom"></typeparam>
	public class DoorInfo<TRoom>
	{
		/// <summary>
		/// Line containing all points of the door.
		/// </summary>
		public OrthogonalLine DoorLine { get; }

		/// <summary>
		/// Direction in which a room is connected to this door.
		/// </summary>
		/// <remarks>
		/// Imagine that we have the following room shape and that
		/// "OO" represents a door.
		/// 
		/// ----OO---
		/// |       | 
		/// |       |
		/// ---------
		/// 
		/// Then the facing direction is equal to Vector2Int.up.
		/// 
		/// ---------
		/// |       O 
		/// |       O
		/// ---------
		/// 
		/// Here the facing direction is equal to Vector2Int.right.
		/// </remarks>
		public Vector2Int FacingDirection { get; }

		/// <summary>
		/// Whether the door line is horizontal or vertical.
		/// </summary>
		public bool IsHorizontal { get; }

		/// <summary>
		/// To which room is the room that contains this door connected.
		/// </summary>
		public TRoom ConnectedRoom { get; }

		public DoorInfo(OrthogonalLine doorLine, Vector2Int facingDirection, TRoom connectedRoom)
		{
			DoorLine = doorLine;
			FacingDirection = facingDirection;
			ConnectedRoom = connectedRoom;
			IsHorizontal = FacingDirection == Vector2Int.up || FacingDirection == Vector2Int.down;
		}
	}
}