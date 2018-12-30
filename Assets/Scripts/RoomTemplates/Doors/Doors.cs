namespace Assets.Scripts.RoomTemplates.Doors
{
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;
	using Transformations;
	using UnityEngine;
	using Utils;

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

		public void Transform(Transformation transformation)
		{
			var newDoorsList = new List<DoorInfo>();

			foreach (var doorInfo in DoorsList)
			{
				// TODO: ugly
				var newFrom = doorInfo.From.RoundToUnityIntVector3().ToCustomIntVector2().Transform(transformation);
				var newTo = doorInfo.To.RoundToUnityIntVector3().ToCustomIntVector2().Transform(transformation);

				newDoorsList.Add(new DoorInfo()
				{
					From = new Vector3(newFrom.X, newFrom.Y),
					To = new Vector3(newTo.X, newTo.Y),
				});
			}

			DoorsList = newDoorsList;
		}
	}
}