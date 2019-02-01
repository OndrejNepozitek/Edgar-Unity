namespace Assets.Scripts.DungeonGenerators
{
	using System.Collections.Generic;
	using GeneratorPipeline;
	using Pipeline;
	using UnityEngine;
	using Utils;

	public class DungeonGeneratorPipeline : MonoBehaviour
	{
		[Expandable]
		public AbstractPayloadGenerator PayloadGenerator;

		[HideInInspector]
		[Expandable]
		public List<PipelineItem> PipelineItems;

		public void Generate()
		{
			var pipelineRunner = new PipelineRunner();
			pipelineRunner.Run(PipelineItems, PayloadGenerator.InitializePayload());
		}
	}
}