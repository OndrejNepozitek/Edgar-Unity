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

		public TileBase VerticalBottom;

		public TileBase VerticalTop;

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

			foreach (var pair in Payload.LayoutData)
			{
				var roomInfo = pair.Value;

				if (roomInfo.IsCorridor)
				{
					if (Payload.Random.NextDouble() < Config.Probability)
					{
						foreach (var doorInfo in roomInfo.Doors)
						{

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

		protected void AddHorizontalDoors(DoorInfo<Room> doorInfo)
		{
			Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(0), Config.HorizontalLeft);

			if (doorInfo.DoorLine.Length > 1)
			{
				Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(1), Config.HorizontalRight);
			}
		}

		protected void AddVerticalDoors(DoorInfo<Room> doorInfo)
		{
			Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(0), Config.VerticalBottom);

			if (doorInfo.DoorLine.Length > 1)
			{
				Payload.CollideableTilemap.SetTile(doorInfo.DoorLine.GetNthPoint(1), Config.VerticalTop);
			}
		}
	}
}
