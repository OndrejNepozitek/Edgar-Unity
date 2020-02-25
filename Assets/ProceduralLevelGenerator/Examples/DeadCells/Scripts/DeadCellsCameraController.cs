using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.DeadCells.Scripts
{
    public class DeadCellsCameraController : MonoBehaviour
    {
        public GameObject Follow;
        public float ZoomOutSize = 70;

        private Vector3 offset;
        private Camera camera;

        private bool isZoomedOut;
        private float previousOrthographicSize;
        private Vector3 previousPosition;
        private GameObject background;

        public void Start()
        {
            offset = transform.position;
            camera = GetComponent<Camera>();
            background = transform.Find("Background")?.gameObject;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                previousOrthographicSize = camera.orthographicSize;
                camera.orthographicSize = ZoomOutSize;

                previousPosition = transform.position;
                transform.position = new Vector3(0, 0, previousPosition.z);

                if (background != null)
                {
                    background.SetActive(false);
                }
                
                isZoomedOut = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                camera.orthographicSize = previousOrthographicSize;
                transform.position = previousPosition;

                if (background != null)
                {
                    background.SetActive(true);
                }

                isZoomedOut = false;
            }
        }

        public void LateUpdate()
        {
            if (Follow != null && !isZoomedOut)
            {
                // TODO: why like this?
                transform.position = new Vector3(Follow.transform.position.x, Follow.transform.position.y, transform.position.z);
            }
        }
    }
}