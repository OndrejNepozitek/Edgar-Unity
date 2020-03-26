using System;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors
{
    /// <summary>
    ///     Door information for editor usage.
    /// </summary>
    [Serializable]
    public class DoorInfoEditor
    {
        public Vector3 From;

        public Vector3 To;

        #region Equals

        protected bool Equals(DoorInfoEditor other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DoorInfoEditor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode() * 397) ^ To.GetHashCode();
            }
        }

        public static bool operator ==(DoorInfoEditor left, DoorInfoEditor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DoorInfoEditor left, DoorInfoEditor right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}