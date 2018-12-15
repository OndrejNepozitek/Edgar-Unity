namespace Assets.Scripts.Pipeline.POC
{
	using GeneratorPipeline;

	public class Task4
	{


		public class Task<T> : IConfigurablePipelineTask<T, Task4>
			where T : IGeneratorPayload
		{
			public Task4 Config { get; set; }

			public void Process(T payload)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}