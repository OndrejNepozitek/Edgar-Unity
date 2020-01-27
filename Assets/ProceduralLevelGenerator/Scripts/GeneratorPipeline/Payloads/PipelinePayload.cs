using Assets.ProceduralLevelGenerator.Scripts.Utils;
using MapGeneration.Interfaces.Core.MapLayouts;

namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Data.Graphs;
	using DungeonGenerators;
    using Interfaces;
    using UnityEngine;
	using UnityEngine.Tilemaps;
	using Random = System.Random;

	/// <summary>
	/// Default pipeline payload.
	/// </summary>
	/// <typeparam name="TRoom"></typeparam>
	public class PipelinePayload<TRoom> : IGeneratorPayload, IGraphBasedGeneratorPayload, IRandomGeneratorPayload, IBenchmarkInfoPayload
	{
		public GameObject ParentGameObject { get; set; }

		public List<Tilemap> Tilemaps { get; set; }

        public LevelDescription LevelDescription { get; set; }

        public GeneratedLevel GeneratedLevel { get; set; }

        public Random Random { get; set; }

        public IMapLayout<Room> GeneratedLayout { get; set; }

        public int Iterations { get; set; }

        public double TimeTotal { get; set; }
    }
}