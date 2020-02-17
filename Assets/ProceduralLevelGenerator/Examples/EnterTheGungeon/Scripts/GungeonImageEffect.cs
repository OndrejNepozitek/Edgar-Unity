using Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts.Levels;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Pro;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using GeneralAlgorithms.Algorithms.Polygons;
using GeneralAlgorithms.DataStructures.Polygons;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    [ExecuteInEditMode]
    public class GungeonImageEffect : MonoBehaviour
    {
        public float intensity;
        public float alpha;
        public Material material;
        public GameObject LevelRoot;
        private VisionGrid visionGrid;
        private VisionTexture texture;

        // Creates a private material used to the effect
        void Awake ()
        {

        }

        public void Update()
        {
            if (visionGrid == null)
            {
                var gridPolygon = new GridPolygonBuilder()
                    .AddPoint(0, 0)
                    .AddPoint(0, 15)
                    .AddPoint(7, 15)
                    .AddPoint(7, 5)
                    .AddPoint(20, 5)
                    .AddPoint(20, 0)
                    .Build();
                var polygon = new Polygon2D(gridPolygon);

                var visionGrid = new VisionGrid();

                // visionGrid.AddPolygon(new Polygon2D(GridPolygon.GetSquare(20)), new Vector2Int(-10, -10));

                if (LevelRoot != null)
                {
                    var levelInfo = LevelRoot.GetComponent<LevelInfo>();

                    if (levelInfo != null && levelInfo.Level != null)
                    {
                        var random = new System.Random();

                        foreach (var roomInstance in levelInfo.Level.GetAllRoomInstances())
                        {
                            if (random.NextDouble() < 0.5 && (roomInstance.Room as GungeonRoom).Type != GungeonRoomType.Entrance) // TODO:
                            {
                                var roomTemplate = roomInstance.RoomTemplateInstance;
                                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplate);
                                var outlineTilemaps = RoomTemplateUtils.GetTilemapsForOutline(tilemaps);
                                var usedTiles = RoomTemplatesLoaderTest.GetUsedTiles(outlineTilemaps);
                                var newPolygon = RoomTemplatesLoader.GetPolygonFromTiles(usedTiles);

                                visionGrid.AddPolygon(roomInstance.OutlinePolygon, (Vector2Int) roomInstance.Position, 0);
                            }
                        }

                        this.visionGrid = visionGrid;
                        texture = visionGrid.GetVisionTexture();
                    }
                }
            }
        }
	
        // Postprocess the image
        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            if (this.texture == null)
            {
                Graphics.Blit (source, destination);
                return;
            }

            if (intensity == 0)
            {
                Graphics.Blit (source, destination);
                return;
            }

            var camera = GetComponent<Camera>();
            var viewMat = camera.worldToCameraMatrix;
            var projMat = GL.GetGPUProjectionMatrix( camera.projectionMatrix, false );
            var viewProjMat = (projMat * viewMat);
            var offset = (Vector3Int) texture.Offset + LevelRoot.gameObject.transform.Find("Tilemaps").localPosition;
            
            material.SetMatrix("_ViewProjInv", viewProjMat.inverse);
            material.SetFloat("_bwBlend", intensity);
            material.SetFloat("_alpha", alpha);
            material.SetTexture("_OtherTex", texture.Texture);
            material.SetVector("_Offset", offset);
            material.SetVector("_TexSize", new Vector4(texture.Texture.width, texture.Texture.height));
            Graphics.Blit (source, destination, material);
        }
    }
}