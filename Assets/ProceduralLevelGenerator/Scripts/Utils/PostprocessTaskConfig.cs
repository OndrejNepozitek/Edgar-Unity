namespace Assets.ProceduralLevelGenerator.Scripts.Utils
{
	using GeneratorPipeline.Payloads.Interfaces;
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline/Postprocess")]
	public class PostprocessTaskConfig : PipelineConfig
	{
		public bool CenterGrid = true;
	}

	public class PostprocessTask<TPayload> : ConfigurablePipelineTask<TPayload, PostprocessTaskConfig>
		where TPayload : class, IGeneratorPayload
	{
		public override void Process()
		{
			if (Config.CenterGrid)
			{
				Payload.Tilemaps[0].CompressBounds();
				Payload.Tilemaps[0].transform.parent.position = -Payload.Tilemaps[0].cellBounds.center;
			}
		}
	}
}