namespace Assets.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.LayoutConverters.CorridorNodesCreators;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using Payloads;
	using Pipeline;
	using UnityEngine;

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

				var roomTemplatesSets = room.RoomTemplateSets;
				var individualRoomTemplates = room.IndividualRoomTemplates;

				if (room.RoomsGroupGuid != Guid.Empty)
				{
					roomTemplatesSets = LayoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).RoomTemplateSets;
					individualRoomTemplates = LayoutGraph.RoomsGroups.Single(x => x.Guid == room.RoomsGroupGuid).IndividualRoomTemplates;
				}

				foreach (var roomTemplatesSet in roomTemplatesSets)
				{
					foreach (var roomTemplate in roomTemplatesSet.Rooms)
					{
						var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
						mapDescription.AddRoomShapes(room, roomDescription);
					}
				}

				foreach (var roomTemplate in individualRoomTemplates)
				{
					var roomDescription = GetRoomDescription(roomTemplate);
					mapDescription.AddRoomShapes(room, roomDescription);
				}
			}

			// Add default room shapes
			foreach (var roomTemplatesSet in LayoutGraph.DefaultRoomTemplateSets.Where(x => x != null))
			{
				foreach (var roomTemplate in roomTemplatesSet.Rooms.Where(x => x != null))
				{
					var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
					mapDescription.AddRoomShapes(roomDescription);
				}
			}

			foreach (var roomTemplate in LayoutGraph.DefaultIndividualRoomTemplates.Where(x => x != null))
			{
				var roomDescription = GetRoomDescription(roomTemplate);
				mapDescription.AddRoomShapes(roomDescription);
			}

			// Add corridors
			if (UseCorridors)
			{
				var corridorLengths = new List<int>();

				foreach (var roomTemplatesSet in LayoutGraph.CorridorRoomTemplateSets.Where(x => x != null))
				{
					foreach (var roomTemplate in roomTemplatesSet.Rooms.Where(x => x != null))
					{
						var roomDescription = GetRoomDescription(roomTemplate.Tilemap);
						mapDescription.AddCorridorShapes(roomDescription);
						var corridorLength = roomShapesLoader.GetCorridorLength(roomDescription);
						corridorLengths.Add(corridorLength);
					}
				}

				foreach (var roomTemplate in LayoutGraph.CorridorIndividualRoomTemplate.Where(x => x != null))
				{
					var roomDescription = GetRoomDescription(roomTemplate);
					mapDescription.AddCorridorShapes(roomDescription);
					var corridorLength = roomShapesLoader.GetCorridorLength(roomDescription);
					corridorLengths.Add(corridorLength);
				}

				mapDescription.SetWithCorridors(true, corridorLengths.Distinct().ToList());
			}

			// Add passages
			foreach (var connection in LayoutGraph.Connections)
			{
				mapDescription.AddPassage(connection.From, connection.To);
			}

			Payload.MapDescription = mapDescription;
			Payload.RoomDescriptionsToRoomTemplates = roomDescriptionsToRoomTemplates;
		}

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