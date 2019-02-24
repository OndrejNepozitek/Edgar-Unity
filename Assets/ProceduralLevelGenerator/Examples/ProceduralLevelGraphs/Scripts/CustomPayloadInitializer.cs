namespace Assets.ProceduralLevelGenerator.Examples.ProceduralLevelGraphs.Scripts
{
	using ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.PayloadInitializers;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Examples/Procedural level graphs/Payload initializer", fileName = "PayloadInitializer")]
	public class CustomPayloadInitializer : PayloadInitializer
	{
		public override object InitializePayload()
		{
			return InitializePayload<Room>();
		}
	}
}
