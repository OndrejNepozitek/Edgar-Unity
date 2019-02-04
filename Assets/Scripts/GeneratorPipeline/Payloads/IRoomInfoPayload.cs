namespace Assets.Scripts.GeneratorPipeline.Payloads
{
	using System.Collections.Generic;
	using RoomTemplates;

	public interface IRoomInfoPayload
	{
		List<RoomInfo<int>> RoomInfos { get; set; }
	}
}