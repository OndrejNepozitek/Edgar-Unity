using System;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.DungeonGenerator.Configs
{
    [Serializable]
    public class DungeonGeneratorConfig
    {
        public GameObject RootGameObject;

        /// <summary>
        ///     Number of milliseconds before the current attempt to generate
        ///     a layout is aborted.
        /// </summary>
        public int Timeout = 10000;
    }
}