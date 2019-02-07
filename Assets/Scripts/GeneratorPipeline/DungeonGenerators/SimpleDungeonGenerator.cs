namespace Assets.Scripts.GeneratorPipeline.DungeonGenerators
{
	using GeneratorPipeline;
	using Markers;
	using Payloads;
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Generators/Simple dungeon generator")]
	public class SimpleDungeonGenerator : PipelineConfig
	{
		public int Width;

		public int Height;
	}

	public class SimpleDungeonGenerator<TPayload> : ConfigurablePipelineTask<TPayload, SimpleDungeonGenerator>
		where TPayload : class, IGeneratorPayload
	{
		public override void Process()
		{
			// TODO:
			//for (var x = 0; x < Config.Width; x++)
			//{
			//	for (var y = 0; y < Config.Height; y++)
			//	{
			//		if (x % 2 == 0 || y % 2 == 0)
			//		{
			//			Payload.MarkerMaps[0].SetMarker(new Vector3Int(x, y, 0), new Marker() { Type = MarkerTypes.Wall });
			//		}
			//	}
			//}
		}
	}
}