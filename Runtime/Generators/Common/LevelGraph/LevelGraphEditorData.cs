using System;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    [Serializable]
    public class LevelGraphEditorData
    {
        public Vector2 PanOffset = Vector2.zero;

        public float Zoom = 1;
    }
}