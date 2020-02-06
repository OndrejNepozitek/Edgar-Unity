using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    public class PlayerFollow : MonoBehaviour
    {
        private Vector3 offset;
        private GameObject player;

        public void Start()
        {
            offset = transform.position;
        }

        public void LateUpdate()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            if (player != null)
            {
                transform.position = player.transform.position + offset;
            }
        }
    }
}