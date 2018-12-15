namespace Assets.Scripts.Pipeline.POC
{
	using UnityEngine;

	[CreateAssetMenu]
	public class Task1 : PipelineTask, IPipelineTask<Payload1>
	{
		public void Process(Payload1 payload)
		{
			Debug.Log(GetType());
		}
	}
}