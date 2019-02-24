namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using System;
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;
	using Utils;

	public class Room : ScriptableObject, ISerializationCallbackReceiver, IRoom
	{
		public string Name = "Room";

		[HideInInspector]
		public Vector2 Position;

		[HideInInspector]
		public List<RoomTemplatesSet> RoomTemplateSets = new List<RoomTemplatesSet>();
		
		public List<GameObject> IndividualRoomTemplates = new List<GameObject>();

		[HideInInspector]
		public Guid RoomsGroupGuid;

		// TODO: this should be done differently. It is not possible to handle every GUID manually.
		#region Pretty ugly GUID handling

		[HideInInspector]
		[SerializeField]
		private byte[] serializedGuid; 

		public void OnBeforeSerialize()
		{
			serializedGuid = RoomsGroupGuid.ToByteArray();
		}

		public void OnAfterDeserialize()
		{
			if (serializedGuid != null && serializedGuid.Length == 16)
			{
				RoomsGroupGuid = new Guid(serializedGuid);
			}
			else
			{
				RoomsGroupGuid = Guid.Empty;
			}
		}

		#endregion
	}
}