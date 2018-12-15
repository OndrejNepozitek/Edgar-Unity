namespace Assets.Scripts.Pipeline
{
	public interface IPipelineTask<in TPayload>
	{
		void Process(TPayload payload);
	}
}