namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.Platformers
{
	using Pipeline;
	using UnityEngine;

	/// <summary>
	/// Represents a pipeline item that is used to generate dungeons.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Platformer generator", fileName = "PlatformerGenerator")]
	public class PlatformerGeneratorConfig : PipelineConfig
	{
		/// <summary>
		/// Whether to show debug info.
		/// </summary>
		public bool ShowDebugInfo = true;

		/// <summary>
		/// Whether to center grid after a dungeon is generated.
		/// </summary>
		public bool CenterGrid = true;

		/// <summary>
		/// Whether to copy tiles from individual room templates to the
		/// common tilemaps that will hold the generated dungeon.
		/// </summary>
		public bool ApplyTemplate = true;

		/// <summary>
		/// Number of millisecons before the current attempt to generate
		/// a layout is aborted.
		/// </summary>
		public int Timeout = 10000;
	}
}