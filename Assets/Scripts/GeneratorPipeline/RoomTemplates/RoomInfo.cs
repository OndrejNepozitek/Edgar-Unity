namespace Assets.Scripts.GeneratorPipeline.RoomTemplates
{
	using MapGeneration.Interfaces.Core.MapLayouts;
	using UnityEngine;

	public class RoomInfo<TNode>
	{
		public IRoom<TNode> LayoutRoom { get; set; }

		public GameObject Room { get; set; }

		public GameObject RoomTemplate { get; set; }
	}
}