namespace Assets.Scripts.Pipeline.POC
{
	using UnityEngine;

	[CreateAssetMenu]
	public class Task3 : PipelineTask
	{
		
	}

	public class Task3<T> : Task3, IPipelineTask<T>
	{
		public void Process(T payload)
		{
			throw new System.NotImplementedException();
		}
	}
}