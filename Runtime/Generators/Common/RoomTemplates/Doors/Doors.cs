using System;
using System.Collections.Generic;
using GeneralAlgorithms.DataStructures.Common;
using MapGeneration.Core.Doors.DoorModes;
using MapGeneration.Core.Doors.Interfaces;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors
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
        public DoorMode SelectedMode;

        public IDoorMode GetDoorMode()
        {
            if (SelectedMode == DoorMode.Manual)
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

            if (SelectedMode == DoorMode.Simple)
            {
                return new SimpleDoorMode(DoorLength - 1, DistanceFromCorners);
            }

            throw new ArgumentException("Invalid door mode selected");
        }

        public enum DoorMode
        {
            Simple = 0,
            Manual = 1,
        }
    }
}