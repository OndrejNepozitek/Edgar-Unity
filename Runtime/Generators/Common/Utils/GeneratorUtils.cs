using System;
using System.Collections.Generic;
using System.Linq;
using MapGeneration.Core.MapDescriptions.Interfaces;
using MapGeneration.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    public static class GeneratorUtils
    {
        public static GeneratedLevel TransformLayout(MapLayout<RoomBase> layout, LevelDescription levelDescription, GameObject rootGameObject)
        {
            // var layoutCenter = GetLayoutCenter(layout);
            var prefabToRoomTemplateMapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var corridorToConnectionMapping = levelDescription.GetCorridorToConnectionMapping();
            
            // Prepare an object to hold instantiated room templates
            var roomTemplateInstancesRoot = new GameObject(GeneratorConstants.RoomsRootName);
            roomTemplateInstancesRoot.transform.parent = rootGameObject.transform;

            // Initialize rooms
            var layoutData = new Dictionary<RoomBase, RoomInstance>();
            var layoutRooms = layout.Rooms.ToDictionary(x => x.Node, x => x);
            foreach (var layoutRoom in layoutRooms.Values)
            {
                var roomTemplatePrefab = prefabToRoomTemplateMapping.GetByValue(layoutRoom.RoomTemplate);

                // Instantiate room template
                var roomTemplateInstance = Object.Instantiate(roomTemplatePrefab);
                roomTemplateInstance.transform.SetParent(roomTemplateInstancesRoot.transform);
                roomTemplateInstance.name = $"{layoutRoom.Node.GetDisplayName()} - {roomTemplatePrefab.name}";
                
                // Compute correct room position
                var position = layoutRoom.Position.ToUnityIntVector3();
                roomTemplateInstance.transform.position = position;

                // Compute outline polygon
                var polygon = new Polygon2D(layoutRoom.Shape + layoutRoom.Position);

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Node] : null;
                var roomInstance = new RoomInstance(layoutRoom.Node, layoutRoom.IsCorridor, connection, roomTemplatePrefab, roomTemplateInstance, position, polygon);

                // Add room info to the GameObject
                var roomInfo = roomTemplateInstance.GetComponent<RoomInfo>();

                if (roomInfo != null)
                {
                    PostProcessUtils.Destroy(roomInfo);
                }

                roomInfo = roomTemplateInstance.AddComponent<RoomInfo>();
                roomInfo.RoomInstance = roomInstance;

                layoutData.Add(layoutRoom.Node, roomInstance);
            }

            foreach (var roomInstance in layoutData.Values)
            {
                roomInstance.SetDoors(TransformDoorInfo(layoutRooms[roomInstance.Room].Doors, layoutData));
            }

            // Add level info
            var levelInfo = rootGameObject.GetComponent<LevelInfo>();

            if (levelInfo != null)
            {
                PostProcessUtils.Destroy(levelInfo);
            }

            levelInfo = rootGameObject.AddComponent<LevelInfo>();
            levelInfo.RoomInstances = layoutData.Values.ToList();

            return new GeneratedLevel(layoutData, layout, rootGameObject);
        }

        private static List<DoorInstance> TransformDoorInfo(IEnumerable<DoorInfo<RoomBase>> doorInfos, Dictionary<RoomBase, RoomInstance> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.Node])).ToList();
        }

        private static DoorInstance TransformDoorInfo(DoorInfo<RoomBase> doorInfo, RoomInstance connectedRoomInstance)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLine.Direction.Right:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Left:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Top:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Bottom:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
                        connectedRoomInstance.Room, connectedRoomInstance);

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