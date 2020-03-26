using System;
using ProceduralLevelGenerator.Unity.Generators.Common.Utils;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class DungeonGeneratorConfig
    {
        public GameObject RootGameObject;

        /// <summary>
        /// Number of milliseconds before the current attempt to generate
        /// a layout is aborted.
        /// </summary>
        public int Timeout = 10000;

        /// <summary>
        /// Whether to override repeat mode configuration of individual room templates.
        /// </summary>
        public RepeatModeOverride RepeatModeOverride;
    }
}