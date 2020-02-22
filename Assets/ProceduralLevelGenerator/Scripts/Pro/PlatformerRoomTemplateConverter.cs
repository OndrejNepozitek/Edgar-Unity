using System.Collections.Generic;
using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR

#endif

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    public class PlatformerRoomTemplateConverter : MonoBehaviour
    {
        public void Convert()
        {
            var grid = gameObject.GetComponent<Grid>();
            if (grid != null)
            {
                DestroyImmediate(grid, true);
            }

            if (gameObject.GetComponent<RoomTemplate>() == null)
            {
                gameObject.AddComponent<RoomTemplate>();
            }

            if (transform.Find(GeneratorConstants.TilemapsRootName) != null)
            {
                var oldRoot = transform.Find(GeneratorConstants.TilemapsRootName).gameObject;

                foreach (var childTransform in oldRoot.transform.Cast<Transform>().ToList())
                {
                    var tilemap = childTransform.GetComponent<Tilemap>();

                    if (tilemap != null)
                    {
                        childTransform.parent = transform;
                    }
                }

                DestroyImmediate(oldRoot);
            }

            // Create tilemaps root
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.AddComponent<Grid>();
            tilemapsRoot.transform.parent = gameObject.transform;
            var tilemaps = new List<Tilemap>();

            foreach (var childTransform in transform.Cast<Transform>().ToList())
            {
                var tilemap = childTransform.GetComponent<Tilemap>();
                
                if (tilemap != null)
                {
                    tilemaps.Add(tilemap);
                    var tilemapRenderer = childTransform.GetComponent<TilemapRenderer>();

                    if (tilemap.name == "Floor")
                    {
                        tilemapRenderer.sortingOrder = 0;
                        tilemap.name = "Background";
                    }
                    if (tilemap.name == "Walls")
                    {
                        tilemapRenderer.sortingOrder = 1;
                        tilemap.gameObject.layer = LayerMask.NameToLayer("Collisions");

                    }
                }
            }

            if (tilemaps.All(x => x.name != "Platforms"))
            {
                var platforms = tilemaps.Single(x => x.name == "Collideable");
                platforms.gameObject.layer = LayerMask.NameToLayer("Collisions");
                platforms.name = "Platforms";
                AddPlatformEffector(platforms.gameObject);

                var collideable = CreateTilemapGameObject("Collideable", platforms.transform.parent.gameObject, 3);
                platforms.gameObject.layer = LayerMask.NameToLayer("Collisions");
                tilemaps.Add(collideable.GetComponent<Tilemap>());
                AddCompositeCollider(collideable);

                var other1 = tilemaps.Single(x => x.name == "Other 1");
                other1.GetComponent<TilemapRenderer>().sortingOrder = 4;
                
                var other2 = tilemaps.Single(x => x.name == "Other 2");
                other2.GetComponent<TilemapRenderer>().sortingOrder = 5;

                var other3 = tilemaps.Single(x => x.name == "Other 3");
                tilemaps.Remove(other3);
                PostProcessUtils.Destroy(other3.gameObject);
            }

            foreach (var tilemap in tilemaps.OrderBy(x => x.GetComponent<TilemapRenderer>().sortingOrder))
            {
                tilemap.transform.parent = tilemapsRoot.transform;
                tilemap.transform.localPosition = Vector3.zero;
            }

            // Fix positions
            tilemapsRoot.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;
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
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(PlatformerRoomTemplateConverter))]
    public class RoomTemplateConverterInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var converter = (PlatformerRoomTemplateConverter) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Convert"))
            {
                converter.Convert();
                EditorUtility.SetDirty(converter.gameObject);
                DestroyImmediate(converter);
            }
        }
    }
    #endif
}