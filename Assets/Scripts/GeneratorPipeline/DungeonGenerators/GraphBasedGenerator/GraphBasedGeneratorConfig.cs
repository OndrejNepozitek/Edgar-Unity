namespace Assets.Scripts.GeneratorPipeline.DungeonGenerators.GraphBasedGenerator
{
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Graph based generator", fileName = "GraphBasesGenerator")]
	public class GraphBasedGeneratorConfig : PipelineConfig
	{
		public GameObject Walls;

		public bool ShowElapsedTime;

		public bool AddDoorMarkers;

		public bool CenterGrid;

		public bool ApplyTemplate;
	}
}