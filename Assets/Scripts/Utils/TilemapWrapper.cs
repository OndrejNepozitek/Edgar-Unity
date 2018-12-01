namespace Assets.Scripts.TileMapping
{
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class TilemapWrapper
	{
		public Tilemap Tilemap { get; }

		public IntVector2 Bounds => new IntVector2(Tilemap.cellBounds.size.x, Tilemap.cellBounds.size.y);

		public TilemapWrapper(Tilemap tilemap)
		{
			Tilemap = tilemap;
		}

		public Tile GetTileAt(IntVector2 position)
		{
			return Tilemap.GetTile<Tile>(GetCorrectPosition(position));
		}

		public void SetTileAt(IntVector2 position, Tile tile)
		{
			Tilemap.SetTile(GetCorrectPosition(position), tile);
		}

		private Vector3Int GetCorrectPosition(IntVector2 position)
		{
			var bounds = Tilemap.cellBounds;
			return new Vector3Int(position.X + bounds.xMin, position.Y + bounds.yMin, 0);
		}

		public IEnumerable<IntVector2> GetAllPositions()
		{
			for (var i = 0; i < Bounds.X; i++)
			{
				for (var j = 0; j < Bounds.Y; j++)
				{
					yield return new IntVector2(i, j);
				}
			}
		}
	}
}