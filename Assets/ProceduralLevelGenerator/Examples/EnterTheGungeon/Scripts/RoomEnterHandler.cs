using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class RoomEnterHandler : MonoBehaviour
    {
        private RoomManager parentRoomManager;

        public void Start()
        {
            parentRoomManager = transform.parent.gameObject.GetComponent<RoomManager>();
            parentRoomManager.FloorCollider = GetComponent<CompositeCollider2D>();
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            // TODO: handle better
            if (otherCollider.gameObject.name == "Player")
            {
                parentRoomManager.OnRoomEnter(otherCollider);
            }
        }

        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.name == "Player")
            {
                parentRoomManager.OnRoomLeave(otherCollider);
            }
        }
    }
}