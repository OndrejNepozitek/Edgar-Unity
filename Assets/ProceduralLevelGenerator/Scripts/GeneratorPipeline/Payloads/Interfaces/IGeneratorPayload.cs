using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads.Interfaces
{
    /// <summary>
    ///     Basic generator pipeline payload.
    /// </summary>
    public interface IGeneratorPayload
    {
        /// <summary>
        ///     GameObject that holds dungeon tilemaps and possibly other game objects.
        /// </summary>
        GameObject ParentGameObject { get; set; }

        /// <summary>
        ///     Tilemaps of the generated dungeon.
        /// </summary>
        List<Tilemap> Tilemaps { get; set; }
    }
}