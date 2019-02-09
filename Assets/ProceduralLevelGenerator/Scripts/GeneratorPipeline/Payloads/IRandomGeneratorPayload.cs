namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads
{
	using System;

	public interface IRandomGeneratorPayload
	{
		Random Random { get; set; }
	}
}