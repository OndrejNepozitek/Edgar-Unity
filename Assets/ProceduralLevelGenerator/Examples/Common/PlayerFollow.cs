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
            player = GameObject.FindWithTag("Player");
        }

        public void LateUpdate()
        {
            if (player != null)
            {
                transform.position = player.transform.position + offset;
            }
        }
    }
}