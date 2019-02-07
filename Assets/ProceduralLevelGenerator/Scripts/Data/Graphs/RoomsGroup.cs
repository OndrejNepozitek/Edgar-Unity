namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using System;
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;

	[Serializable]
	public class RoomsGroup : ISerializationCallbackReceiver
	{
		public string Name = "New group";

		public List<RoomTemplatesSet> RoomTemplateSets = new List<RoomTemplatesSet>();

		public List<GameObject> IndividualRoomTemplates = new List<GameObject>();

		[HideInInspector]
		public Guid Guid = Guid.NewGuid();

		[SerializeField]
		private byte[] serializedGuid;

		public void OnBeforeSerialize()
		{
			if (Guid != Guid.Empty)
			{
				serializedGuid = Guid.ToByteArray();
			}
		}

		public void OnAfterDeserialize()
		{
			if (serializedGuid != null && serializedGuid.Length == 16)
			{
				Guid = new Guid(serializedGuid);
			}
		}
	}
}