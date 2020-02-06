using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ProceduralLevelGenerator.Examples.EnterTheGungeon.Scripts
{
    /// <summary>
    ///     Generic visual tile for creating different tilesets like terrain, pipeline, random or animated tiles.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New Enhanced Rule Tile", menuName = "Tiles/Enhanced Rule Tile")]
    public class EnhancedRuleTile : RuleTile<EnhancedRuleTile.Neighbor>
    {
        private Tilemap[] tilemaps;

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.NotNull: return tile != null && tile != this;
            }

            return base.RuleMatch(neighbor, tile);
        }

        public class Neighbor : TilingRule.Neighbor
        {
            public const int NotNull = 3;
        }
    }
}