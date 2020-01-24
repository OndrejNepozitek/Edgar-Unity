using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public GameObject Player;
        private Vector3 offset;

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