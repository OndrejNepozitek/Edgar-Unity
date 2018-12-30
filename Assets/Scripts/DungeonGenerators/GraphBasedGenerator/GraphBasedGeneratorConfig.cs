namespace Assets.Scripts.DungeonGenerators.GraphBasedGenerator
{
	using Data.Graphs;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Graph based generator", fileName = "GraphBasesGenerator")]
	public class GraphBasedGeneratorConfig : PipelineTask
	{
		public LayoutGraph LayoutGraph;

		public GameObject Walls;

		public Tile DoorTile;

		public bool UseCorridors;

		public bool ShowElapsedTime;

		public bool AddDoors;

		public bool CorrectWalls;

		public bool CombineTilemaps;

		public bool ApplyTemplate;
	}
}