using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR

#endif

namespace ProceduralLevelGenerator.Unity.Utils
{
    public class RoomTemplateMigrationV1ToV2 : MonoBehaviour
    {
        public void Convert()
        {
            var grid = gameObject.GetComponent<Grid>();
            if (grid != null)
            {
                DestroyImmediate(grid, true);
            }

            if (gameObject.GetComponent<RoomTemplateSettings>() == null)
            {
                gameObject.AddComponent<RoomTemplateSettings>();
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
                    }
                    if (tilemap.name == "Walls")
                    {
                        tilemapRenderer.sortingOrder = 1;
                    }
                }
            }

            foreach (var tilemap in tilemaps.OrderBy(x => x.GetComponent<TilemapRenderer>().sortingOrder))
            {
                tilemap.transform.parent = tilemapsRoot.transform;
            }

            // Fix positions
            tilemapsRoot.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(RoomTemplateMigrationV1ToV2))]
    public class RoomTemplateMigrationV1ToV2Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var converter = (RoomTemplateMigrationV1ToV2) target;

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