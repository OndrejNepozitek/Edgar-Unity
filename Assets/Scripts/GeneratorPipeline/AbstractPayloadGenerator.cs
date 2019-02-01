namespace Assets.Scripts.GeneratorPipeline
{
	using UnityEngine;

	public abstract class AbstractPayloadGenerator : ScriptableObject
	{
		public abstract object InitializePayload();
	}
}