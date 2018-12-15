namespace Assets.Scripts.Pipeline
{
	using UnityEngine;

	public abstract class PayloadGenerator : ScriptableObject
	{
		public abstract object InitializePayload();
	}
}