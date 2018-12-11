namespace Assets.Scripts.DataOld
{
	using System.Collections.Generic;

	public class DataWrapper
	{
		public Dictionary<int, RoomShape> RoomShapes { get; set; } = new Dictionary<int, RoomShape>();

		public Dictionary<int, RoomShapeSet> RoomShapeSets { get; set; } = new Dictionary<int, RoomShapeSet>();
	}
}