using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 offset;
        public GameObject Player;

        public void Start()
        {
            offset = transform.position;
        }

        public void LateUpdate()
        {
            transform.position = Player.transform.position + offset;
        }
    }
}