namespace Assets.ProceduralLevelGenerator.Examples.Example1.Pipeline_tasks
{
	using Scripts.Data.Graphs;
	using Scripts.GeneratorPipeline.Payloads.Interfaces;
	using Scripts.GeneratorPipeline.RoomTemplates.Doors;
	using Scripts.Pipeline;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	[CreateAssetMenu(menuName = "Dungeon generator/Examples/Example 1/Doors task", fileName = "DoorsTask")]
	public class DoorsConfig : PipelineConfig
	{
		public bool AddDoors;

		public TileBase HorizontalLeft;

		public TileBase HorizontalRight;

		public TileBase VerticalTop;

		public TileBase VerticalBottom;

		[Range(0f, 1f)]
		public float Probability = 1;
	}

	public class DoorsTask<TPayload> : ConfigurablePipelineTask<TPayload, DoorsConfig> 
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, INamedTilemapsPayload, IRandomGeneratorPayload
	{
		public override void Process()
		{
			if (!Config.AddDoors)
			{
				return;
			}

			// Iterate through all rooms
			foreach (var roomInfo in Payload.Layout.GetAllRoomInfo())
			{
				// Check if corridor room
				if (roomInfo.IsCorridor)
				{
					if (Payload.Random.NextDouble() < Config.Probability)
					{
						// Iterate through all used door positons
						foreach (var doorInfo in roomInfo.Doors)
						{
							// We cannot handle doors with length > 2
							if (doorInfo.DoorLine.Length > 2)
							{
								continue;
							}

							if (doorInfo.IsHorizontal)
							{
								AddHorizontalDoors(doorInfo);
							}
							else
							{
								AddVerticalDoors(doorInfo);
							}
						}
					}
				}
			}
		}

		protected void AddHorizontalDoors(DoorInfo<int> doorInfo)
		{
			Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(0), Config.HorizontalLeft);

			if (doorInfo.DoorLine.Length > 1)
			{
				Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(1), Config.HorizontalRight);
			}
		}

		protected void AddVerticalDoors(DoorInfo<int> doorInfo)
		{
			Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(0), Config.VerticalBottom);

			if (doorInfo.DoorLine.Length > 1)
			{
				Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(1), Config.VerticalTop);
			}
		}
	}
}
