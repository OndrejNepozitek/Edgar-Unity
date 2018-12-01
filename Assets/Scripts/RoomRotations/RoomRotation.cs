namespace Assets.Scripts.RoomRotations
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class RoomRotation
	{
		public void RotateRoom(GameObject go, int degrees, List<Sprite> notRotatedSprites = null)
		{
			foreach (var tilemap in go.GetComponentsInChildren<Tilemap>())
			{
				RotateTilemap(tilemap, degrees, notRotatedSprites);
			}
		}

		private void RotateTilemap(Tilemap tilemap, int degrees, List<Sprite> notRotatedSprites = null)
		{
			if (notRotatedSprites == null)
			{
				notRotatedSprites = new List<Sprite>();
			}

			var newTiles = new List<Tuple<Vector3Int, Tile>>();

			var rotation = Quaternion.Euler(0f, 0f, degrees);
			var rotationMatrix = Matrix4x4.Rotate(rotation);

			foreach (var tilePair in tilemap.GetAllTiles())
			{
				var position = tilePair.Item1;
				var tile = tilePair.Item2;

				if (!notRotatedSprites.Contains(tile.sprite))
				{
					tile.transform = rotationMatrix;
				}

				var newPosition = position.RotateAroundCenter(-degrees);

				newTiles.Add(Tuple.Create(newPosition, tile));
			}

			tilemap.ClearAllTiles();

			foreach (var newTile in newTiles)
			{
				tilemap.SetTile(newTile.Item1, newTile.Item2);
			}
		}
	}
}