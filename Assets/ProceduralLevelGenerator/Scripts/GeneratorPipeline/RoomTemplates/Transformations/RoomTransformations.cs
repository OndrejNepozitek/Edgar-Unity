namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.RoomTemplates.Transformations
{
	using System;
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	/// <summary>
	/// Currently not used.
	/// </summary>
	public class RoomTransformations
	{
		public void Transform(GameObject roomTemplate, Transformation transformation)
		{
			foreach (var tilemap in roomTemplate.GetComponentsInChildren<Tilemap>())
			{
				Transform(tilemap, transformation);
			}
		}

		public void Transform(Tilemap tilemap, Transformation transformation)
		{
			var newTiles = new List<Tuple<Vector3Int, TileBase>>();

			foreach (var position in tilemap.cellBounds.allPositionsWithin)
			{
				var tileBase = tilemap.GetTile(position);

				if (tileBase == null)
					continue;

				var newPosition = position.Transform(transformation);

				newTiles.Add(Tuple.Create(newPosition, tileBase));

				var tile = tileBase as Tile;

				if (tile != null)
				{
					if (transformation == Transformation.MirrorY || transformation == Transformation.Identity)
					{
						var transformationMatrix = Matrix4x4.Scale(new Vector3(1, 1));

						if (transformation == Transformation.MirrorY)
						{
							transformationMatrix = Matrix4x4.Scale(new Vector3(-1, 1));
						}

						tile.transform = transformationMatrix;
					}
				}
			}

			tilemap.ClearAllTiles();

			foreach (var newTile in newTiles)
			{
				var position = newTile.Item1;
				var tileBase = newTile.Item2;

				tilemap.SetTile(position, tileBase);
			}
		}
	}
}