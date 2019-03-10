namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators.GraphBasedGenerator
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutGenerator;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using Payloads.Interfaces;
	using Pipeline;
	using RoomTemplates;
	using RoomTemplates.Doors;
	using RoomTemplates.Transformations;
	using UnityEngine;
	using UnityEngine.Tilemaps;
	using Utils;
	using Debug = UnityEngine.Debug;
	using Object = UnityEngine.Object;
	using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

	/// <summary>
	/// Actual implementation of the task that generates dungeons.
	/// </summary>
	/// <typeparam name="TPayload"></typeparam>
	public class GraphBasedGeneratorTask<TPayload> : GraphBasedGeneratorBaseTask<TPayload, GraphBasedGeneratorConfig, int>
		where TPayload : class, IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload
	{
		private readonly DungeonGeneratorUtils dungeonGeneratorUtils = new DungeonGeneratorUtils();

		public override void Process()
		{
			if (Config.Timeout <= 0)
			{
				throw new ArgumentException("Timeout must be a positive number.");
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
			IBenchmarkableLayoutGenerator<MapDescription<int>, IMapLayout<int>> generator;
			if (mapDescription.IsWithCorridors)
			{
				var gen = UnityLayoutGeneratorFactory.GetChainBasedGeneratorWithCorridors<int>(mapDescription.CorridorsOffsets);
				gen.InjectRandomGenerator(Payload.Random);
				generator = gen;
			}
			else
			{
				var gen = UnityLayoutGeneratorFactory.GetDefaultChainBasedGenerator<int>();
				gen.InjectRandomGenerator(Payload.Random);
				generator = gen;
			}

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