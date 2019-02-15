namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Doors
{
	using UnityEngine;
	using Utils;

	public class DoorInfo<TRoom>
	{
		public OrthogonalLine DoorLine { get; }

		public Vector2Int FacingDirection { get; }

		public bool IsHorizontal { get; }

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