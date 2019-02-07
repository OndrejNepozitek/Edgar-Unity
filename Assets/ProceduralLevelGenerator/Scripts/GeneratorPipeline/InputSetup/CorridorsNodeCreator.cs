namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.InputSetup
{
	using System;
	using System.Linq;
	using Data.Graphs;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Interfaces.Core.LayoutConverters;
	using MapGeneration.Interfaces.Core.MapDescriptions;
	using UnityEngine;

	/// <summary>
	/// Class that can create instaces of the Room class for corridor rooms.
	/// It is needed because corridor nodes do not exist in the original input graph
	/// but we need them to properly create the output.
	/// 
	/// The new instances must be computed right when the instance of this class is created
	/// because Unity does not allow creating instances of scriptable objects outside 
	/// the main thread.
	/// </summary>
	public class CorridorsNodeCreator : ICorridorNodesCreator<Room>
	{
		private readonly TwoWayDictionary<Room, int> precomputedMapping = new TwoWayDictionary<Room, int>();

		private bool alreadyUsed;

		public CorridorsNodeCreator(ICorridorMapDescription<int> mapDescription)
		{
			PrepareMapping(mapDescription);
		}

		private void PrepareMapping(ICorridorMapDescription<int> mapDescription)
		{
			var graph = mapDescription.GetGraph();
			var corridors = graph.Vertices.Where(mapDescription.IsCorridorRoom).ToList();

			foreach (var corridor in corridors)
			{
				var room = ScriptableObject.CreateInstance<Room>();
				precomputedMapping.Add(room, corridor);
			}
		}

		public void AddCorridorsToMapping(ICorridorMapDescription<int> mapDescription, TwoWayDictionary<Room, int> mapping)
		{
			if (alreadyUsed)
			{
				throw new InvalidOperationException("Each instance of this class can be used at most once because it is tied to a specific map description.");
			}

			foreach (var pair in precomputedMapping)
			{
				mapping.Add(pair.Key, pair.Value);
			}

			alreadyUsed = true;
		}
	}
}