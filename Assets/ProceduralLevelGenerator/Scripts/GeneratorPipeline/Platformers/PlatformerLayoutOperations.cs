namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Platformers
{
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.ConfigurationSpaces;
	using MapGeneration.Core.LayoutOperations;
	using MapGeneration.Interfaces.Core.Configuration;
	using MapGeneration.Interfaces.Core.Configuration.EnergyData;
	using MapGeneration.Interfaces.Core.ConfigurationSpaces;
	using MapGeneration.Interfaces.Core.Layouts;
	using MapGeneration.Interfaces.Utils;

	/// <summary>
	/// Layout operations for platformers.
	/// Experimental. Without proper documentation.
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <typeparam name="TNode"></typeparam>
	/// <typeparam name="TConfiguration"></typeparam>
	/// <typeparam name="TShapeContainer"></typeparam>
	/// <typeparam name="TEnergyData"></typeparam>
	/// <typeparam name="TLayoutEnergyData"></typeparam>
	public class PlatformerLayoutOperations<TLayout, TNode, TConfiguration, TShapeContainer, TEnergyData, TLayoutEnergyData> : LayoutOperationsWithConstraints<TLayout, TNode, TConfiguration, TShapeContainer, TEnergyData, TLayoutEnergyData>
		where TLayout : IEnergyLayout<TNode, TConfiguration, TLayoutEnergyData>, ISmartCloneable<TLayout>
		where TConfiguration : IEnergyConfiguration<TShapeContainer, TEnergyData>, ISmartCloneable<TConfiguration>, new()
		where TEnergyData : IEnergyData, new()
		where TLayoutEnergyData : IEnergyData, new()
	{
		public PlatformerLayoutOperations(IConfigurationSpaces<TNode, TShapeContainer, TConfiguration, ConfigurationSpace> configurationSpaces, int averageSize) : base(configurationSpaces, averageSize)
		{
		}

		public bool IsValid(TLayout layout, TNode node, TShapeContainer shape, IntVector2 position)
		{
			return NodeComputeEnergyData(layout, node, CreateConfiguration(shape, position)).IsValid;
		}

		public IntVector2 GetRandomPosition(TShapeContainer shape, IList<TConfiguration> configurations)
		{
			return ConfigurationSpaces.GetRandomIntersectionPoint(CreateConfiguration(shape, new IntVector2()), configurations);
		}

		public IList<TShapeContainer> GetShapesForNode(TNode node)
		{
			return ConfigurationSpaces.GetShapesForNode(node).ToList();
		}
	}
}