namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	/// <summary>
	/// Basic generator pipeline payload.
	/// </summary>
	public interface IGeneratorPayload
	{
		/// <summary>
		/// Gameobject that holds dungeon tilemaps and possibly other game objects.
		/// </summary>
		GameObject GameObject { get; set; }

		/// <summary>
		/// Tilemaps of the generated dungeon.
		/// </summary>
		List<Tilemap> Tilemaps { get; set; }
	}
}