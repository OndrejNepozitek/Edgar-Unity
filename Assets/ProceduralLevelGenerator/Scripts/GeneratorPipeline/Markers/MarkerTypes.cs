namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.Markers
{
	using System;
	using UnityEngine;

	[Obsolete("Marker maps should not be used.")]
	public class MarkerTypes
	{
		public static MarkerType Nothing;

		public static MarkerType Wall;

		public static MarkerType Floor;

		public static MarkerType Door;

		public static MarkerType UnderDoor;

		static MarkerTypes()
		{
			Nothing = Resources.Load<MarkerType>("MarkerTypes/Nothing");
			Wall = Resources.Load<MarkerType>("MarkerTypes/Wall");
			Floor = Resources.Load<MarkerType>("MarkerTypes/Floor");
			Door = Resources.Load<MarkerType>("MarkerTypes/Door");
			UnderDoor = Resources.Load<MarkerType>("MarkerTypes/UnderDoor");
		}
	}
}