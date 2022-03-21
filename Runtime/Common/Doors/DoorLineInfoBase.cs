using System;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Provides information about which parts (if any) of a door line were actually used to place a door.
    /// </summary>
    /// <remarks>
    /// In the manual door mode, each door line contains exactly one position where a door can be placed.
    /// In the simple and hybrid door modes, each door line usually contains multiple positions where a door can be placed.
    ///
    /// The usual use case of this class is to use the GetTiles() method to get all the tiles on the door line.
    /// Each tile contains information about whether the position/tile is used in any door and if so, in which door.
    /// </remarks>
    /// <typeparam name="TDoorInstance"></typeparam>
    [Serializable]
    public abstract class DoorLineInfoBase<TDoorInstance, TDoorLine> 
        where TDoorInstance : class
        where TDoorLine : IDoorLine
    {
        /// <summary>
        /// Door line that described where doors can start and how wide they are.
        /// </summary>
        public TDoorLine DoorLine => doorLine;

        [SerializeField]
        private TDoorLine doorLine;

        [SerializeField]
        private SerializableVector3Int direction;

        /// <summary>
        /// All the doors that are used in the level and are contained in the door line.
        /// </summary>
        public List<TDoorInstance> UsedDoors => usedDoors;

        [SerializeField]
        private List<TDoorInstance> usedDoors;

        protected DoorLineInfoBase(TDoorLine doorLine, SerializableVector3Int direction, List<TDoorInstance> usedDoors)
        {
            this.doorLine = doorLine;
            this.direction = direction;
            this.usedDoors = usedDoors;
        }

        /// <summary>
        /// Returns all the tiles/positions that are contained in the door line.
        /// Each tile comes with information whether it was used for a door or not.
        /// </summary>
        /// <returns></returns>
        public List<TileInfo> GetTiles()
        {
            var from = doorLine.From;
            var to = doorLine.To + (Vector3Int) direction * (doorLine.Length - 1);
            var line = new OrthogonalLine(from, to);
            var result = new List<TileInfo>();

            foreach (var position in line.GetPoints())
            {
                var isUsed = false;

                foreach (var doorInstance in UsedDoors)
                {
                    var index = GetLine(doorInstance).Contains(position);

                    if (index != -1)
                    {
                        result.Add(new TileInfo(position, true, doorInstance, index));
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed)
                {
                    result.Add(new TileInfo(position, false, null, -1));
                }
            }

            return result;
        }

        protected abstract OrthogonalLine GetLine(TDoorInstance doorInstance);

        /// <summary>
        /// Information about a tile on a door line.
        /// </summary>
        public class TileInfo
        {
            /// <summary>
            /// Position of the tile, relative to the room template.
            /// </summary>
            public Vector3Int Position { get; }

            /// <summary>
            /// Whether the tile is used for a door or not.
            /// </summary>
            public bool IsUsed { get; }

            /// <summary>
            /// If the tile was used for a door, this property contains the corresponding door instance.
            /// </summary>
            public TDoorInstance Door { get; }

            /// <summary>
            /// If the tile is used for a door, this property marks the position of this tile inside that door.
            /// If the door is 3 tiles wide, there will be 3 TileInfos for that corresponding door, with indexes 0, 1 and 2.
            /// </summary>
            public int IndexInsideDoor { get; }

            public TileInfo(Vector3Int position, bool isUsed, TDoorInstance door, int indexInsideDoor)
            {
                IsUsed = isUsed;
                Door = door;
                IndexInsideDoor = indexInsideDoor;
                Position = position;
            }
        }
    }
}