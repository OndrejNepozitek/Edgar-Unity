namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.PayloadInitializers
{
	using UnityEngine;

	/// <summary>
	/// Class that is able to initializer a pipeline payload.
	/// </summary>
	public abstract class AbstractPayloadInitializer : ScriptableObject
	{
		/// <summary>
		/// Returns a new instance of a pipeline payload.
		/// </summary>
		/// <returns></returns>
		public abstract object InitializePayload();
	}
}