using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector2Int = UnityEngine.Vector2Int;

namespace Edgar.Unity
{
    public static class GeneratorUtils
    {
        public static GeneratedLevel TransformLayout(LayoutGrid2D<RoomBase> layout, LevelDescription levelDescription, GameObject rootGameObject)
        {
            // var layoutCenter = GetLayoutCenter(layout);
            var prefabToRoomTemplateMapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var corridorToConnectionMapping = levelDescription.GetCorridorToConnectionMapping();
            
            // Prepare an object to hold instantiated room templates
            var roomTemplateInstancesRoot = new GameObject(GeneratorConstants.RoomsRootName);
            roomTemplateInstancesRoot.transform.parent = rootGameObject.transform;

            // Initialize rooms
            var layoutData = new Dictionary<RoomBase, RoomInstance>();
            var layoutRooms = layout.Rooms.ToDictionary(x => x.Room, x => x);
            foreach (var layoutRoom in layoutRooms.Values)
            {
                var roomTemplatePrefab = prefabToRoomTemplateMapping.GetByValue(layoutRoom.RoomTemplate);

                // Instantiate room template
                var roomTemplateInstance = Object.Instantiate(roomTemplatePrefab);
                roomTemplateInstance.transform.SetParent(roomTemplateInstancesRoot.transform);
                roomTemplateInstance.name = $"{layoutRoom.Room.GetDisplayName()} - {roomTemplatePrefab.name}";
                
                // Compute correct room position
                var position = layoutRoom.Position.ToUnityIntVector3();
                roomTemplateInstance.transform.position = position;

                // Correct the position based on the grid
                // This is important when there is some cell spacing or when the level is isometric
                var tilemapsHolder = roomTemplateInstance.transform.Find(GeneratorConstants.TilemapsRootName).gameObject;
                if (tilemapsHolder != null)
                {
                    var grid = tilemapsHolder.GetComponent<Grid>();
                    roomTemplateInstance.transform.position = grid.CellToLocal(position);
                }

                // Compute outline polygon
                var polygon = new Polygon2D(layoutRoom.Outline + layoutRoom.Position);

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Room] : null;
                var roomInstance = new RoomInstance(layoutRoom.Room, layoutRoom.IsCorridor, connection, roomTemplatePrefab, roomTemplateInstance, position, polygon);

                // Add room info to the GameObject
                var roomInfo = roomTemplateInstance.GetComponent<RoomInfo>();

                if (roomInfo != null)
                {
                    PostProcessUtils.Destroy(roomInfo);
                }

                roomInfo = roomTemplateInstance.AddComponent<RoomInfo>();
                roomInfo.RoomInstance = roomInstance;

                layoutData.Add(layoutRoom.Room, roomInstance);
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

        private static List<DoorInstance> TransformDoorInfo(IEnumerable<LayoutDoorGrid2D<RoomBase>> doorInfos, Dictionary<RoomBase, RoomInstance> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.ToRoom])).ToList();
        }

        private static DoorInstance TransformDoorInfo(LayoutDoorGrid2D<RoomBase> doorInfo, RoomInstance connectedRoomInstance)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLineGrid2D.Direction.Right:
                    return new DoorInstance(new OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Left:
                    return new DoorInstance(new OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Top:
                    return new DoorInstance(new OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Bottom:
                    return new DoorInstance(new OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
                        connectedRoomInstance.Room, connectedRoomInstance);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static RoomTemplateRepeatMode? GetRepeatMode(RepeatModeOverride repeatModeOverride)
        {
            switch (repeatModeOverride)
            {
                case RepeatModeOverride.NoOverride:
                    return null;

                case RepeatModeOverride.AllowRepeat:
                    return RoomTemplateRepeatMode.AllowRepeat;

                case RepeatModeOverride.NoImmediate:
                    return RoomTemplateRepeatMode.NoImmediate;

                case RepeatModeOverride.NoRepeat:
                    return RoomTemplateRepeatMode.NoRepeat;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}