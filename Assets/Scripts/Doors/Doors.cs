namespace Assets.Scripts.Doors
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	[ExecuteInEditMode]
	public class Doors : MonoBehaviour
	{
		public Vector3 firstPoint;
		public bool hasFirstPoint;

		public Vector3 secondPoint;
		public bool hasSecondPoint;

		[HideInInspector]
		public List<DoorInfo> doors = new List<DoorInfo>();
	}
}