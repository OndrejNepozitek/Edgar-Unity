namespace Assets.ProceduralLevelGenerator.Scripts.Pipeline
{
	/// <summary>
	/// Base class for configurable pipeline tasks.
	/// </summary>
	/// <typeparam name="TPayload">Type of payload.</typeparam>
	/// <typeparam name="TConfig">Type of config.</typeparam>
	public abstract class ConfigurablePipelineTask<TPayload, TConfig> : IConfigurablePipelineTask<TPayload, TConfig> 
		where TConfig : PipelineConfig 
		where TPayload : class
	{
		/// <summary>
		/// Payload object.
		/// </summary>
		public TPayload Payload { get; set; }

		/// <summary>
		/// Config object.
		/// </summary>
		public TConfig Config { get; set; }

		/// <summary>
		/// Method containing all the logic of the task.
		/// </summary>
		/// <remarks>
		/// When this method is called, both Payload and Config properties are already set.
		/// </remarks>
		public abstract void Process();
	}
}