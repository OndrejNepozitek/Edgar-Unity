using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces
{
    /// <summary>
    ///     Basic generator pipeline payload.
    /// </summary>
    public interface IGeneratorPayload
    {
        /// <summary>
        ///     GameObject that holds dungeon tilemaps and possibly other game objects.
        /// </summary>
        GameObject RootGameObject { get; set; }

        /// <summary>
        ///     Tilemaps of the generated dungeon.
        /// </summary>
        List<Tilemap> Tilemaps { get; set; }
    }
}