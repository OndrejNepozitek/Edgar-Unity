namespace Assets.Scripts.DungeonGenerators
{
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu]
	public class SimpleDungeonGenerator : PipelineTask
	{
		public int Width;

		public int Height;
	}

	[PipelineTaskFor(typeof(SimpleDungeonGenerator))]
	public class SimpleDungeonGenerator<T> : IConfigurablePipelineTask<T, SimpleDungeonGenerator>
		where T : IGeneratorPayload
	{
		public SimpleDungeonGenerator Config { get; set; }

		public void Process(T payload)
		{
			for (var x = 0; x < Config.Width; x++)
			{
				for (var y = 0; y < Config.Height; y++)
				{
					if (x % 2 == 0 || y % 2 == 0)
					{
						payload.MarkerMap.SetMarker(new Vector3Int(x, y, 0), new Marker() { Type = MarkerType.Wall });
					}
				}
			}
		}
	}
}