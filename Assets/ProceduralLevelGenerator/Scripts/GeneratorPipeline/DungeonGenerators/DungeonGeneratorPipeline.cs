namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System.Collections.Generic;
	using Payloads.PayloadInitializers;
	using Pipeline;
	using UnityEngine;
	using Utils;

	public class DungeonGeneratorPipeline : MonoBehaviour
	{
		[ExpandableNotFoldable]
		public AbstractPayloadInitializer PayloadInitializer;

		[HideInInspector]
		[ExpandableNotFoldable]
		public List<PipelineItem> PipelineItems;

		public void Generate()
		{
			var pipelineRunner = new PipelineRunner();
			pipelineRunner.Run(PipelineItems, PayloadInitializer.InitializePayload());
		}
	}
}