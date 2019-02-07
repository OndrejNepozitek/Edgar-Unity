namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	public abstract class PipelineTask<TPayload> : PipelineItem, IPipelineTask<TPayload> 
		where TPayload : class
	{
		public TPayload Payload { get; set; }

		public abstract void Process();
	}

	public abstract class PipelineConfig : PipelineItem
	{

	}
}