using System;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Door information for editor usage.
    /// </summary>
    [Serializable]
    public class DoorGrid2D
    {
        [HideInInspector]
        public Vector3 From;

        [HideInInspector]
        public Vector3 To;

        #region Equals

        protected bool Equals(DoorGrid2D other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DoorGrid2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode() * 397) ^ To.GetHashCode();
            }
        }

        public static bool operator ==(DoorGrid2D left, DoorGrid2D right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DoorGrid2D left, DoorGrid2D right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}