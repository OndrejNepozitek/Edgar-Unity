namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
	using GeneralAlgorithms.DataStructures.Common;

	public interface IRoomToIntMappingPayload<TRoom>
	{
		TwoWayDictionary<TRoom, int> RoomToIntMapping { get; set; }
	}
}