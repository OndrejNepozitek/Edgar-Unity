using UnityEngine;

namespace Edgar.Unity.Examples.CurrentRoomDetection
{
    #region codeBlock:2d_currentRoomDetection_triggerHandler

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

    #endregion
}