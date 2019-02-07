namespace Assets.ProceduralLevelGenerator.Scripts.Utils
{
	using System.Collections.Generic;
	using GeneralAlgorithms.Algorithms.Common;
	using GeneralAlgorithms.Algorithms.Polygons;
	using GeneralAlgorithms.DataStructures.Common;
	using GeneralAlgorithms.DataStructures.Polygons;
	using MapGeneration.Core.ChainDecompositions;
	using MapGeneration.Core.Configurations;
	using MapGeneration.Core.Configurations.EnergyData;
	using MapGeneration.Core.ConfigurationSpaces;
	using MapGeneration.Core.Constraints;
	using MapGeneration.Core.Doors;
	using MapGeneration.Core.GeneratorPlanners;
	using MapGeneration.Core.LayoutConverters;
	using MapGeneration.Core.LayoutEvolvers;
	using MapGeneration.Core.LayoutGenerators;
	using MapGeneration.Core.LayoutOperations;
	using MapGeneration.Core.Layouts;
	using MapGeneration.Core.MapDescriptions;
	using MapGeneration.Interfaces.Core.LayoutConverters;
	using MapGeneration.Interfaces.Core.MapLayouts;
	using MapGeneration.Utils;

	public static class UnityLayoutGeneratorFactory
	{
		/// <summary>
		/// Gets a basic layout generator that should not be used to generated layouts with corridors.
		/// </summary>
		/// <typeparam name="TNode"></typeparam>
		/// <returns></returns>
		public static ChainBasedGenerator<MapDescription<TNode>, Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IMapLayout<TNode>> GetDefaultChainBasedGenerator<TNode>()
		{
			return LayoutGeneratorFactory.GetDefaultChainBasedGenerator<TNode>();
		}

		/// <summary>
		/// Gets a generator that can work with corridors.
		/// </summary>
		/// <remarks>
		/// TODO: This is only a temporary solution because we must be able to inject our own corridor nodes creator and this is the easiest way right now.
		/// </remarks>
		/// <param name="offsets"></param>
		/// <param name="canTouch">Whether rooms can touch. Perfomance is decreased when set to false.</param>
		/// <param name="corridorNodesCreator"></param>
		/// <returns></returns>
		public static ChainBasedGenerator<MapDescription<TNode>, Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, IMapLayout<TNode>> GetChainBasedGeneratorWithCorridors<TNode>(List<int> offsets, bool canTouch = false, ICorridorNodesCreator<TNode> corridorNodesCreator = null)
		{
			var layoutGenerator = new ChainBasedGenerator<MapDescription<TNode>, Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, IMapLayout<TNode>>();

			var chainDecomposition = new BreadthFirstChainDecomposition<int>();
			var configurationSpacesGenerator = new ConfigurationSpacesGenerator(new PolygonOverlap(), DoorHandler.DefaultHandler, new OrthogonalLineIntersection(), new GridPolygonUtils());
			var generatorPlanner = new BasicGeneratorPlanner<Layout<Configuration<CorridorsData>, BasicEnergyData>>();

			layoutGenerator.SetChainDecompositionCreator(mapDescription => new CorridorsChainDecomposition<int>(mapDescription, chainDecomposition));
			layoutGenerator.SetConfigurationSpacesCreator(mapDescription => configurationSpacesGenerator.Generate<TNode, Configuration<CorridorsData>>(mapDescription));
			layoutGenerator.SetInitialLayoutCreator(mapDescription => new Layout<Configuration<CorridorsData>, BasicEnergyData>(mapDescription.GetGraph()));
			layoutGenerator.SetGeneratorPlannerCreator(mapDescription => generatorPlanner);
			layoutGenerator.SetLayoutConverterCreator((mapDescription, configurationSpaces) => new BasicLayoutConverter<Layout<Configuration<CorridorsData>, BasicEnergyData>, TNode, Configuration<CorridorsData>>(mapDescription, configurationSpaces, configurationSpacesGenerator.LastIntAliasMapping, corridorNodesCreator));
			layoutGenerator.SetLayoutEvolverCreator((mapDescription, layoutOperations) => new SimulatedAnnealingEvolver<Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>>(layoutOperations));
			layoutGenerator.SetLayoutOperationsCreator((mapDescription, configurationSpaces) =>
			{
				var corridorConfigurationSpaces = configurationSpacesGenerator.Generate<TNode, Configuration<CorridorsData>>(mapDescription, offsets);
				var layoutOperations = new LayoutOperationsWithCorridors<Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, IntAlias<GridPolygon>, CorridorsData, BasicEnergyData>(configurationSpaces, mapDescription, corridorConfigurationSpaces, configurationSpaces.GetAverageSize());
				var polygonOverlap = new FastPolygonOverlap();

				var averageSize = configurationSpaces.GetAverageSize();

				layoutOperations.AddNodeConstraint(new BasicContraint<Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, CorridorsData, IntAlias<GridPolygon>>(
					polygonOverlap,
					averageSize,
					configurationSpaces
				));

				layoutOperations.AddNodeConstraint(new CorridorConstraints<Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, CorridorsData, IntAlias<GridPolygon>>(
					mapDescription,
					averageSize,
					corridorConfigurationSpaces
				));

				if (!canTouch)
				{
					layoutOperations.AddNodeConstraint(new TouchingConstraints<Layout<Configuration<CorridorsData>, BasicEnergyData>, int, Configuration<CorridorsData>, CorridorsData, IntAlias<GridPolygon>>(
						mapDescription,
						polygonOverlap
					));
				}

				return layoutOperations;
			});

			return layoutGenerator;
		}
	}
}