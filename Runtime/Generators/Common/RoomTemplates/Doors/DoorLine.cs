using System;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common.Doors;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class DoorLine
    {
        public Vector3Int From;

        public Vector3Int To;

        public int Length;

        public DoorLineGrid2D ToInternal()
        {
            var line = new OrthogonalLineGrid2D(From.ToCustomIntVector2(), To.ToCustomIntVector2());

            if (Length > 1)
            {
                line = line.Shrink(0, Length - 1);
            }
            
            return new DoorLineGrid2D(
                line,
                Length - 1,
                null,
                DoorType.Undirected);
        }
    }
}