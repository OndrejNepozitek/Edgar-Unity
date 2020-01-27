using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Data.Graphs;
using MapGeneration.Interfaces.Core.MapLayouts;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System.Collections.Generic;
	using RoomTemplates;

	public class GeneratedLevel
    {
        private readonly Dictionary<Room, RoomInstance> roomInstances;
        private readonly IMapLayout<Room> mapLayout;

        public GeneratedLevel(Dictionary<Room, RoomInstance> roomInstances, IMapLayout<Room> mapLayout)
        {
            this.roomInstances = roomInstances;
            this.mapLayout = mapLayout;
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