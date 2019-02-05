namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using UnityEngine;

	public interface IGraphBasedInputPayload
	{
		MapDescription<Room> MapDescription { get; set; }

		TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates { get; set; }
	}
}