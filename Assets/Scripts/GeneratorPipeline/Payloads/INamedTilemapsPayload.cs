namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using UnityEngine.Tilemaps;

	public interface INamedTilemapsPayload
	{
		Tilemap WallsTilemap { get; }

		Tilemap FloorTilemap { get; }

		Tilemap CollideableTilemap { get; }
	}
}