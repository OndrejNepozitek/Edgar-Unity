using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates;
using MapGeneration.Interfaces.Core.MapLayouts;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
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
    }
}