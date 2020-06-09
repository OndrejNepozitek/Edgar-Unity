using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    /// <summary>
    ///     Represents a room in a level graph.
    /// </summary>
    public class Room : RoomBase
    {
        /// <summary>
        ///     Name of the room.
        /// </summary>
        public string Name = "Room";

        /// <summary>
        ///     Room templates assigned to the room.
        /// </summary>
        public List<GameObject> IndividualRoomTemplates = new List<GameObject>();

        /// <summary>
        ///     Assigned room template sets.
        /// </summary>
        public List<RoomTemplatesSet> RoomTemplateSets = new List<RoomTemplatesSet>();

        public override List<GameObject> GetRoomTemplates()
        {
            return IndividualRoomTemplates
                .Union(RoomTemplateSets.SelectMany(x => x.RoomTemplates))
                .Distinct()
                .ToList();
        }

        public override string GetDisplayName()
        {
            return Name;
        }
    }
}