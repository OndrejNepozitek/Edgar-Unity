using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class RoomEnterHandler : MonoBehaviour
    {
        private GungeonRoomManager parentGungeonRoomManager;

        public void Start()
        {
            Setup();
        }

        public void Setup()
        {
            parentGungeonRoomManager = transform.parent.parent.gameObject.GetComponent<GungeonRoomManager>();

            if (parentGungeonRoomManager != null)
            {
                parentGungeonRoomManager.FloorCollider = GetComponent<CompositeCollider2D>();
            }
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            // TODO: handle better
            if (otherCollider.gameObject.name == "Player")
            {
                parentGungeonRoomManager?.OnRoomEnter(otherCollider);
            }
        }

        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.name == "Player")
            {
                parentGungeonRoomManager?.OnRoomLeave(otherCollider);
            }
        }
    }
}