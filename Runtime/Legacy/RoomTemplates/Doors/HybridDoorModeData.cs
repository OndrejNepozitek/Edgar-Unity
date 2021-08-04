using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common.Doors;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class HybridDoorModeData : IDoorModeData
    {
        [Min(1)]
        public int DefaultLength = 1;

        public List<DoorLine> DoorLines = new List<DoorLine>();

        public IDoorModeGrid2D GetDoorMode(Doors doors)
        {
            var transformedDoorLines = DoorLines
                .Select(x => x.ToInternal())
                .ToList();

            return new ManualDoorModeGrid2D(transformedDoorLines);
        }
    }
}