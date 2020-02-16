using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Transformations;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Interfaces.Core.MapDescriptions;
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
                var position = layoutRoom.Position.ToUnityIntVector3();
                roomTemplateInstance.transform.position = position;

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Node] : null;
                var roomInfo = new RoomInstance(layoutRoom.Node, connection, roomTemplatePrefab, roomTemplateInstance, position, layoutRoom,
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

        public static RepeatMode? GetRepeatMode(RepeatModeOverride repeatModeOverride)
        {
            switch (repeatModeOverride)
            {
                case RepeatModeOverride.NoOverride:
                    return null;

                case RepeatModeOverride.AllowRepeat:
                    return RepeatMode.AllowRepeat;

                case RepeatModeOverride.NoImmediate:
                    return RepeatMode.NoImmediate;

                case RepeatModeOverride.NoRepeat:
                    return RepeatMode.NoRepeat;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}