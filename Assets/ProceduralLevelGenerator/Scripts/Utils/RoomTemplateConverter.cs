using System.Linq;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.ProceduralLevelGenerator.Scripts.Utils
{
    public class RoomTemplateConverter : MonoBehaviour
    {
        public void Convert()
        {
            var grid = gameObject.GetComponent<Grid>();
            if (grid != null)
            {
                DestroyImmediate(grid);
            }

            // Create tilemaps root
            var tilemapsRoot = new GameObject(GeneratorConstants.TilemapsRootName);
            tilemapsRoot.AddComponent<Grid>();

            foreach (var childTransform in transform.Cast<Transform>().ToList())
            {
                var tilemap = childTransform.GetComponent<Tilemap>();

                if (tilemap != null)
                {
                    childTransform.parent = tilemapsRoot.transform;
                }
            }

            // Fix positions
            tilemapsRoot.transform.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;

            tilemapsRoot.transform.parent = gameObject.transform;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(RoomTemplateConverter))]
    public class RoomTemplateConverterInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var converter = (RoomTemplateConverter) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Convert"))
            {
                converter.Convert();
                DestroyImmediate(converter);
            }
        }
    }
    #endif
}