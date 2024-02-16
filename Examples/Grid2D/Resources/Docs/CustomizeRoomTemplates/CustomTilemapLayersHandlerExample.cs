using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Examples.Resources
{
    #region codeBlock:2d_roomTemplateCustomization_handler

    // TilemapLayersHandlerBase inherit from ScriptableObject so we need to create an asset
    // menu item that we will use to create the scriptable object instance.
    // The menu name can be changed to anything you want.
    // [CreateAssetMenu(menuName = "Dungeon generator/Custom tilemap layers handler", fileName = "CustomTilemapLayersHandler")]
    public class CustomTilemapLayersHandlerExample : TilemapLayersHandlerBaseGrid2D
    {
        public override void InitializeTilemaps(GameObject gameObject)
        {
            // First make sure that you add the grid component
            var grid = gameObject.AddComponent<Grid>();

            // If we want a different cell size, we can configure that here
            // grid.cellSize = new Vector3(1, 2, 1);

            // And now we create individual tilemap layers
            var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject, 0);

            var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 1);
            AddCompositeCollider(wallsTilemapObject);

            CreateTilemapGameObject("Additional layer 1", gameObject, 2);
            CreateTilemapGameObject("Additional layer 2", gameObject, 3);
        }

        /// <summary>
        /// Helper to create a tilemap layer
        /// </summary>
        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
        {
            // Create a new game object that will hold our tilemap layer
            var tilemapObject = new GameObject(name);
            // Make sure to correctly set the parent
            tilemapObject.transform.SetParent(parentObject.transform);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = sortingOrder;

            return tilemapObject;
        }

        /// <summary>
        /// Helper to add a collider to a given tilemap game object.
        /// </summary>
        protected void AddCompositeCollider(GameObject tilemapGameObject, bool isTrigger = false)
        {
            var tilemapCollider2D = tilemapGameObject.AddComponent<TilemapCollider2D>();
            #if UNITY_2023_2_OR_NEWER
            tilemapCollider2D.compositeOperation = Collider2D.CompositeOperation.Merge;
            #else
            tilemapCollider2D.usedByComposite = true;
            #endif

            var compositeCollider2d = tilemapGameObject.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = isTrigger;

            tilemapGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    #endregion
}