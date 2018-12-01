namespace Assets.Scripts.TileMapping
{
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class WallsCorrection
	{
		private static readonly Dictionary<IntVector2, int> VectorToConnection = new Dictionary<IntVector2, int>()
		{
			{ IntVector2Helper.Top, TileConnection.Top },
			{ IntVector2Helper.Right, TileConnection.Right },
			{ IntVector2Helper.Down, TileConnection.Bottom },
			{ IntVector2Helper.Left, TileConnection.Left },
		};

		public void CorrectWalls(GameObject gameObjectToCorrect, Tilemap wallTiles)
		{
			foreach (var tilemap in gameObjectToCorrect.GetComponentsInChildren<Tilemap>())
			{
				CorrectWalls(tilemap, wallTiles);
			}
		}

		public void CorrectWalls(Tilemap tilemapToCorrect, Tilemap wallTiles)
		{
			var wallTilesList = wallTiles
				.GetAllTiles()
				.Select(x => x.Item2)
				.ToList();

			var wallTilesToCorrect = tilemapToCorrect
				.GetAllTiles()
				.Where(x => wallTilesList.Contains(x.Item2))
				.Select(x => x.Item1)
				.ToHashSet();

			var wallMapper = new WallMapper(wallTiles);

			var rotation = Quaternion.Euler(0f, 0f, 0);
			var rotationMatrix = Matrix4x4.Rotate(rotation);

			foreach (var wallTile in wallTilesToCorrect)
			{
				var connections = TileConnection.None;

				foreach (var pair in VectorToConnection)
				{
					var vector = pair.Key;
					var connection = pair.Value;

					if (wallTilesToCorrect.Contains(wallTile + vector.ToUnityIntVector3()))
					{
						connections |= connection;
					}
				}

				var newTile = wallMapper.GetCorrespondingTile(connections);

				if (newTile != null)
				{
					tilemapToCorrect.SetTile(wallTile, newTile);
					tilemapToCorrect.SetTransformMatrix(wallTile, rotationMatrix);
				}
			}
		}
	}
}