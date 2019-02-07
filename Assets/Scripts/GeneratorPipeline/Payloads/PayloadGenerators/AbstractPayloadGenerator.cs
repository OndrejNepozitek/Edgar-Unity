namespace Assets.Scripts.GeneratorPipeline.Payloads.PayloadGenerators
{
	using UnityEngine;

	public abstract class AbstractPayloadGenerator : ScriptableObject
	{
		public abstract object InitializePayload();
	}
}