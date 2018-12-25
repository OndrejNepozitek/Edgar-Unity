namespace Assets.Scripts.Utils
{
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Dungeon generator/Pipeline tasks/Postprocess")]
	public class PostprocessTask : PipelineTask
	{
		public bool CenterGrid = true;
	}

	[PipelineTaskFor(typeof(PostprocessTask))]
	public class PostprocessTask<T> : IConfigurablePipelineTask<T, PostprocessTask>
		where T : IGeneratorPayload
	{
		public PostprocessTask Config { get; set; }

		public void Process(T payload)
		{
			if (Config.CenterGrid)
			{
				payload.Tilemaps[0].ResizeBounds();
				payload.Tilemaps[0].transform.parent.position = -payload.Tilemaps[0].cellBounds.center;
			}
		}
	}
}