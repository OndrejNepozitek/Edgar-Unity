namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.GraphBasedGenerator
{
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Graph based generator", fileName = "GraphBasedGenerator")]
	public class GraphBasedGeneratorConfig : PipelineConfig
	{
		public bool ShowDebugInfo = true;

		public bool CenterGrid = true;

		public bool ApplyTemplate = true;

		public int Timeout = 10000;
	}
}