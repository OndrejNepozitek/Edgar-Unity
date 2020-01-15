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
		protected TwoWayDictionary<IRoomTemplate, GameObject> RoomTemplatesToGameObjects;

		protected RoomShapesLoader RoomShapesLoader;

		public override void Process()
		{
			RoomTemplatesToGameObjects = new TwoWayDictionary<IRoomTemplate, GameObject>();
			RoomShapesLoader = new RoomShapesLoader();

			Payload.MapDescription = SetupMapDescription();
			Payload.RoomDescriptionsToRoomTemplates = RoomTemplatesToGameObjects;
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
		protected List<IRoomTemplate> GetRoomTemplates(List<RoomTemplatesSet> roomTemplatesSets, List<GameObject> individualRoomTemplates)
		{
			var result = new List<IRoomTemplate>();

			// Add room templates from template sets
			foreach (var roomTemplatesSet in roomTemplatesSets.Where(x => x != null))
			{
				foreach (var roomTemplate in roomTemplatesSet.Rooms.Where(x => x != null))
				{
					var roomDescription = GetRoomTemplate(roomTemplate.Tilemap);
					result.Add(roomDescription);
				}
			}

			// Add room templates that are not part of a set
			foreach (var roomTemplate in individualRoomTemplates.Where(x => x != null))
			{
				var roomDescription = GetRoomTemplate(roomTemplate);
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
		/// <param name="roomTemplateGameObject"></param>
		/// <returns></returns>
		protected IRoomTemplate GetRoomTemplate(GameObject roomTemplateGameObject)
		{
			if (RoomTemplatesToGameObjects.ContainsValue(roomTemplateGameObject))
			{
				return RoomTemplatesToGameObjects.GetByValue(roomTemplateGameObject);
			}

			var roomTemplate = RoomShapesLoader.GetRoomDescription(roomTemplateGameObject);
			RoomTemplatesToGameObjects.Add(roomTemplate, roomTemplateGameObject);

			return roomTemplate;
		}
    }
}