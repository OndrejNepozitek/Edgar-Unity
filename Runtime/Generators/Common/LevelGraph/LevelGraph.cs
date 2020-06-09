using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    /// <summary>
    ///     Represents a level graph.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelGraph", menuName = "Dungeon generator/Level graph")]
    public class LevelGraph : ScriptableObject
    {
        /// <summary>
        ///     List of connections in the graph.
        /// </summary>
        [HideInInspector]
        public List<ConnectionBase> Connections = new List<ConnectionBase>();

        /// <summary>
        ///     Set of room templates that are used for corridor rooms.
        /// </summary>
        public List<GameObject> CorridorIndividualRoomTemplates = new List<GameObject>();

        /// <summary>
        ///     Sets of corridor room templates.
        /// </summary>
        public List<RoomTemplatesSet> CorridorRoomTemplateSets = new List<RoomTemplatesSet>();

        /// <summary>
        ///     Set of room templates that is used for room thah do not have any room templates assigned.
        /// </summary>
        public List<GameObject> DefaultIndividualRoomTemplates = new List<GameObject>();

        /// <summary>
        ///     Sets of default room templates.
        /// </summary>
        public List<RoomTemplatesSet> DefaultRoomTemplateSets = new List<RoomTemplatesSet>();

        /// <summary>
        ///     List of rooms in the graph.
        /// </summary>
        [HideInInspector]
        public List<RoomBase> Rooms = new List<RoomBase>();

        /// <summary>
        ///     Editor data like zoom, etc.
        /// </summary>
        public LevelGraphEditorData EditorData = new LevelGraphEditorData();

        public string RoomType = typeof(Room).FullName;

        public string ConnectionType = typeof(Connection).FullName;
    }
}