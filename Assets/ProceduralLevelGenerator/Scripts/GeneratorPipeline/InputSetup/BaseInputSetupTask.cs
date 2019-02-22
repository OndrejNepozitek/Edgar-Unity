namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup
{
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

	public abstract class BaseInputSetupTask<TPayload, TConfig> : ConfigurablePipelineTask<TPayload, TConfig> 
		where TConfig : PipelineConfig 
		where TPayload : class, IGraphBasedGeneratorPayload
	{
		protected TwoWayDictionary<IRoomDescription, GameObject> RoomDescriptionsToRoomTemplates;

		protected RoomShapesLoader RoomShapesLoader;

		public override void Process()
		{
			Payload.MapDescription = SetupMapDescription();
			Payload.RoomDescriptionsToRoomTemplates = RoomDescriptionsToRoomTemplates;
		}

		protected abstract MapDescription<Room> SetupMapDescription();

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
	}
}