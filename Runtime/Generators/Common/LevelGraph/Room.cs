using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    /// <summary>
    ///     Represents a room in a level graph.
    /// </summary>
    public class Room : ScriptableObject
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

        /// <summary>
        ///     Position of the room in the graph editor.
        /// </summary>
        /// <remarks>
        ///     This value is not used by the dungeon generator.
        /// </remarks>
        [HideInInspector]
        public Vector2 Position;

        public override string ToString()
        {
            return Name;
        }
    }
}