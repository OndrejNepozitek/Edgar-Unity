namespace Assets.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Data.Graphs;
	using Data.Rooms;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using Payloads;
	using Pipeline;
	using UnityEngine;

	/// <summary>
	/// Pipeline task that prepares map description from a given layout graph.
	/// </summary>
	[CreateAssetMenu(menuName = "Dungeon generator/Input setup/Fixed input", fileName = "FixedInput")]
	public class FixedInputTask : PipelineTask<IGraphBasedInputPayload>
	{
		public LayoutGraph LayoutGraph;

		public bool UseCorridors;

		private TwoWayDictionary<IRoomDescription, GameObject> roomDescriptionsToRoomTemplates;

		private RoomShapesLoader roomShapesLoader;

		public override void Process()
		{
			SetupMapDescription();
		}

		protected void SetupMapDescription()
		{
			roomDescriptionsToRoomTemplates = new TwoWayDictionary<IRoomDescription, GameObject>();
			roomShapesLoader = new RoomShapesLoader();
			var mapDescription = new MapDescription<Room>();
			mapDescription.SetDefaultTransformations(new List<Transformation>() { Transformation.Identity });

			// Setup individual rooms
			foreach (var room in LayoutGraph.Rooms)
			{
				mapDescription.AddRoom(room);
				SetupRoomShapesForRoom(mapDescription, room);
			}

			// Add default room shapes
			SetupDefaultRoomShapes(mapDescription, LayoutGraph);

			// Add corridors
			if (UseCorridors)
			{
				SetupCorridorRoomShapes(mapDescription, LayoutGraph);
			}

			// Add passages
			foreach (var connection in LayoutGraph.Connections)
			{
				mapDescription.AddPassage(connection.From, connection.To);
			}

			Payload.MapDescription = mapDescription;
			Payload.RoomDescriptionsToRoomTemplates = roomDescriptionsToRoomTemplates;
		}

		/// <summary>
		/// Setups room shapes for a given room.
		/// </summary>
		/// <param name="room"></param>
		/// <param name="mapDescription"></param>
		protected void SetupRoomShapesForRoom(MapDescription<Room> mapDescription, Room room)
		{
			// Get assigned room templates
			var roomTemplatesSets = room.RoomTemplateSets;
			var individualRoomTemplates = room.IndividualRoomTemplates;

			// If the room is assigned to a Rooms group, use room templates for the group instead
			if (room.RoomsGroupGuid != Guid.Empty)
			{
				roomTemplatesSets = LayoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).RoomTemplateSets;
				individualRoomTemplates = LayoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).IndividualRoomTemplates;
			}

			var roomDescriptions = GetRoomDescriptions(roomTemplatesSets, individualRoomTemplates).Distinct();

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddRoomShapes(room, roomDescription);
			}
		}

		/// <summary>
		/// Setups default room shapes.
		/// These are used if a room does not have any room shapes assigned.
		/// </summary>
		/// <param name="mapDescription"></param>
		/// <param name="layoutGraph"></param>
		protected void SetupDefaultRoomShapes(MapDescription<Room> mapDescription, LayoutGraph layoutGraph)
		{
			var roomDescriptions = GetRoomDescriptions(layoutGraph.DefaultRoomTemplateSets, layoutGraph.DefaultIndividualRoomTemplates).Distinct();

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddRoomShapes(roomDescription);
			}
		}

		/// <summary>
		/// Setups corridor room shapes.
		/// </summary>
		/// <param name="mapDescription"></param>
		/// <param name="layoutGraph"></param>
		protected void SetupCorridorRoomShapes(MapDescription<Room> mapDescription, LayoutGraph layoutGraph)
		{
			var corridorLengths = new List<int>();
			var roomDescriptions = GetRoomDescriptions(layoutGraph.CorridorRoomTemplateSets, layoutGraph.CorridorIndividualRoomTemplate).Distinct();

			foreach (var roomDescription in roomDescriptions)
			{
				mapDescription.AddCorridorShapes(roomDescription);

				var corridorLength = roomShapesLoader.GetCorridorLength(roomDescription);
				corridorLengths.Add(corridorLength);
			}

			mapDescription.SetWithCorridors(true, corridorLengths.Distinct().ToList());
		}

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
			if (roomDescriptionsToRoomTemplates.ContainsValue(roomTemplate))
			{
				return (RoomDescription) roomDescriptionsToRoomTemplates.GetByValue(roomTemplate);
			}

			var roomDescription = roomShapesLoader.GetRoomDescription(roomTemplate);
			roomDescriptionsToRoomTemplates.Add(roomDescription, roomTemplate);

			return roomDescription;
		}
	}
}