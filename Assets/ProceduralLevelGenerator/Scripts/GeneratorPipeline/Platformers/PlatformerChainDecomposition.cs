namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Platformers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using GeneralAlgorithms.Algorithms.Common;
	using GeneralAlgorithms.DataStructures.Graphs;
	using MapGeneration.Core.ChainDecompositions;

	/// <summary>
	/// Chain decomposition for platformers.
	/// Experimental. Without proper documentation.
	/// </summary>
	/// <typeparam name="TNode"></typeparam>
	public class PlatformerChainDecomposition<TNode> : ChainDecompositionBase<TNode>
	{
		private const int ChainSize = 6;
		private Dictionary<TNode, int> subtreeSize;
		private TNode startingNode;

		public override List<List<TNode>> GetChains(IGraph<TNode> graph)
		{
			Initialize(graph);
			subtreeSize = new Dictionary<TNode, int>();

			startingNode = graph.Vertices.First(x => graph.GetNeighbours(x).Count() == 1);
			ComputeSubtreeSizes(startingNode);

			var chains = new List<List<TNode>>();
			while (graph.Vertices.Any(x => !IsCovered(x)))
			{
				chains.Add(GetNextChain());
			}

			return chains;
		}

		protected List<TNode> GetNextChain()
		{
			var bestNode = startingNode;
			var bestDepth = int.MaxValue;
			var bestSize = int.MinValue;

			foreach (var node in Graph.Vertices.Where(x => !IsCovered(x)))
			{
				var depth = SmallestCoveredNeighbourDepth(node);
				var size = subtreeSize[node];

				if (depth <= bestDepth)
				{
					if (depth < bestDepth || size > bestSize)
					{
						bestNode = node;
						bestDepth = depth;
						bestSize = size;
					}
				}
			}

			var chain = new List<TNode> {};
			var currentNode = bestNode;

			while (true)
			{
				chain.Add(currentNode);
				SetDepth(currentNode, ChainsCounter);

				foreach (var neighbour in Graph.GetNeighbours(currentNode).Where(x => !IsCovered(x)))
				{
					chain.Add(neighbour);
					SetDepth(neighbour, ChainsCounter);
				}

				if (Graph.Vertices.All(IsCovered) || chain.Count > ChainSize)
					break;

				var nonCoveredVertices = Graph.Vertices.Where(x => !IsCovered(x)).ToList();
				var smallestDepthVertexIndex = nonCoveredVertices.MinBy(SmallestCoveredNeighbourDepth);
				currentNode = nonCoveredVertices[smallestDepthVertexIndex];
			}

			ChainsCounter++;
			return chain;
		}

		protected void ComputeSubtreeSizes(TNode node)
		{
			var size = 1;

			foreach (var neighbour in Graph.GetNeighbours(node))
			{
				size += SubtreeSize(neighbour, node);
			}

			subtreeSize[node] = size;
		}

		protected int SubtreeSize(TNode node, TNode parent)
		{
			var size = 1;

			foreach (var neighbour in Graph.GetNeighbours(node))
			{
				if (neighbour.Equals(parent))
					continue;

				if (subtreeSize.ContainsKey(node))
					throw new ArgumentException("A given graph is not a tree");

				size += SubtreeSize(neighbour, node);
			}

			subtreeSize[node] = size;

			return size;
		}
	}
}