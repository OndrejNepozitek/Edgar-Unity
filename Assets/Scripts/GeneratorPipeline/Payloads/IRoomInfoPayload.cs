namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using Data.Graphs;
	using RoomTemplates;

	public interface IRoomInfoPayload
	{
		List<RoomInfo<Room>> RoomInfos { get; set; }
	}
}