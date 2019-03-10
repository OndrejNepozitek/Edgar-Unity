namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Data.Graphs;
	using Data.Rooms;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using Payloads.Interfaces;
	using Pipeline;
	using UnityEngine;
	using Utils;

	public abstract class InputSetupBaseTask<TPayload, TConfig> : ConfigurablePipelineTask<TPayload, TConfig> 
		where TConfig : PipelineConfig 
		where TPayload : class, IGraphBasedGeneratorPayload
	{
		/// <summary>
		/// Mapping between room descriptions and gameobjects.
		/// </summary>
		protected TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates;

		protected RoomShapesLoader RoomShapesLoader;

		public override void Process()
		{
			RoomDescriptionsToRoomTemplates = new TwoWayDictionary<IRoomDescription, GameObject>();
			RoomShapesLoader = new RoomShapesLoader();

			Payload.MapDescription = SetupMapDescription();
			Payload.RoomDescriptionsToRoomTemplates = RoomDescriptionsToRoomTemplates;
		}

		/// <summary>
		/// Returns map description.
		/// </summary>
		/// <returns></returns>
		protected abstract MapDescription<int> SetupMapDescription();

		/// <summary>
		/// Gets all room descriptions from given template sets and individual room templates.
		/// </summary>
		/// <param name="roomTemplatesSets"></param>
		/// <param name="individualRoomTemplates"></param>
		/// <returns></returns>
		protected List<RoomDescription> GetRoomDescriptions(List<RoomTemplatesSet> roomTemplatesSets, List<GameObject> individualRoomTemplates)
		{
			var result = new List<RoomDescription>();

			// Add room templates from template sets
			foreach (var roomTemplatesSet in roomTemplatesSets.Where(x => x != null))
			{
				foreach (var roomTemplate in roomTemplatesSet.Rooms.Where(x => x != null))
				{
					var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
					result.Add(roomDescription);
				}
			}

			// Add room templates that are not part of a set
			foreach (var roomTemplate in individualRoomTemplates.Where(x => x != null))
			{
				var roomDescription = GetRoomDescription(roomTemplate);
				result.Add(roomDescription);
			}

			return result;
		}

		/// <summary>
		/// Gets room description from a given room template.
		/// </summary>
		/// <remarks>
		/// Returns cached result if a given room template was already processed.
		/// </remarks>
		/// <param name="roomTemplate"></param>
		/// <returns></returns>
		protected RoomDescription GetRoomDescription(GameObject roomTemplate)
		{
			if (RoomDescriptionsToRoomTemplates.ContainsValue(roomTemplate))
			{
				return (RoomDescription)RoomDescriptionsToRoomTemplates.GetByValue(roomTemplate);
			}

			var roomDescription = RoomShapesLoader.GetRoomDescription(roomTemplate);
			RoomDescriptionsToRoomTemplates.Add(roomDescription, roomTemplate);

			return roomDescription;
		}

		/// <summary>
		/// Setups corridor room shapes.
		/// </summary>
		/// <param name="mapDescription"></param> 
		/// <param name="corridorRoomDescriptions"></param>
		protected void SetupCorridorRoomShapes(MapDescription<int> mapDescription, List<RoomDescription> corridorRoomDescriptions)
		{
			var corridorLengths = new List<int>();

			if (corridorRoomDescriptions.Count == 0)
			{
				throw new ArgumentException("There must be at least 1 corridor room template if corridors are enabled.");
			}

			foreach (var roomDescription in corridorRoomDescriptions)
			{
				mapDescription.AddCorridorShapes(roomDescription);

				var corridorLength = RoomShapesLoader.GetCorridorLength(roomDescription);
				corridorLengths.Add(corridorLength);
			}

			mapDescription.SetWithCorridors(true, corridorLengths.Distinct().ToList());
		}
	}
}