namespace Assets.Scripts.DungeonGenerators.GraphBasedGenerator
{
	using Data.Graphs;
	using Data.Rooms;
	using Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	[CreateAssetMenu(menuName = "Graph based generator", fileName = "GraphBasesGenerator")]
	public class GraphBasedGeneratorConfig : PipelineTask
	{
		public RoomTemplatesWrapper RoomTemplatesWrapper;

		public RoomTemplatesWrapper CorridorTemplatesWrapper;

		public LayoutGraph LayoutGraph;

		public GameObject Walls;

		public Tile DoorTile;

		public bool UseCorridors;

		public bool ShowElapsedTime;

		public bool AddDoors;

		public bool CorrectWalls;

		public bool CombineTilemaps;
	}
}