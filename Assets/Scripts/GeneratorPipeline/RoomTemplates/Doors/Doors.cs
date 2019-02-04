namespace Assets.Scripts.GeneratorPipeline.RoomTemplates.Doors
{
	using System;
	using System.Collections.Generic;
	using GeneralAlgorithms.DataStructures.Common;
	using MapGeneration.Core.Doors.DoorModes;
	using MapGeneration.Interfaces.Core.Doors;
	using UnityEngine;
	using Utils;

	[ExecuteInEditMode]
	public class Doors : MonoBehaviour
	{
		[HideInInspector]
		public List<DoorInfo> DoorsList = new List<DoorInfo>();

		[HideInInspector]
		public int SelectedMode;

		[HideInInspector]
		public int DoorLength = 1;

		[HideInInspector]
		public int DistanceFromCorners = 1;

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

		public IDoorMode GetDoorMode()
		{
			if (SelectedMode == 0)
			{
				var doorLines = new List<OrthogonalLine>();

				foreach (var door in DoorsList)
				{
					var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(), door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

					doorLines.Add(doorLine);
				}

				return new SpecificPositionsMode(doorLines);
			}

			if (SelectedMode == 1)
			{
				return new OverlapMode(DoorLength - 1, DistanceFromCorners);
			}

			throw new ArgumentException("Invalid door mode selected");
		}
	}
}