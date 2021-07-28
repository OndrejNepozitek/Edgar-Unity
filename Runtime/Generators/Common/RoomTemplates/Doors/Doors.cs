using System;
using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Doors MonoBehaviour that is used to define doors for room templates.
    /// </summary>
    [ExecuteInEditMode]
    public class Doors : VersionedMonoBehaviour
    {
        [Obsolete]
        public int DistanceFromCorners = 1;

        [Obsolete]
        public int DoorLength = 1;

        [Obsolete]
        public List<Door> DoorsList = new List<Door>();

        [HideInInspector]
        public DoorMode SelectedMode;

        public HybridDoorModeData HybridDoorModeData;

        public ManualDoorModeData ManualDoorModeData;

        public SimpleDoorModeData SimpleDoorModeData;

        public IDoorModeGrid2D GetDoorMode()
        {
            if (SelectedMode == DoorMode.Manual)
            {
                return ManualDoorModeData.GetDoorMode(this);
            }

            if (SelectedMode == DoorMode.Simple)
            {
                return SimpleDoorModeData.GetDoorMode(this);
            }

            if (SelectedMode == DoorMode.Hybrid)
            {
                return HybridDoorModeData.GetDoorMode(this);
            }

            throw new ArgumentException("Invalid door mode selected");
        }

        protected override int OnUpgradeSerializedData(int version)
        {
            if (version == 1)
            {
#pragma warning disable 612
                SimpleDoorModeData.DistanceFromCorners = DistanceFromCorners;
                SimpleDoorModeData.DoorLength = DoorLength;
                ManualDoorModeData.DoorsList = DoorsList;
#pragma warning restore 612
            }

            return 2;
        }

        public enum DoorMode
        {
            Simple = 0,
            Manual = 1,
            Hybrid = 2,
        }
    }
}