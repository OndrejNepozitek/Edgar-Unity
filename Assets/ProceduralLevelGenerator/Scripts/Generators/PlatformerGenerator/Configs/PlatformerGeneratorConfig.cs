using System;
using Assets.ProceduralLevelGenerator.Scripts.Generators.Common.Utils;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.PlatformerGenerator.Configs
{
    [Serializable]
    public class PlatformerGeneratorConfig
    {
        public GameObject RootGameObject;

        public RepeatMode RepeatMode = RepeatMode.Allow;

        /// <summary>
        ///     Number of milliseconds before the current attempt to generate
        ///     a layout is aborted.
        /// </summary>
        public int Timeout = 10000;
    }
}