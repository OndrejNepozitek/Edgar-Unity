namespace Assets.Scripts.DataOld
{
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;

	public class RoomShape
	{
		public string Name { get; set; }

		public IEnumerable<IntVector2> GridPoints { get; set; }
	}
}