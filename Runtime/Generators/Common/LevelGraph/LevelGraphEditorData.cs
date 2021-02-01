using System;
using UnityEngine;

namespace Edgar.Unity
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