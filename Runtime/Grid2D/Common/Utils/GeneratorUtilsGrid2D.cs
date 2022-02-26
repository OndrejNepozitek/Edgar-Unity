﻿using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector2Int = UnityEngine.Vector2Int;

namespace Edgar.Unity
{
    internal static class GeneratorUtilsGrid2D
    {
        public static DungeonGeneratorLevelGrid2D TransformLayout(LayoutGrid2D<RoomBase> layout, LevelDescriptionGrid2D levelDescription, GameObject rootGameObject)
        {
            // var layoutCenter = GetLayoutCenter(layout);
            var prefabToRoomTemplateMapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var corridorToConnectionMapping = levelDescription.GetCorridorToConnectionMapping();

            // Prepare an object to hold instantiated room templates
            var roomTemplateInstancesRoot = new GameObject(GeneratorConstantsGrid2D.RoomsRootName);
            roomTemplateInstancesRoot.transform.parent = rootGameObject.transform;

            // Initialize rooms
            var layoutData = new Dictionary<RoomBase, RoomInstanceGrid2D>();
            var layoutRooms = layout.Rooms.ToDictionary(x => x.Room, x => x);
            foreach (var layoutRoom in layoutRooms.Values)
            {
                var roomTemplatePrefab = prefabToRoomTemplateMapping.GetByValue(layoutRoom.RoomTemplate);

                // Instantiate the room template
                var roomTemplateInstance = InstantiateRoomTemplate(roomTemplatePrefab);

                roomTemplateInstance.transform.SetParent(roomTemplateInstancesRoot.transform);
                roomTemplateInstance.name = $"{layoutRoom.Room.GetDisplayName()} - {roomTemplatePrefab.name}";

                // Compute correct room position
                var position = layoutRoom.Position.ToUnityIntVector3();
                roomTemplateInstance.transform.position = position;

                // Correct the position based on the grid
                // This is important when there is some cell spacing or when the level is isometric
                var tilemapsHolder = roomTemplateInstance.transform.Find(GeneratorConstantsGrid2D.TilemapsRootName).gameObject;
                if (tilemapsHolder != null)
                {
                    var grid = tilemapsHolder.GetComponent<Grid>();
                    roomTemplateInstance.transform.position = grid.CellToLocal(position);
                }

                // Compute outline polygon
                var polygon = new Polygon2D(layoutRoom.Outline + layoutRoom.Position);

                var connection = layoutRoom.IsCorridor ? corridorToConnectionMapping[layoutRoom.Room] : null;
                var roomInstance = new RoomInstanceGrid2D(layoutRoom.Room, layoutRoom.IsCorridor, connection, roomTemplatePrefab, roomTemplateInstance, position, polygon);

                // Add room info to the GameObject
                var roomInfo = roomTemplateInstance.GetComponent<RoomInfoGrid2D>();

                if (roomInfo != null)
                {
                    PostProcessUtilsGrid2D.Destroy(roomInfo);
                }

                roomInfo = roomTemplateInstance.AddComponent<RoomInfoGrid2D>();
                roomInfo.RoomInstance = roomInstance;

                layoutData.Add(layoutRoom.Room, roomInstance);
            }

            foreach (var roomInstance in layoutData.Values)
            {
                roomInstance.SetDoors(TransformDoorInfo(layoutRooms[roomInstance.Room].Doors, layoutData));
            }

            // Add level info
            var levelInfo = rootGameObject.GetComponent<LevelInfoGrid2D>();

            if (levelInfo != null)
            {
                PostProcessUtilsGrid2D.Destroy(levelInfo);
            }

            levelInfo = rootGameObject.AddComponent<LevelInfoGrid2D>();
            levelInfo.RoomInstances = layoutData.Values.ToList();

            return new DungeonGeneratorLevelGrid2D(layoutData, layout, rootGameObject, levelDescription);
        }

        private static List<DoorInstanceGrid2D> TransformDoorInfo(IEnumerable<LayoutDoorGrid2D<RoomBase>> doorInfos, Dictionary<RoomBase, RoomInstanceGrid2D> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.ToRoom])).ToList();
        }

        private static DoorInstanceGrid2D TransformDoorInfo(LayoutDoorGrid2D<RoomBase> doorInfo, RoomInstanceGrid2D connectedRoomInstance)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLineGrid2D.Direction.Right:
                    return new DoorInstanceGrid2D(new OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Left:
                    return new DoorInstanceGrid2D(new OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Top:
                    return new DoorInstanceGrid2D(new OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLineGrid2D.Direction.Bottom:
                    return new DoorInstanceGrid2D(new OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
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

        #region codeBlock:2d_keepPrefabsInEditor

        private static GameObject InstantiateRoomTemplate(GameObject roomTemplatePrefab)
        {
            // Set to true if you want to keep prefabs when generating levels in the editor
            const bool keepPrefabsInEditor = false;

            // Set to true if you want to unpack the root game object of the prefab
            // (keepPrefabsInEditor must be true for this constant to change anything)
            const bool unpackRootObject = false;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (!Application.isPlaying && keepPrefabsInEditor)
            {
                #if UNITY_EDITOR
                var roomTemplateInstance = (GameObject) PrefabUtility.InstantiatePrefab(roomTemplatePrefab);

                if (unpackRootObject)
                {
                    #pragma warning disable CS0162 // Unreachable code detected
                    PrefabUtility.UnpackPrefabInstance(
                        roomTemplateInstance,
                        PrefabUnpackMode.OutermostRoot,
                        InteractionMode.AutomatedAction
                    );
                    #pragma warning restore CS0162 // Unreachable code detected
                }

                return roomTemplateInstance;
                #endif
            }

            return Object.Instantiate(roomTemplatePrefab);
        }

        #endregion
    }
}