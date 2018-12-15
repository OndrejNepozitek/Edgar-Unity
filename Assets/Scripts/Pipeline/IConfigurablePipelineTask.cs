namespace Assets.Scripts.Pipeline
{
	public interface IConfigurablePipelineTask<in TPayload, TConfig> : IPipelineTask<TPayload>
	{
		TConfig Config { get; set; }
	}
}