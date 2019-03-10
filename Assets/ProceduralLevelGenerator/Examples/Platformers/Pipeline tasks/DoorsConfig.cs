namespace Assets.ProceduralLevelGenerator.Examples.Platformer.Pipeline_tasks
{
	using Scripts.GeneratorPipeline.Payloads.Interfaces;
	using Scripts.Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Examples/Platformer/Doors task", fileName = "DoorsTask")]
	public class DoorsConfig : PipelineConfig
	{

	}

	public class DoorsTask<TPayload> : ConfigurablePipelineTask<TPayload, DoorsConfig> 
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, INamedTilemapsPayload, IRandomGeneratorPayload
	{
		public override void Process()
		{
			// Iterate through all rooms
			foreach (var roomInfo in Payload.Layout.GetAllRoomInfo())
			{
				// Iterate through all used door positons
				foreach (var doorInfo in roomInfo.Doors)
				{
					foreach (var point in doorInfo.DoorLine.GetPoints())
					{
						Payload.WallsTilemap.SetTile(point, null);
					}
				}
			}
		}
	}
}
