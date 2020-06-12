using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Examples.CurrentRoomDetection.Scripts
{
    public class CurrentRoomDetectionTriggerHandler : MonoBehaviour
    {
        private CurrentRoomDetectionRoomManager roomManager;

        public void Start()
        {
            roomManager = transform.parent.parent.gameObject.GetComponent<CurrentRoomDetectionRoomManager>();
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomEnter(otherCollider.gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomLeave(otherCollider.gameObject);
            }
        }
    }
}