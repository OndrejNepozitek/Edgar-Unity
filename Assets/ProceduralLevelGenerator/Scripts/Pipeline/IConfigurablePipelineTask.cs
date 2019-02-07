namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	public interface IConfigurablePipelineTask<TPayload, TConfig> : IPipelineTask<TPayload>
		where TConfig : PipelineConfig
		where TPayload : class
	{
		TConfig Config { get; set; }
	}
}