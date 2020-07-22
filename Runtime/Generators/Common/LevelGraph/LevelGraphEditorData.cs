using System;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph
{
    /// <summary>
    /// Additional editor data that are stored with level graphs.
    /// </summary>
    [Serializable]
    public class LevelGraphEditorData
    {
        public Vector2 PanOffset = Vector2.zero;

        public float Zoom = 1;
    }
}