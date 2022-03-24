using System;
using System.Collections.Generic;

namespace Edgar.Unity
{
    /// <inheritdoc />
    [Serializable]
    public class DoorLineInfoGrid2D : DoorLineInfoBase<DoorInstanceGrid2D, DoorLineGrid2D>
    {
        public DoorLineInfoGrid2D(DoorLineGrid2D doorLine, SerializableVector3Int direction, List<DoorInstanceGrid2D> usedDoors) : base(doorLine, direction, usedDoors)
        {
        }

        protected override OrthogonalLine GetLine(DoorInstanceGrid2D doorInstance)
        {
            return doorInstance.DoorLine;
        }
    }
}