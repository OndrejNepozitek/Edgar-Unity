namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Platformers
{
    public class PlatformerGeneratorFactory
    {
        //	/// <summary>
        //	/// Gets platformers level generator.
        //	/// </summary>
        //	/// <typeparam name="TNode"></typeparam>
        //	/// <returns></returns>
        //	public static ChainBasedGenerator<MapDescription<TNode>, Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IMapLayout<TNode>> GetPlatformerGenerator<TNode>()
        //	{
        //		var layoutGenerator = new ChainBasedGenerator<MapDescription<TNode>, Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IMapLayout<TNode>>();

        //		var chainDecomposition = new PlatformerChainDecomposition<int>();
        //		var configurationSpacesGenerator = new ConfigurationSpacesGenerator(new PolygonOverlap(), DoorHandler.DefaultHandler, new OrthogonalLineIntersection(), new GridPolygonUtils());
        //		var generatorPlanner = new BasicGeneratorPlanner<Layout<Configuration<EnergyData>, BasicEnergyData>>();

        //		layoutGenerator.SetChainDecompositionCreator(mapDescription => chainDecomposition);
        //		layoutGenerator.SetConfigurationSpacesCreator(mapDescription => configurationSpacesGenerator.Generate<TNode, Configuration<EnergyData>>(mapDescription));
        //		layoutGenerator.SetInitialLayoutCreator(mapDescription => new Layout<Configuration<EnergyData>, BasicEnergyData>(mapDescription.GetGraph()));
        //		layoutGenerator.SetGeneratorPlannerCreator(mapDescription => generatorPlanner);
        //		layoutGenerator.SetLayoutConverterCreator((mapDescription, configurationSpaces) => new BasicLayoutConverter<Layout<Configuration<EnergyData>, BasicEnergyData>, TNode, Configuration<EnergyData>>(mapDescription, configurationSpaces, configurationSpacesGenerator.LastIntAliasMapping));
        //		layoutGenerator.SetLayoutEvolverCreator((mapDescription, layoutOperations) => new PlatformerLayoutEvolver<Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IntAlias<GridPolygon>>(
        //			(PlatformerLayoutOperations<Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IntAlias<GridPolygon>, EnergyData, BasicEnergyData>)layoutOperations));
        //		layoutGenerator.SetLayoutOperationsCreator((mapDescription, configurationSpaces) =>
        //		{
        //			var layoutOperations = new PlatformerLayoutOperations<Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, IntAlias<GridPolygon>, EnergyData, BasicEnergyData>(configurationSpaces, configurationSpaces.GetAverageSize());

        //			var averageSize = configurationSpaces.GetAverageSize();

        //			layoutOperations.AddNodeConstraint(new BasicContraint<Layout<Configuration<EnergyData>, BasicEnergyData>, int, Configuration<EnergyData>, EnergyData, IntAlias<GridPolygon>>(
        //				new FastPolygonOverlap(),
        //				averageSize,
        //				configurationSpaces
        //			));

        //			return layoutOperations;
        //		});

        //		return layoutGenerator;
        //	}
        //}
    }
}