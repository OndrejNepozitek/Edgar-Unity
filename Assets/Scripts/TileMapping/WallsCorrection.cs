namespace Assets.Scripts.TileMapping
{
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class WallsCorrection : MonoBehaviour
	{
		public GameObject TilesToCorrectObject;

		public GameObject GoToCorrect;

		private static readonly Dictionary<IntVector2, int> VectorToConnection = new Dictionary<IntVector2, int>()
		{
			{ IntVector2Helper.Top, TileConnection.Top },
			{ IntVector2Helper.Right, TileConnection.Right },
			{ IntVector2Helper.Down, TileConnection.Bottom },
			{ IntVector2Helper.Left, TileConnection.Left },
		};

		public void Execute()
		{
			var wallTilesList = TilesToCorrectObject
				.GetComponentInChildren<Tilemap>()
				.GetAllTiles()
				.Select(x => x.Item2)
				.ToList();

			var goToCorrect = GoToCorrect; //FindObjectsOfType<GameObject>().FirstOrDefault(x => x.name == "Shop(Clone)");
			var tilemap = goToCorrect.GetComponentInChildren<Tilemap>();

			var wallTiles = tilemap
				.GetAllTiles()
				.Where(x => wallTilesList.Contains(x.Item2))
				.Select(x => x.Item1)
				.ToHashSet();

			var wallMapper = new WallMapper(TilesToCorrectObject);

			foreach (var wallTile in wallTiles)
			{
				var connections = TileConnection.None;

				foreach (var pair in VectorToConnection)
				{
					var vector = pair.Key;
					var connection = pair.Value;

					if (wallTiles.Contains(wallTile + vector.ToUnityIntVector3()))
					{
						connections |= connection;
					}
				}

				var newTile = wallMapper.GetCorrespondingTile(connections);

				if (newTile != null)
				{
					var rotation = Quaternion.Euler(0f, 0f, 0);
					var rotationMatrix = Matrix4x4.Rotate(rotation);
					tilemap.SetTile(wallTile, newTile);
					tilemap.SetTransformMatrix(wallTile, rotationMatrix);
				}
			}
		}
	}
}