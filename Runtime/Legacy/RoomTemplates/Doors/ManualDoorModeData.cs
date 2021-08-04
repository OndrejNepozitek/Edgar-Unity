using System;
using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Grid2D;

namespace Edgar.Unity
{
    [Serializable]
    public class ManualDoorModeData : IDoorModeData
    {
        public List<DoorGrid2D> DoorsList = new List<DoorGrid2D>();

        public IDoorModeGrid2D GetDoorMode(Doors doorsComponent)
        {
            var doors = new List<GraphBasedGenerator.Grid2D.DoorGrid2D>();

            foreach (var door in DoorsList)
            {
                // TODO: ugly
                var doorLine = new GraphBasedGenerator.Grid2D.DoorGrid2D(
                    door.From.RoundToUnityIntVector3().ToCustomIntVector2(),
                    door.To.RoundToUnityIntVector3().ToCustomIntVector2()
                );

                doors.Add(doorLine);
            }

            return new ManualDoorModeGrid2D(doors);
        }
    }
}