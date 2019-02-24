namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
	using GeneralAlgorithms.DataStructures.Common;

	/// <summary>
	/// Represents a payload that provides a mapping from a custom room type to integers.
	/// </summary>
	/// <remarks>
	/// Currently the graph-based dungeon generator works only with integers so
	/// we need to map our room to integers.
	/// </remarks>
	/// <typeparam name="TRoom"></typeparam>
	public interface IRoomToIntMappingPayload<TRoom>
	{
		TwoWayDictionary<TRoom, int> RoomToIntMapping { get; set; }
	}
}