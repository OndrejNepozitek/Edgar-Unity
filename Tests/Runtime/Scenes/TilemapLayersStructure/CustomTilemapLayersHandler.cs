﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Edgar.Tests.Runtime.Scenes.TilemapLayersStructure
{
    //[CreateAssetMenu(menuName = "Edgar/Tests/Tilemap Layers Structure/CustomTilemapLayersHandler")]
    public class CustomTilemapLayersHandler : TilemapLayersHandlerBaseGrid2D
    {
        /// <summary>
        ///  Initializes individual tilemap layers.
        /// </summary>
        /// <param name="gameObject"></param>
        public override void InitializeTilemaps(GameObject gameObject)
        {
            gameObject.AddComponent<Grid>();

            var floorTilemapObject = CreateTilemapGameObject("Floor Custom", gameObject, 0);

            var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, 1);
            AddCompositeCollider(wallsTilemapObject);

            var collideableTilemapObject = CreateTilemapGameObject("Collideable", gameObject, 2);
            AddCompositeCollider(collideableTilemapObject);

            var other1TilemapObject = CreateTilemapGameObject("Other 1", gameObject, 3);

            var other2TilemapObject = CreateTilemapGameObject("Other 2", gameObject, 4);

            var other3TilemapObject = CreateTilemapGameObject("Other 3", gameObject, 5);
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
            #if UNITY_2023_2_OR_NEWER
                tilemapCollider2D.compositeOperation = Collider2D.CompositeOperation.Merge;
            #else
                tilemapCollider2D.usedByComposite = true;
            #endif

            gameObject.AddComponent<CompositeCollider2D>();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}