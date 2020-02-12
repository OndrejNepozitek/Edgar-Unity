using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils
{
    public static class GeneratorUtils
    {
        public static GeneratedLevel TransformLayout(IMapLayout<Room> layout, LevelDescription levelDescription, GameObject rootGameObject)
        {
            // var layoutCenter = GetLayoutCenter(layout);
            var prefabToRoomTemplateMapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var corridorToConnectionMapping = levelDescription.GetCorridorToConnectionMapping();

            var roomTransformations = new RoomTransformations();

            // Prepare an object to hold instantiated room templates
            var roomTemplateInstancesRoot = new GameObject("Room template instances");
            roomTemplateInstancesRoot.transform.parent = rootGameObject.transform;

            // Initialize rooms
            var layoutData = new Dictionary<Room, RoomInstance>();
            var layoutRooms = layout.Rooms.ToDictionary(x => x.Node, x => x);
            foreach (var layoutRoom in layoutRooms.Values)
            {
                var roomTemplatePrefab = prefabToRoomTemplateMapping.GetByValue(layoutRoom.RoomTemplate);

                // Instantiate room template
                var roomTemplateInstance = Object.Instantiate(roomTemplatePrefab);
                roomTemplateInstance.transform.SetParent(roomTemplateInstancesRoot.transform);

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
                // roomTemplateInstance.transform.position -= layoutCenter;

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Node] : null;
                var roomInfo = new RoomInstance(layoutRoom.Node, connection, roomTemplatePrefab, roomTemplateInstance, correctPosition, layoutRoom,
                    layoutRoom.IsCorridor);

                layoutData.Add(layoutRoom.Node, roomInfo);
            }

            foreach (var roomInstance in layoutData.Values)
            {
                roomInstance.Doors = TransformDoorInfo(layoutRooms[roomInstance.Room].Doors, layoutData);
            }

            return new GeneratedLevel(layoutData, layout, rootGameObject);
        }

        private static List<DoorInstance> TransformDoorInfo(IEnumerable<IDoorInfo<Room>> doorInfos, Dictionary<Room, RoomInstance> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.Node])).ToList();
        }

        private static DoorInstance TransformDoorInfo(IDoorInfo<Room> doorInfo, RoomInstance connectedRoom)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLine.Direction.Right:
                    return new DoorInstance(new Scripts.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoom);

                case OrthogonalLine.Direction.Left:
                    return new DoorInstance(new Scripts.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoom);

                case OrthogonalLine.Direction.Top:
                    return new DoorInstance(new Scripts.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoom);

                case OrthogonalLine.Direction.Bottom:
                    return new DoorInstance(new Scripts.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
                        connectedRoom);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


    }
}