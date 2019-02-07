namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System.Collections.Generic;
	using Payloads.PayloadGenerators;
	using Pipeline;
	using UnityEngine;
	using Utils;

	public class DungeonGeneratorPipeline : MonoBehaviour
	{
		public AbstractPayloadGenerator PayloadGenerator;

		[HideInInspector]
		[ExpandableNotFoldable]
		public List<PipelineItem> PipelineItems;

		public void Generate()
		{
			var pipelineRunner = new PipelineRunner();
			pipelineRunner.Run(PipelineItems, PayloadGenerator.InitializePayload());
		}
	}
}