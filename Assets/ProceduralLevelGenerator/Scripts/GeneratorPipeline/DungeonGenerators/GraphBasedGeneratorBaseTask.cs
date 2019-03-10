namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutGenerator;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using Payloads.Interfaces;
	using Pipeline;
	using RoomTemplates;
	using RoomTemplates.Doors;
	using RoomTemplates.Transformations;
	using UnityEngine;
	using Utils;
	using Object = UnityEngine.Object;
	using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

	public abstract class GraphBasedGeneratorBaseTask<TPayload, TConfig, TRoom> : ConfigurablePipelineTask<TPayload, TConfig> 
		where TConfig : PipelineConfig
		where TPayload : class, IGeneratorPayload
	{
		public abstract override void Process();

		protected IMapLayout<TRoom> GenerateLayout(MapDescription<TRoom> mapDescription, ILayoutGenerator<MapDescription<TRoom>, IMapLayout<TRoom>> generator, int timeout = 0, bool showDebugInfo = false)
		{
			IMapLayout<TRoom> layout = null;
			var task = Task.Run(() => layout = generator.GetLayouts(mapDescription, 1)[0]);

			if (timeout > 0)
			{
				var taskCompleted = task.Wait(timeout);

				if (!taskCompleted)
				{
					throw new DungeonGeneratorException("Timeout was reached when generating the layout");
				}
			}

			if (showDebugInfo)
			{
				PrintGeneratorStats((IBenchmarkableLayoutGenerator<MapDescription<TRoom>, IMapLayout<TRoom>>) generator);
			}

			return layout;
		}

		protected void PrintGeneratorStats(IBenchmarkableLayoutGenerator<MapDescription<TRoom>, IMapLayout<TRoom>> generator)
		{
			Debug.Log($"Layout generated in {generator.TimeFirst / 1000f:F} seconds");
			Debug.Log($"{generator.IterationsCount} iterations needed, {(generator.IterationsCount / (generator.TimeFirst / 1000d)):0} iterations per second");
		}

		protected Layout<TRoom> TransformLayout(IMapLayout<TRoom> layout, TwoWayDictionary<IRoomDescription, GameObject> roomDescriptionsToRoomTemplates)
		{
			var roomTransformations = new RoomTransformations();

			// Prepare an object to hold instantiated room templates
			var parentGameObject = new GameObject("Room template instances");
			parentGameObject.transform.parent = Payload.GameObject.transform;

			// Initialize rooms
			var layoutData = new Dictionary<TRoom, RoomInfo<TRoom>>();
			foreach (var layoutRoom in layout.Rooms)
			{
				var roomTemplate = roomDescriptionsToRoomTemplates[layoutRoom.RoomDescription];

				// Instatiate room template
				var room = Object.Instantiate(roomTemplate);
				room.SetActive(false);
				room.transform.SetParent(parentGameObject.transform);

				// Transform room template if needed
				var transformation = layoutRoom.Transformation;
				roomTransformations.Transform(room, transformation);

				// Compute correct room position
				// We cannot directly use layoutRoom.Position because the dungeon moves
				// all room shapes in a way that they are in the first plane quadrant
				// and touch the xy axes. So we have to subtract the original lowest
				// x and y coordinates.
				var smallestX = layoutRoom.RoomDescription.Shape.GetPoints().Min(x => x.X);
				var smallestY = layoutRoom.RoomDescription.Shape.GetPoints().Min(x => x.Y);
				var correctPosition = layoutRoom.Position.ToUnityIntVector3() - new Vector3Int(smallestX, smallestY, 0);
				room.transform.position = correctPosition;

				var roomInfo = new RoomInfo<TRoom>(roomTemplate, room, correctPosition, TransformDoorInfo(layoutRoom.Doors),
					layoutRoom.IsCorridor, layoutRoom);

				layoutData.Add(layoutRoom.Node, roomInfo);
			}

			return new Layout<TRoom>(layoutData);
		}

		protected List<DoorInfo<TRoom>> TransformDoorInfo(IEnumerable<IDoorInfo<TRoom>> oldDoorInfos)
		{
			return oldDoorInfos.Select(TransformDoorInfo).ToList();
		}

		protected DoorInfo<TRoom> TransformDoorInfo(IDoorInfo<TRoom> oldDoorInfo)
		{
			var doorLine = oldDoorInfo.DoorLine;

			switch (doorLine.GetDirection())
			{
				case OrthogonalLine.Direction.Right:
					return new DoorInfo<TRoom>(new Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up, oldDoorInfo.Node);

				case OrthogonalLine.Direction.Left:
					return new DoorInfo<TRoom>(new Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down, oldDoorInfo.Node);

				case OrthogonalLine.Direction.Top:
					return new DoorInfo<TRoom>(new Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left, oldDoorInfo.Node);

				case OrthogonalLine.Direction.Bottom:
					return new DoorInfo<TRoom>(new Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right, oldDoorInfo.Node);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}