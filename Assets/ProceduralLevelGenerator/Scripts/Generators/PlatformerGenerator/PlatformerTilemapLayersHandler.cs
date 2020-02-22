using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.TilemapLayers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator
{
    public class PlatformerTilemapLayersHandler : ITilemapLayersHandler
    {
        /// <summary>
        ///     Initializes individual tilemap layers.
        /// </summary>
        /// <param name="gameObject"></param>
        public void InitializeTilemaps(GameObject gameObject)
        {
            gameObject.AddComponent<Grid>();

            var backgroundTilemapObject = CreateTilemapGameObject("Background", gameObject, 0);

            var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 1);
            AddCompositeCollider(wallsTilemapObject);

            var platformsTilemapObject = CreateTilemapGameObject("Platforms", gameObject, 2);
            platformsTilemapObject.layer = LayerMask.NameToLayer("Collisions");
            AddCompositeCollider(platformsTilemapObject);
            AddPlatformEffector(platformsTilemapObject);

            var collideableTilemapObject = CreateTilemapGameObject("Collideable", gameObject, 3);
            collideableTilemapObject.layer = LayerMask.NameToLayer("Collisions");
            AddCompositeCollider(collideableTilemapObject);

            var other1TilemapObject = CreateTilemapGameObject("Other 1", gameObject, 4);

            var other2TilemapObject = CreateTilemapGameObject("Other 2", gameObject, 5);
        }

        protected void AddPlatformEffector(GameObject gameObject)
        {
            var compositeCollider2D = gameObject.GetComponent<CompositeCollider2D>();
            compositeCollider2D.usedByEffector = true;

            gameObject.AddComponent<PlatformEffector2D>();
        }

        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, int sortingOrder)
        {
            var tilemapObject = new GameObject(name);
            tilemapObject.transform.SetParent(parentObject.transform);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = sortingOrder;

            return tilemapObject;
        }

        protected void AddCompositeCollider(GameObject gameObject)
        {
            var tilemapCollider2D = gameObject.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;

            gameObject.AddComponent<CompositeCollider2D>();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.layer = LayerMask.NameToLayer("Collisions");

        }
    }
}