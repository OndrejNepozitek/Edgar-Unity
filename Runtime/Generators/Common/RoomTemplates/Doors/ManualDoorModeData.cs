using System;
using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;

namespace Edgar.Unity
{
    [Serializable]
    public class ManualDoorModeData : IDoorModeData
    {
        public List<Door> DoorsList = new List<Door>();

        public IDoorModeGrid2D GetDoorMode(Doors doorsComponent)
        {
            var doors = new List<DoorGrid2D>();

            foreach (var door in DoorsList)
            {
                // TODO: ugly
                var doorLine = new DoorGrid2D(
                    door.From.RoundToUnityIntVector3().ToCustomIntVector2(),
                    door.To.RoundToUnityIntVector3().ToCustomIntVector2()
                );

                doors.Add(doorLine);
            }

            return new ManualDoorModeGrid2D(doors);
        }
    }
}