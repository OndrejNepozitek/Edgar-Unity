namespace Assets.ProceduralLevelGenerator.Eamples.Example1.Pipeline_tasks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using Scripts.Data.Graphs;
	using Scripts.GeneratorPipeline.Payloads;
	using Scripts.GeneratorPipeline.Payloads.Interfaces;
	using Scripts.GeneratorPipeline.RoomTemplates;
	using Scripts.Pipeline;
	using Scripts.Utils;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Examples/Example 2/Corridors correction task", fileName = "CorridorsCorrectionTask")]
	public class CorridorsCorrectionConfig : PipelineConfig
	{
		public GameObject CorrectionTilemap;
	}

	public class CorridorsCorrectionTask<TPayload> : ConfigurablePipelineTask<TPayload, CorridorsCorrectionConfig>
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload
	{
		private Vector3Int tilemapsBound;
		private List<Tilemap> correctionTilemaps;

		public override void Process()
		{
			correctionTilemaps = Config.CorrectionTilemap.GetComponentsInChildren<Tilemap>().ToList();
			tilemapsBound = ComputeTilemapsBound();

			foreach (var pair in Payload.LayoutData)
			{
				var room = pair.Key;
				var roomInfo = pair.Value;

				if (roomInfo.GeneratorData.IsCorridor)
				{
					CorrectCorridor(room, roomInfo);
				}
			}
		}

		protected void CorrectCorridor(Room room, RoomInfo<Room> roomInfo)
		{
			var generatorData = roomInfo.GeneratorData;
			var doors = generatorData.Doors;

			if (doors[0].DoorLine.Length > 0)
			{
				if (doors[0].DoorLine.GetDirection() != OrthogonalLine.Direction.Left &&
				    doors[0].DoorLine.GetDirection() != OrthogonalLine.Direction.Right)
				{
					CorrectHorizontalCorridor(roomInfo);
					return;
				}
			}
			else
			{
				if (doors[0].DoorLine.From.X < doors[1].DoorLine.From.X)
				{
					CorrectHorizontalCorridor(roomInfo);
					return;
				}
			}

			if (doors[0].DoorLine.From.Y > doors[1].DoorLine.From.Y)
			{
				CorrectBottomConnection(roomInfo, doors[1].DoorLine);
				CorrectTopConnection(roomInfo, doors[0].DoorLine);
			}
			else
			{
				CorrectBottomConnection(roomInfo, doors[0].DoorLine);
				CorrectTopConnection(roomInfo, doors[1].DoorLine);
			}

			//foreach (var tilemap in Payload.Tilemaps)
			//{
			//	tilemap.SetTile(doors[0].DoorLine.From.ToUnityIntVector3(), null);
			//	tilemap.SetTile(doors[1].DoorLine.From.ToUnityIntVector3(), null);

			//	tilemap.SetTile(doors[0].DoorLine.To.ToUnityIntVector3(), null);
			//	tilemap.SetTile(doors[1].DoorLine.To.ToUnityIntVector3(), null);
			//}
		}

		protected void CorrectHorizontalCorridor(RoomInfo<Room> roomInfo)
		{
			var doors = roomInfo.GeneratorData.Doors;
			CorrectHorizontalCorridor(doors[0].DoorLine);
			CorrectHorizontalCorridor(doors[1].DoorLine);
		}

		protected void CorrectHorizontalCorridor(OrthogonalLine doorLine)
		{
			var from = doorLine.From.ToUnityIntVector3();
			var to = doorLine.To.ToUnityIntVector3();

			if (from.y > to.y)
			{
				var tmp = from;
				from = to;
				to = tmp;
			}

			var tilemap = Payload.Tilemaps[2];

			for (int i = 0; i < doorLine.Length; i++)
			{
				var tilePosition = from + Vector3Int.up * (i + 1);
				tilemap.SetTile(tilePosition, null);
			}
		}

		protected void CorrectVerticalCorridor(RoomInfo<Room> roomInfo)
		{
			var doors = roomInfo.GeneratorData.Doors;

			if (doors[0].DoorLine.From.Y > doors[1].DoorLine.From.Y)
			{
				CorrectBottomConnection(roomInfo, doors[1].DoorLine);
				CorrectTopConnection(roomInfo, doors[0].DoorLine);
			}
			else
			{
				CorrectBottomConnection(roomInfo, doors[0].DoorLine);
				CorrectTopConnection(roomInfo, doors[1].DoorLine);
			}
		}

		protected void CorrectBottomConnection(RoomInfo<Room> roomInfo, OrthogonalLine doorLine)
		{
			var from = doorLine.From.ToUnityIntVector3();
			var to = doorLine.To.ToUnityIntVector3();
			
			if (from.x > to.x)
			{
				var tmp = from;
				from = to;
				to = tmp;
			}

			for (int i = 0; i < Payload.Tilemaps.Count; i++)
			{
				var sourceTilemap = correctionTilemaps[i];
				var destinationTilemap = Payload.Tilemaps[i];

				// Handle left tiles
				for (int j = 0; j < 2; j++)
				{
					var correctionTilePosition = tilemapsBound + Vector3Int.up * j;
					var destionationTilePosition = from + Vector3Int.left + Vector3Int.up * (j - 1);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}

				// Handle middle tiles
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < doorLine.Length + 1; k++)
					{
						var correctionTilePosition = tilemapsBound + Vector3Int.right + Vector3Int.up * j;
						var destionationTilePosition = from + Vector3Int.right * k + Vector3Int.up * (j - 1);

						destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
					}
				}

				// Handle right tiles
				for (int j = 0; j < 2; j++)
				{
					var correctionTilePosition = tilemapsBound + Vector3Int.right * 2 + Vector3Int.up * j;
					var destionationTilePosition = to + Vector3Int.right + Vector3Int.up * (j - 1);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}
			}
		}

		protected void CorrectTopConnection(RoomInfo<Room> roomInfo, OrthogonalLine doorLine)
		{
			var from = doorLine.From.ToUnityIntVector3();
			var to = doorLine.To.ToUnityIntVector3();

			if (from.x > to.x)
			{
				var tmp = from;
				from = to;
				to = tmp;
			}

			for (int i = 0; i < Payload.Tilemaps.Count; i++)
			{
				var sourceTilemap = correctionTilemaps[i];
				var destinationTilemap = Payload.Tilemaps[i];

				// Handle left tiles
				for (int j = 2; j < 4; j++)
				{
					var correctionTilePosition = tilemapsBound + Vector3Int.up * j;
					var destionationTilePosition = from + Vector3Int.left + Vector3Int.up * (j - 2);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}

				// Handle middle tiles
				for (int j = 2; j < 4; j++)
				{
					for (int k = 0; k < doorLine.Length + 1; k++)
					{
						var correctionTilePosition = tilemapsBound + Vector3Int.right + Vector3Int.up * j;
						var destionationTilePosition = from + Vector3Int.right * k + Vector3Int.up * (j - 2);

						destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
					}
				}

				// Handle right tiles
				for (int j = 2; j < 4; j++)
				{
					var correctionTilePosition = tilemapsBound + Vector3Int.right * 2 + Vector3Int.up * j;
					var destionationTilePosition = to + Vector3Int.right + Vector3Int.up * (j - 2);

					destinationTilemap.SetTile(destionationTilePosition, sourceTilemap.GetTile(correctionTilePosition));
				}
			}
		}

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
