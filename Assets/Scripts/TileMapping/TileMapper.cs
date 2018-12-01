namespace Assets.Scripts.TileMapping
{
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using RoomRotations;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;

	public class TileMapper : MonoBehaviour
	{
		public GameObject Room;

		public GameObject DummyRoom;

		public GameObject OutputObject;

		public GameObject NotRotatedTiles;

		public int RotateDegrees;

		private GameObject lastGameObject;

		private static readonly Dictionary<IntVector2, int> VectorToConnection = new Dictionary<IntVector2, int>()
		{
			{ IntVector2Helper.Top, TileConnection.Top },
			{ IntVector2Helper.Right, TileConnection.Right },
			{ IntVector2Helper.Down, TileConnection.Bottom },
			{ IntVector2Helper.Left, TileConnection.Left },
		};

		public void Execute()
		{
			ExecuteRotate();
			return;

			var tilemap = Room.GetComponentInChildren<Tilemap>();
			var tilemapWrapper = new TilemapWrapper(tilemap);

			var outline = RoomShapesLogic.GetOutline(tilemap);
			var mapping = GetTilesMapping();

			var outputTilemap = OutputObject.GetComponentInChildren<Tilemap>();
			var outputTilemapWrapper = new TilemapWrapper(outputTilemap);
			outputTilemap.ClearAllTiles();

			foreach (var position in tilemapWrapper.GetAllPositions())
			{
				var tile = tilemapWrapper.GetTileAt(position);
				outputTilemapWrapper.SetTileAt(position, tile);
			}

			foreach (var point in outline)
			{
				var tileIndex = new Vector3Int(point.X, point.Y, 0);
				var connections = TileConnection.None;

				foreach (var pair in VectorToConnection)
				{
					var vector = pair.Key;
					var connection = pair.Value;

					if (outline.Contains(point + vector))
					{
						connections |= connection;
					}
				}

				IntVector2 mappedTileIndex;
				if (mapping.TryGetValue(connections, out mappedTileIndex))
				{
					outputTilemap.SetTile(tileIndex, GetMappedTile(mappedTileIndex));
				}
			}
		}

		public void ExecuteRotate()
		{
			//var tilemap = Room.GetComponentInChildren<Tilemap>();
			//var tilemapWrapper = new TilemapWrapper(tilemap);

			//var outline = RoomShapesLogic.GetOutline(tilemap);
			//var mapping = GetTilesMapping();

			//var outputTilemap = OutputObject.GetComponentInChildren<Tilemap>();
			//var outputTilemapWrapper = new TilemapWrapper(outputTilemap);
			//outputTilemap.ClearAllTiles();

			//var rot = Quaternion.Euler(0f, 0f, 0);
			//var rotMatrix = Matrix4x4.Rotate(rot);

			//foreach (var position in tilemapWrapper.GetAllPositions())
			//{
			//	var tile = tilemapWrapper.GetTileAt(position);

			//	if (tile == null)
			//		continue;

			//	tile.transform = rotMatrix;
			//	outputTilemapWrapper.SetTileAt(new IntVector2(position.X, position.Y), tile);
			//}

			if (lastGameObject != null)
			{
				Object.DestroyImmediate(lastGameObject);
			}

			var roomRotation = new RoomRotation();
			var go = Instantiate(Room);
			var notRotatedTiles = NotRotatedTiles.GetComponentsInChildren<Tilemap>().SelectMany(x => x.GetAllTiles())
				.Select(x => x.Item2.sprite).ToList();

			roomRotation.RotateRoom(go, RotateDegrees, notRotatedTiles);

			lastGameObject = go;
		}

		private TileBase GetMappedTile(IntVector2 position)
		{
			var mappingTilemap = DummyRoom.GetComponentInChildren<Tilemap>();
			var bounds = mappingTilemap.cellBounds;
			var correctPosition = new Vector3Int(position.X + bounds.xMin, position.Y + bounds.yMin, 0);
			var tile = mappingTilemap.GetTile(correctPosition);

			return tile;
		}

		private Dictionary<int, IntVector2> GetTilesMapping()
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

			return mapping;
		}
	}
}