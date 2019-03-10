namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.Platformers
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using DungeonGenerators;
	using GeneratorPipeline.Platformers;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutGenerator;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using Payloads.Interfaces;
	using Utils;
	using Debug = UnityEngine.Debug;

	/// <summary>
	/// Actual implementation of the task that generates platfomers.
	/// </summary>
	/// <typeparam name="TPayload"></typeparam>
	public class PlatformerGeneratorTask<TPayload> : GraphBasedGeneratorBaseTask<TPayload, PlatformerGeneratorConfig, int>
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
	{
		private readonly DungeonGeneratorUtils dungeonGeneratorUtils = new DungeonGeneratorUtils();

		public override void Process()
		{
			if (Config.Timeout <= 0)
			{
				throw new ArgumentException("Timeout must be a positive number.");
			}

			if (Payload.MapDescription.IsWithCorridors)
			{
				throw new ArgumentException("Platformer levels cannot have corridors.");
			}

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (Config.ShowDebugInfo)
			{
				Debug.Log("--- Generator started ---");
			}

			// Setup map description
			var mapDescription = Payload.MapDescription;

			// Generate layout
			var layout = GenerateLayout(mapDescription, GetGenerator(mapDescription), Config.Timeout, Config.ShowDebugInfo);

			// Setup room templates
			Payload.Layout = TransformLayout(layout, Payload.RoomDescriptionsToRoomTemplates);

			// Apply tempaltes
			if (Config.ApplyTemplate)
			{
				ApplyTemplates();
			}

			// Center grid
			if (Config.CenterGrid)
			{
				Payload.Tilemaps[0].CompressBounds();
				Payload.Tilemaps[0].transform.parent.position = -Payload.Tilemaps[0].cellBounds.center;
			}

			if (Config.ShowDebugInfo)
			{
				Debug.Log($"--- Completed. {stopwatch.ElapsedMilliseconds / 1000f:F} s ---");
			}
		}

		protected IBenchmarkableLayoutGenerator<MapDescription<int>, IMapLayout<int>> GetGenerator(MapDescription<int> mapDescription)
		{
			var generator = PlatformerGeneratorFactory.GetPlatformerGenerator<int>();
			generator.InjectRandomGenerator(Payload.Random);

			return generator;
		}

		/// <summary>
		/// Copies tiles from individual room templates to the tilemaps that hold generated dungeons.
		/// </summary>
		protected void ApplyTemplates()
		{
			var nonCorridors = Payload.Layout.GetAllRoomInfo().Where(x => !x.IsCorridor).ToList();
			var corridors = Payload.Layout.GetAllRoomInfo().Where(x => x.IsCorridor).ToList();

			dungeonGeneratorUtils.ApplyTemplates(nonCorridors, Payload.Tilemaps);
			dungeonGeneratorUtils.ApplyTemplates(corridors, Payload.Tilemaps);
		}
	}
}