namespace Assets.ProceduralLevelGenerator.Scripts.GeneratorPipeline.DungeonGenerators
{
	using System.Collections.Generic;
	using RoomTemplates;

	public class Layout<TRoom>
	{
		private readonly Dictionary<TRoom, RoomInfo<TRoom>> data;

		public Layout(Dictionary<TRoom, RoomInfo<TRoom>> data)
		{
			this.data = data;
		}

		public RoomInfo<TRoom> GetRoomInfo(TRoom room)
		{
			return data[room];
		}

		public IEnumerable<RoomInfo<TRoom>> GetAllRoomInfo()
		{
			return data.Values;
		}
	}
}