using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.CurrentRoomDetection.Scripts
{
    public class CurrentRoomDetectionRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Room instance of the corresponding room.
        /// </summary>
        public RoomInstance RoomInstance;

        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            CurrentRoomDetectionGameManager.Instance.OnRoomEnter(RoomInstance);
        }

        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            CurrentRoomDetectionGameManager.Instance.OnRoomLeave(RoomInstance);
        }
    }
}