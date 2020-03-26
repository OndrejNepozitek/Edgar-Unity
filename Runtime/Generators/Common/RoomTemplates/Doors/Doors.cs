using System;
using System.Collections.Generic;
using Assets.ProceduralLevelGenerator.Scripts.Utils;
using GeneralAlgorithms.DataStructures.Common;
using MapGeneration.Core.Doors.DoorModes;
using MapGeneration.Interfaces.Core.Doors;
using UnityEngine;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.RoomTemplates.Doors
{
    /// <summary>
    ///     Doors MonoBehaviour that is used to define doors for room templates.
    /// </summary>
    [ExecuteInEditMode]
    public class Doors : MonoBehaviour
    {
        [HideInInspector]
        public int DistanceFromCorners = 1;

        [HideInInspector]
        public int DoorLength = 1;

        [HideInInspector]
        public List<DoorInfoEditor> DoorsList = new List<DoorInfoEditor>();

        [HideInInspector]
        public int SelectedMode;

        public void Transform(Transformation transformation)
        {
            var newDoorsList = new List<DoorInfoEditor>();

            foreach (var doorInfo in DoorsList)
            {
                // TODO: ugly
                var newFrom = doorInfo.From.RoundToUnityIntVector3().ToCustomIntVector2().Transform(transformation);
                var newTo = doorInfo.To.RoundToUnityIntVector3().ToCustomIntVector2().Transform(transformation);

                newDoorsList.Add(new DoorInfoEditor
                {
                    From = new Vector3(newFrom.X, newFrom.Y),
                    To = new Vector3(newTo.X, newTo.Y)
                });
            }

            DoorsList = newDoorsList;
        }

        public IDoorMode GetDoorMode()
        {
            if (SelectedMode == 1)
            {
                var doorLines = new List<OrthogonalLine>();

                foreach (var door in DoorsList)
                {
                    var doorLine = new OrthogonalLine(door.From.RoundToUnityIntVector3().ToCustomIntVector2(),
                        door.To.RoundToUnityIntVector3().ToCustomIntVector2()); // TODO: ugly

                    doorLines.Add(doorLine);
                }

                return new ManualDoorMode(doorLines);
            }

            if (SelectedMode == 0)
            {
                return new SimpleDoorMode(DoorLength - 1, DistanceFromCorners);
            }

            throw new ArgumentException("Invalid door mode selected");
        }
    }
}