using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    public abstract class ConnectionBase : ScriptableObject, IConnection<RoomBase>
    {
        [HideInInspector]
        public RoomBase From;

        [HideInInspector]
        public RoomBase To;

        RoomBase IConnection<RoomBase>.From => From;

        RoomBase IConnection<RoomBase>.To => To;
    }
}