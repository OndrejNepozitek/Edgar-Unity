using System;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Common.Doors;
using UnityEngine;
using UnityEngine.Serialization;

namespace Edgar.Unity
{
    [Serializable]
    public class DoorLineGrid2D
    {
        public Vector3Int From => from;

        [SerializeField, FormerlySerializedAs("From")]
        private Vector3Int from;

        public Vector3Int To => to;

        [SerializeField, FormerlySerializedAs("To")]
        private Vector3Int to;

        public int Length => length;

        [SerializeField, FormerlySerializedAs("Length")]
        private int length;

        public DoorLineGrid2D(Vector3Int @from, Vector3Int to, int length)
        {
            this.from = from;
            this.to = to;
            this.length = length;
        }

        internal GraphBasedGenerator.Grid2D.DoorLineGrid2D ToInternal()
        {
            var line = new OrthogonalLineGrid2D(From.ToCustomIntVector2(), To.ToCustomIntVector2());

            if (Length > 1)
            {
                line = line.Shrink(0, Length - 1);
            }

            return new GraphBasedGenerator.Grid2D.DoorLineGrid2D(
                line,
                Length - 1,
                null,
                DoorType.Undirected);
        }

        #region Equals

        protected bool Equals(DoorLineGrid2D other)
        {
            return From.Equals(other.From) && To.Equals(other.To) && Length == other.Length;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DoorLineGrid2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = From.GetHashCode();
                hashCode = (hashCode * 397) ^ To.GetHashCode();
                hashCode = (hashCode * 397) ^ Length;
                return hashCode;
            }
        }

        public static bool operator ==(DoorLineGrid2D left, DoorLineGrid2D right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DoorLineGrid2D left, DoorLineGrid2D right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}