using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common
{
    public class GeneratedLevel
    {
        public GameObject RootGameObject { get; }

        private readonly IMapLayout<Room> mapLayout;
        private readonly Dictionary<Room, RoomInstance> roomInstances;

        public GeneratedLevel(Dictionary<Room, RoomInstance> roomInstances, IMapLayout<Room> mapLayout, GameObject rootGameObject)
        {
            this.roomInstances = roomInstances;
            this.mapLayout = mapLayout;
            RootGameObject = rootGameObject;
        }

        public List<RoomInstance> GetAllRoomInstances()
        {
            return roomInstances.Values.ToList();
        }

        public RoomInstance GetRoomInstance(Room room)
        {
            return roomInstances[room];
        }

        public IMapLayout<Room> GetInternalLayoutRepresentation()
        {
            return mapLayout;
        }

        public List<Tilemap> GetTilemaps()
        {
            return RoomTemplateUtils.GetTilemaps(RootGameObject);
        }
    }
}