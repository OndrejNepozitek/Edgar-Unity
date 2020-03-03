using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.Common
{
    /// <summary>
    /// A simple script that should be attached to Camera GameObject and follows the player by moving the camera.
    /// </summary>
    public class PlayerFollow : MonoBehaviour
    {
        private GameObject player;
        public void LateUpdate()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            if (player != null)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        }
    }
}