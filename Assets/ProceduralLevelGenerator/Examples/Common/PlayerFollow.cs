using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    /// <summary>
    /// A simple script that should be attached to Camera GameObject and follows the player by moving the camera.
    /// </summary>
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