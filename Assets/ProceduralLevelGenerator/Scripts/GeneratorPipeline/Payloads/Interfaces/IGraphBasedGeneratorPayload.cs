namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
	using System.Collections.Generic;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using RoomTemplates;
	using UnityEngine;

	public interface IGraphBasedGeneratorPayload
	{
		MapDescription<int> MapDescription { get; set; }

		TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates { get; set; }

		Dictionary<int, RoomInfo<int>> LayoutData { get; set; }
	}
}