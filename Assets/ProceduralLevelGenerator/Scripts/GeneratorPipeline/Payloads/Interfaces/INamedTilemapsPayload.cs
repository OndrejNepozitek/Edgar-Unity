namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
	using UnityEngine.Tilemaps;

	/// <summary>
	/// Represents a payload with named tilemaps.
	/// </summary>
	public interface INamedTilemapsPayload
	{
		Tilemap WallsTilemap { get; }

		Tilemap FloorTilemap { get; }

		Tilemap CollideableTilemap { get; }
	}
}