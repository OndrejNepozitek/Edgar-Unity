namespace Assets.Resources.Docs.Pipeline.Example2
{
	using ProceduralLevelGenerator.Scripts.Pipeline;
	using UnityEngine;
	using Random = System.Random;

	public interface IDungeonPayload
	{
		int Size { get; set; }
	}

	public interface IRandomGeneratorPayload
	{
		Random Random { get; set; }
	}

	public class Payload : IRandomGeneratorPayload, IDungeonPayload
	{
		public Random Random { get; set; }

		public int Size { get; set; }
	}

	[CreateAssetMenu(menuName = "Example tasks/Random size task", fileName = "RandomSizeTask")]
	public class RandomSizeConfig : PipelineConfig
	{
		public int MinSize;

		public int MaxSize;
	}

	public class RandomSizeTask<TPayload> : ConfigurablePipelineTask<TPayload, RandomSizeConfig> 
		where TPayload : class, IRandomGeneratorPayload, IDungeonPayload
	{
		public override void Process()
		{
			Payload.Size = Payload.Random.Next(Config.MinSize, Config.MaxSize);
		}
	}
}