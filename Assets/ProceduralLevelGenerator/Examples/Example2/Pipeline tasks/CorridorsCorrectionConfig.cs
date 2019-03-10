namespace Assets.ProceduralLevelGenerator.Examples.Example2.Pipeline_tasks
{
	using System.Collections.Generic;
	using System.Linq;
	using Scripts.GeneratorPipeline.Payloads.Interfaces;
	using Scripts.GeneratorPipeline.RoomTemplates;
	using Scripts.Pipeline;
	using Scripts.Utils;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Examples/Example 2/Corridors correction task", fileName = "CorridorsCorrectionTask")]
	public class CorridorsCorrectionConfig : PipelineConfig
	{
		public GameObject CorrectionLayout;
	}

	public class CorridorsCorrectionTask<TPayload> : ConfigurablePipelineTask<TPayload, CorridorsCorrectionConfig>
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload
	{
		private Vector3Int tilemapsBound;
		private List<Tilemap> correctionTilemaps;

		public override void Process()
		{
			correctionTilemaps = Config.CorrectionLayout.GetComponentsInChildren<Tilemap>().ToList();
			tilemapsBound = ComputeTilemapsBound();

			foreach (var roomInfo in Payload.Layout.GetAllRoomInfo())
			{
				if (roomInfo.GeneratorData.IsCorridor)
				{
					CorrectCorridor(roomInfo);
				}
			}
		}

		/// <summary>
		/// Corrects a given corridor room if needed.
		/// </summary>
		/// <param name="roomInfo"></param>
		protected void CorrectCorridor(RoomInfo<int> roomInfo)
		{
			if (!roomInfo.Doors[0].IsHorizontal)
			{
				return;
			}

			CorrectVerticalCorridor(roomInfo);
		}

		/// <summary>
		/// Corrects a vertical corridor connection.
		/// </summary>
		/// <param name="roomInfo"></param>
		protected void CorrectVerticalCorridor(RoomInfo<int> roomInfo)
		{
			var doors = roomInfo.Doors;

			if (doors[0].FacingDirection == Vector2Int.down)
			{
				CorrectTopConnection(roomInfo, doors[0].DoorLine);
				CorrectBottomConnection(roomInfo, doors[1].DoorLine);
			}
			else
			{
				CorrectBottomConnection(roomInfo, doors[0].DoorLine);
				CorrectTopConnection(roomInfo, doors[1].DoorLine);
			}
		}

		/// <summary>
		/// Corrects bottom connection of a given corridor room.
		/// </summary>
		/// <param name="roomInfo"></param>
		/// <param name="doorLine"></param>
		protected void CorrectBottomConnection(RoomInfo<int> roomInfo, OrthogonalLine doorLine)
		{	
			CopyTiles(doorLine.From + new Vector3Int(-1, -1, 0), tilemapsBound, doorLine.Length);
		}

		/// <summary>
		/// Corrects top connection of a given corridor room.
		/// </summary>
		/// <param name="roomInfo"></param>
		/// <param name="doorLine"></param>
		protected void CorrectTopConnection(RoomInfo<int> roomInfo, OrthogonalLine doorLine)
		{
			CopyTiles(doorLine.From + new Vector3Int(-1, 1, 0), tilemapsBound + Vector3Int.up * 3, doorLine.Length);
		}

		/// <summary>
		/// Copy tiles from a given point on the correction layout to a given point in the dungeon.
		/// </summary>
		/// <param name="destinationPosition"></param>
		/// <param name="correctionPosition"></param>
		/// <param name="doorLength"></param>
		protected void CopyTiles(Vector3Int destinationPosition, Vector3Int correctionPosition, int doorLength)
		{
			for (int i = 0; i < Payload.Tilemaps.Count; i++)
			{
				var sourceTilemap = correctionTilemaps[i];
				var destinationTilemap = Payload.Tilemaps[i];

				// Handle left tiles
				{
					var correctionTilePosition = correctionPosition;
					var destionationTilePosition = destinationPosition;

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}

				// Handle middle tiles
				for (int k = 0; k < doorLength; k++)
				{
					var correctionTilePosition = correctionPosition + Vector3Int.right;
					var destionationTilePosition = destinationPosition + Vector3Int.right * (k + 1);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}

				// Handle right tiles
				{
					var correctionTilePosition = correctionPosition + Vector3Int.right * 2;
					var destionationTilePosition = destinationPosition + Vector3Int.right * (doorLength + 1);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}
			}
		}

		/// <summary>
		/// Computes the x and y such that x is the smallest used x-position and
		/// y is the smallest used y-position of correction layout tilemaps.
		/// </summary>
		/// <returns></returns>
		protected Vector3Int ComputeTilemapsBound()
		{
			foreach (var tilemap in correctionTilemaps)
			{
				tilemap.CompressBounds();
			}

			var smallestX = correctionTilemaps.Min(x => x.cellBounds.position.x);
			var smallestY = correctionTilemaps.Min(x => x.cellBounds.position.y);

			return new Vector3Int(smallestX, smallestY, 0);
		}
	}
}
