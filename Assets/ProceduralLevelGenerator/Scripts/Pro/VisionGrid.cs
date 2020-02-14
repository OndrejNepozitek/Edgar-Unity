using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Pro
{
    [Serializable]
    public class VisionGrid
    {
        [SerializeField]
        private Dictionary<Vector2Int, float> values = new Dictionary<Vector2Int, float>();

        [SerializeField]
        private bool hasChanges;

        public bool HasChanges()
        {
            return hasChanges;
        }

        public void ResetHasChanges()
        {
            hasChanges = false;
        }

        public void AddPolygon(Polygon2D polygon, Vector2Int position, float value)
        {
            foreach (var point in polygon.GetAllPoints())
            {
                values[point + position] = value;
            }

            hasChanges = true;
        }

        public VisionTexture GetVisionTexture(float defaultValue = 0)
        {
            if (values.Count == 0)
            {
                return null;
            }

            var minX = values.Keys.Min(x => x.x);
            var minY = values.Keys.Min(x => x.y);
            var maxX = values.Keys.Max(x => x.x);
            var maxY = values.Keys.Max(x => x.y);

            var width = maxX - minX + 1;
            var height = maxY - minY + 1;

            var colors = new Color[width * height];

            for (int i = 0; i < width * height; i++)
            {
                colors[i] = Color.clear;
            }

            foreach (var pair in values)
            {
                var pos = pair.Key;
                var value = pair.Value;

                var x = pos.x - minX;
                var y = pos.y - minY;

                colors[y * width + x] = new Color(value, value, value);
            }

            var texture = new Texture2D(width, height);
            texture.SetPixels(colors);

            return new VisionTexture()
            {
                Offset = new Vector2Int(minX, minY),
                Texture = texture,
            };
        }
    }
}