namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.PayloadInitializers
{
	using UnityEngine;

	public abstract class AbstractPayloadInitializer : ScriptableObject
	{
		public abstract object InitializePayload();
	}
}