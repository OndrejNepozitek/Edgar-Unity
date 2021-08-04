using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class HybridDoorModeDataGrid2D : IDoorModeDataGrid2D
    {
        [Min(1)]
        public int DefaultLength = 1;

        public List<DoorLineGrid2D> DoorLines = new List<DoorLineGrid2D>();

        public IDoorModeGrid2D GetDoorMode(DoorsGrid2D doors)
        {
            var transformedDoorLines = DoorLines
                .Select(x => x.ToInternal())
                .ToList();

            return new ManualDoorModeGrid2D(transformedDoorLines);
        }
    }
}