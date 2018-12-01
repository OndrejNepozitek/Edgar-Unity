namespace Assets.Scripts.Utils
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public static class TilemapExtensions
	{
		public static IEnumerable<Tuple<Vector3Int, Tile>> GetAllTiles(this Tilemap tilemap, bool includeEmpty = false)
		{
			var bounds = tilemap.cellBounds;
			var boundsPosition = bounds.position;
			var allTiles = tilemap.GetTilesBlock(bounds);
			
			for (var x = 0; x < bounds.size.x; x++)
			{
				for (var y = 0; y < bounds.size.y; y++)
				{
					var tile = (Tile) allTiles[x + y * bounds.size.x];
					var position = new Vector3Int(x, y, 0);

					if (tile != null || includeEmpty)
					{
						yield return Tuple.Create(boundsPosition + position, tile);
					}
				}
			}
		}
	}
}