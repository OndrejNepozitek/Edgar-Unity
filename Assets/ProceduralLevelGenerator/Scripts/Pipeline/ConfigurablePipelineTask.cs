namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	public abstract class ConfigurablePipelineTask<TPayload, TConfig> : IConfigurablePipelineTask<TPayload, TConfig> 
		where TConfig : PipelineConfig 
		where TPayload : class
	{
		public TPayload Payload { get; set; }

		public TConfig Config { get; set; }

		public abstract void Process();
	}
}