using GeneralAlgorithms.DataStructures.Polygons;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    [ExecuteInEditMode]
    public class FogOfWar : MonoBehaviour
    {
        [HideInInspector]
        public VisionGrid VisionGrid = new VisionGrid();

        public Color FogColor = Color.black;

        public GameObject GeneratedLevelRoot;

        private Material material;

        private Texture2D visionTexture;

        private Vector2Int visionTextureOffset;

        public void Awake()
        {
            material = new Material(Shader.Find("Hidden/FogOfWar"));
        }

        public void Start()
        {
            // VisionGrid.AddPolygon(new Polygon2D(GridPolygon.GetSquare(20)), new Vector2Int(-10, -10), 1f);
        }

        public void Reset()
        {
            VisionGrid = new VisionGrid();
        }

        public void Update()
        {
            if (VisionGrid.HasChanges())
            {
                Debug.Log("Has changes");

                var vision = VisionGrid.GetVisionTexture();
                visionTexture = vision.Texture;
                visionTextureOffset = vision.Offset;

                VisionGrid.ResetHasChanges();
            }
        }

        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            if (visionTexture == null)
            {
                Graphics.Blit (source, destination);
                return;
            }

            var camera = GetComponent<Camera>();
            var viewMat = camera.worldToCameraMatrix;
            var projMat = GL.GetGPUProjectionMatrix( camera.projectionMatrix, false );
            var viewProjMat = (projMat * viewMat);
            var offset = (Vector3) (Vector3Int) visionTextureOffset;

            if (GeneratedLevelRoot != null)
            {
                var tilemapsRoot = GeneratedLevelRoot.gameObject.transform.Find("Tilemaps");

                if (tilemapsRoot != null)
                {
                    offset += tilemapsRoot.localPosition;
                }
            }
            
            material.SetMatrix("_ViewProjInv", viewProjMat.inverse);
            material.SetTexture("_VisionTex", visionTexture);
            material.SetVector("_VisionTexOffset", offset);
            material.SetVector("_VisionTexSize", new Vector4(visionTexture.width, visionTexture.height));
            material.SetColor("_FogColor", FogColor);
            Graphics.Blit (source, destination, material);
        }

    }
}