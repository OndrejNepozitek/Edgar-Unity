namespace Assets.Scripts.TileMapping
{
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class WallMapper
	{
		private readonly Tilemap wallsTilemap;
		private readonly Dictionary<int, IntVector2> mapping;

		public WallMapper(Tilemap wallsTilemap)
		{
			this.wallsTilemap = wallsTilemap;
			mapping = GetTilesMapping();
		}

		public Tile GetCorrespondingTile(int connections)
		{
			IntVector2 tilePosition;

			if (mapping.TryGetValue(connections, out tilePosition))
			{
				var correctedPosition = wallsTilemap.cellBounds.position + tilePosition.ToUnityIntVector3();
				var tile = wallsTilemap.GetTile<Tile>(correctedPosition);

				return tile;
			}

			return null;
		}

		private static Dictionary<int, IntVector2> GetTilesMapping()
		{
			var mapping = new Dictionary<int, IntVector2>();

			// Straight lines
			mapping.Add(TileConnection.Top | TileConnection.Bottom, new IntVector2(0, 1));
			mapping.Add(TileConnection.Left | TileConnection.Right, new IntVector2(2, 3));

			// Simple corners
			mapping.Add(TileConnection.Top | TileConnection.Right, new IntVector2(1, 1));
			mapping.Add(TileConnection.Right | TileConnection.Bottom, new IntVector2(1, 2));
			mapping.Add(TileConnection.Bottom | TileConnection.Left, new IntVector2(2, 2));
			mapping.Add(TileConnection.Left | TileConnection.Top, new IntVector2(2, 1));

			// Single connections
			mapping.Add(TileConnection.Left, new IntVector2(3, 3));
			mapping.Add(TileConnection.Right, new IntVector2(1, 3));
			mapping.Add(TileConnection.Top, new IntVector2(0, 0));
			mapping.Add(TileConnection.Bottom, new IntVector2(0, 2));

			// Three connections
			mapping.Add(TileConnection.Left | TileConnection.Right | TileConnection.Bottom, new IntVector2(2, 0));
			mapping.Add(TileConnection.Left | TileConnection.Right | TileConnection.Top, new IntVector2(3, 1));
			mapping.Add(TileConnection.Left | TileConnection.Bottom | TileConnection.Top, new IntVector2(0, 1));
			mapping.Add(TileConnection.Right | TileConnection.Bottom | TileConnection.Top, new IntVector2(0, 1));

			return mapping;
		}
	}
}