namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Platformers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.Configurations.EnergyData;
	using MapGeneration.Interfaces.Core.Configuration;
	using MapGeneration.Interfaces.Core.LayoutEvolvers;
	using MapGeneration.Interfaces.Core.Layouts;
	using MapGeneration.Interfaces.Utils;
	using MapGeneration.Utils;

	/// <summary>
	/// Layout evolvoer for platformers.
	/// Experimental. Without proper documentation.
	/// </summary>
	/// <typeparam name="TLayout"></typeparam>
	/// <typeparam name="TNode"></typeparam>
	/// <typeparam name="TConfiguration"></typeparam>
	/// <typeparam name="TShapeContainer"></typeparam>
	public class PlatformerLayoutEvolver<TLayout, TNode, TConfiguration, TShapeContainer> : ILayoutEvolver<TLayout, TNode>, IRandomInjectable, ICancellable
		where TLayout : ISmartCloneable<TLayout>, IEnergyLayout<TNode, TConfiguration, BasicEnergyData>
		where TConfiguration : IEnergyConfiguration<TShapeContainer, EnergyData>, ISmartCloneable<TConfiguration>, new()
	{
		protected Random Random;
		protected CancellationToken? CancellationToken;
		protected readonly int NumberOfAttemptsTotal = 10;
		protected readonly int NumberOfAttemptsPerNode = 10;
		protected readonly PlatformerLayoutOperations<TLayout, TNode, TConfiguration, TShapeContainer, EnergyData, BasicEnergyData> LayoutOperations;

		public event Action<TLayout> OnPerturbed;

		public event Action<TLayout> OnValid;

		public PlatformerLayoutEvolver(PlatformerLayoutOperations<TLayout, TNode, TConfiguration, TShapeContainer, EnergyData, BasicEnergyData> layoutOperations)
		{
			LayoutOperations = layoutOperations;
		}

		public IEnumerable<TLayout> Evolve(TLayout initialLayout, IList<TNode> chain, int count)
		{
			for (int i = 0; i < NumberOfAttemptsTotal; i++)
			{
				var layout = initialLayout.SmartClone();
				var nodesCount = 0;

				foreach (var node in chain)
				{
					if (TryLayoutNode(layout, node, out var newLayout))
					{
						layout = newLayout;
						nodesCount++;
					}
					else
					{
						break;
					}
				}

				if (nodesCount == chain.Count)
				{
					OnValid?.Invoke(layout);
					yield return layout;
				}
			}

			yield break;
		}

		private bool TryLayoutNode(TLayout layout, TNode node, out TLayout newLayout)
		{
			// possibleShapes.Shuffle(Random);
			var neighbouringConfigurations = GetNeighbouringConfigurations(layout, node);

			var possibleShapes = LayoutOperations.GetShapesForNode(node);
			var notUsedShapes = possibleShapes.Where(x => neighbouringConfigurations.All(y => !y.ShapeContainer.Equals(x))).ToList();

			if (notUsedShapes.Count != 0)
			{
				possibleShapes = notUsedShapes;
			}

			for (int i = 0; i < NumberOfAttemptsPerNode; i++)
			{
				var shape = possibleShapes.GetRandom(Random);
				var position = LayoutOperations.GetRandomPosition(shape, neighbouringConfigurations);
				var isValid = LayoutOperations.IsValid(layout, node, shape, position);

				// TODO: this is not really needed
				newLayout = layout.SmartClone();
				newLayout.SetConfiguration(node, CreateConfiguration(shape, position));
				OnPerturbed?.Invoke(newLayout);

				if (isValid)
				{
					return true;
				}
			}

			newLayout = default;
			return false;
		}

		protected TConfiguration CreateConfiguration(TShapeContainer shapeContainer, IntVector2 position)
		{
			var configuration = new TConfiguration
			{
				ShapeContainer = shapeContainer,
				Position = position
			};

			return configuration;
		}

		private List<TConfiguration> GetNeighbouringConfigurations(TLayout layout, TNode node)
		{
			var configurations = new List<TConfiguration>();
			var neighbours = layout.Graph.GetNeighbours(node);

			foreach (var neighbour in neighbours)
			{
				if (layout.GetConfiguration(neighbour, out var configuration))
				{
					configurations.Add(configuration);
				}
			}

			return configurations;
		}

		public void InjectRandomGenerator(Random random)
		{
			Random = random;
		}

		public void SetCancellationToken(CancellationToken? cancellationToken)
		{
			CancellationToken = cancellationToken;
		}
	}
}