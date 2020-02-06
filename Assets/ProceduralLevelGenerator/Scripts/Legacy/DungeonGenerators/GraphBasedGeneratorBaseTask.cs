using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Payloads.Interfaces;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations;
using Assets.ProceduralLevelGenerator.Scripts.Pipeline;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Core.LayoutGenerators.DungeonGenerator;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using Object = UnityEngine.Object;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace Assets.ProceduralLevelGenerator.Scripts.Legacy.DungeonGenerators
{
    public abstract class GraphBasedGeneratorBaseTask<TPayload, TConfig> : ConfigurablePipelineTask<TPayload, TConfig>
        where TConfig : PipelineConfig
        where TPayload : class, IGeneratorPayload
    {
        public abstract override void Process();

        // TODO: should probably use some interface instead of DungeonGenerator<TRoom>
        protected IMapLayout<Room> GenerateLayout(IMapDescription<Room> mapDescription, DungeonGenerator<Room> generator, int timeout = 0,
            bool showDebugInfo = false)
        {
            IMapLayout<Room> layout = null;
            var task = Task.Run(() => layout = generator.GenerateLayout());

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
                PrintGeneratorStats(generator);
            }

            return layout;
        }

        protected void PrintGeneratorStats(DungeonGenerator<Room> generator)
        {
            Debug.Log($"Layout generated in {generator.TimeTotal / 1000f:F} seconds");
            Debug.Log($"{generator.IterationsCount} iterations needed, {generator.IterationsCount / (generator.TimeTotal / 1000d):0} iterations per second");
        }

        protected GeneratedLevel TransformLayout(IMapLayout<Room> layout, LevelDescription levelDescription)
        {
            var prefabToRoomTemplateMapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var corridorToConnectionMapping = levelDescription.GetCorridorToConnectionMapping();

            var roomTransformations = new RoomTransformations();

            // Prepare an object to hold instantiated room templates
            var parentGameObject = new GameObject("Room template instances");
            parentGameObject.transform.parent = Payload.RootGameObject.transform;

            // Initialize rooms
            var layoutData = new Dictionary<Room, RoomInstance>();
            var layoutRooms = layout.Rooms.ToDictionary(x => x.Node, x => x);
            foreach (var layoutRoom in layoutRooms.Values)
            {
                var roomTemplatePrefab = prefabToRoomTemplateMapping.GetByValue(layoutRoom.RoomTemplate);

                // Instantiate room template
                var roomTemplateInstance = Object.Instantiate(roomTemplatePrefab);
                roomTemplateInstance.SetActive(false);
                roomTemplateInstance.transform.SetParent(parentGameObject.transform);

                // Transform room template if needed
                var transformation = layoutRoom.Transformation;
                roomTransformations.Transform(roomTemplateInstance, transformation);

                // Compute correct room position
                // We cannot directly use layoutRoom.Position because the dungeon moves
                // all room shapes in a way that they are in the first plane quadrant
                // and touch the xy axes. So we have to subtract the original lowest
                // x and y coordinates.
                var smallestX = layoutRoom.RoomTemplate.Shape.GetPoints().Min(x => x.X);
                var smallestY = layoutRoom.RoomTemplate.Shape.GetPoints().Min(x => x.Y);
                var correctPosition = layoutRoom.Position.ToUnityIntVector3() - new Vector3Int(smallestX, smallestY, 0);
                roomTemplateInstance.transform.position = correctPosition;

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Node] : null;
                var roomInfo = new RoomInstance(layoutRoom.Node, connection, roomTemplatePrefab, roomTemplateInstance, correctPosition, layoutRoom,
                    layoutRoom.IsCorridor);

                layoutData.Add(layoutRoom.Node, roomInfo);
            }

            foreach (var roomInstance in layoutData.Values)
            {
                roomInstance.Doors = TransformDoorInfo(layoutRooms[roomInstance.Room].Doors, layoutData);
            }

            return new GeneratedLevel(layoutData, layout, Payload.RootGameObject);
        }

        protected List<DoorInstance> TransformDoorInfo(IEnumerable<IDoorInfo<Room>> doorInfos, Dictionary<Room, RoomInstance> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.Node])).ToList();
        }

        protected DoorInstance TransformDoorInfo(IDoorInfo<Room> doorInfo, RoomInstance connectedRoom)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLine.Direction.Right:
                    return new DoorInstance(new Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoom);

                case OrthogonalLine.Direction.Left:
                    return new DoorInstance(new Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoom);

                case OrthogonalLine.Direction.Top:
                    return new DoorInstance(new Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoom);

                case OrthogonalLine.Direction.Bottom:
                    return new DoorInstance(new Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
                        connectedRoom);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}