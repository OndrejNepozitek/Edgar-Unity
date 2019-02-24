namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	/// <summary>
	/// Represents configurable pipeline tasks.
	/// </summary>
	/// <typeparam name="TPayload">Type of payload.</typeparam>
	/// <typeparam name="TConfig">Type of config.</typeparam>
	public interface IConfigurablePipelineTask<TPayload, TConfig> : IPipelineTask<TPayload>
		where TConfig : PipelineConfig
		where TPayload : class
	{
		TConfig Config { get; set; }
	}
}