using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph
{
    /// <summary>
    ///     Represents a connection between two rooms.
    /// </summary>
    public class Connection : ScriptableObject, IConnection<Room>
    {
        [HideInInspector]
        public Room From;

        [HideInInspector]
        public Room To;

        Room IConnection<Room>.From => From;

        Room IConnection<Room>.To => To;
    }
}