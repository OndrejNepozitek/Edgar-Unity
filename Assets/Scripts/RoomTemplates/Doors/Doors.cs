namespace Assets.Scripts.RoomTemplates.Doors
{
	using System.Collections.Generic;
	using UnityEngine;

	[ExecuteInEditMode]
	public class Doors : MonoBehaviour
	{
		[HideInInspector]
		public Vector3 FirstPoint;

		[HideInInspector]
		public bool HasFirstPoint;

		[HideInInspector]
		public Vector3 SecondPoint;

		[HideInInspector]
		public bool HasSecondPoint;

		[HideInInspector]
		public List<DoorInfo> DoorsList = new List<DoorInfo>();
	}
}