using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts
{
    public class Parallax : MonoBehaviour
    {
        private float length;
        private float startPosition;
        public GameObject Camera;
        public float ParallaxEffect;

        public void Start()
        {
            startPosition = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        public void Update()
        {
            var temp = Camera.transform.position.x * (1 - ParallaxEffect);
            var distance = Camera.transform.position.x * ParallaxEffect;

            transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

            if (temp > startPosition + length)
            {
                startPosition += length;
            }

            if (temp < startPosition - length)
            {
                startPosition -= length;
            }
        }
    }
}