using System;
using System.Linq;

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
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
    {
        private Tilemap collideableTilemap;

		public override void Process()
		{
			if (!Config.AddDoors)
			{
				return;
			}

            collideableTilemap = Payload.Tilemaps.SingleOrDefault(x => x.name == "Collideable");

            if (collideableTilemap == null)
            {
				throw new InvalidOperationException("Tilemap named \"Collideable\" not found");
            }

			// Iterate through all rooms
			foreach (var roomInstance in Payload.GeneratedLevel.GetAllRoomInstances())
			{
				// Check if corridor room
				if (roomInstance.IsCorridor)
				{
					if (Payload.Random.NextDouble() < Config.Probability)
					{
						// Iterate through all used door positons
						foreach (var doorInfo in roomInstance.Doors)
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

		protected void AddHorizontalDoors(DoorInstance doorInstance)
		{
            collideableTilemap.SetTile(doorInstance.DoorLine.GetNthPoint(0), Config.HorizontalLeft);

			if (doorInstance.DoorLine.Length > 1)
			{
                collideableTilemap.SetTile(doorInstance.DoorLine.GetNthPoint(1), Config.HorizontalRight);
			}
		}

		protected void AddVerticalDoors(DoorInstance doorInstance)
		{
            collideableTilemap.SetTile(doorInstance.DoorLine.GetNthPoint(0), Config.VerticalBottom);

			if (doorInstance.DoorLine.Length > 1)
			{
                collideableTilemap.SetTile(doorInstance.DoorLine.GetNthPoint(1), Config.VerticalTop);
			}
		}
	}
}
