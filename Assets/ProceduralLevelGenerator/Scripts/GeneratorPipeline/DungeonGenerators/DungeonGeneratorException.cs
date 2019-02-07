namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System;

	public class DungeonGeneratorException : Exception
	{
		public DungeonGeneratorException()
		{

		}

		public DungeonGeneratorException(string message) : base(message)
		{

		}
	}
}