namespace Assets.ProceduralLevelGenerator.Scripts.Data.Graphs
{
	using System;
	using System.Collections.Generic;
	using Rooms;
	using UnityEngine;
	using Utils;

	/// <summary>
	/// Represents a room in a level graph.
	/// </summary>
	public class Room : ScriptableObject, ISerializationCallbackReceiver
	{
		/// <summary>
		/// Name of the room.
		/// </summary>
		public string Name = "Room";

		/// <summary>
		/// Position of the room in the graph editor.
		/// </summary>
		/// <remarks>
		/// This valus is not used by the dungeon generator.
		/// </remarks>
		[HideInInspector]
		public Vector2 Position;

		/// <summary>
		/// Assigned room template sets.
		/// </summary>
		/// <remarks>
		/// This functionality is not included in GUI because it is not ready.
		/// </remarks>
		[HideInInspector]
		public List<RoomTemplatesSet> RoomTemplateSets = new List<RoomTemplatesSet>();
		
		/// <summary>
		/// Room templates assigned to the room.
		/// </summary>
		public List<GameObject> IndividualRoomTemplates = new List<GameObject>();

		/// <summary>
		/// GUID of a rooms group if the room is assigned to any.
		/// </summary>
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