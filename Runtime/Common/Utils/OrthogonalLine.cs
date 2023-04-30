using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Structure representing an orthogonal line in a integer grid.
    /// </summary>
    [Serializable]
    public struct OrthogonalLine : IEquatable<OrthogonalLine>
    {
        [SerializeField]
        private int fromX;

        [SerializeField]
        private int fromY;

        [SerializeField]
        private int fromZ;

        [SerializeField]
        private int toX;

        [SerializeField]
        private int toY;

        [SerializeField]
        private int toZ;

        public Vector3Int From => new Vector3Int(fromX, fromY, fromZ);

        public Vector3Int To => new Vector3Int(toX, toY, toZ);

        /// <summary>
        /// Returns number of points on the line.
        /// If From equals To, the Length is 1 (point).
        /// </summary>
        public int Length { get; }

        public Vector3Int Direction { get; }

        /// <summary>
        /// Construct an orthogonal line from given endpoints.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <exception cref="ArgumentException">Thrown when given points do not form an orthogonal line.</exception>
        public OrthogonalLine(Vector3Int from, Vector3Int to)
        {
            var sameComponentsCount = 0;
            if (from.x == to.x) sameComponentsCount++;
            if (from.y == to.y) sameComponentsCount++;
            if (from.z == to.z) sameComponentsCount++;

            if (sameComponentsCount < 2)
            {
                throw new ArgumentException("The line is not orthogonal. At least two components of from and to must be equal.");
            }

            fromX = from.x;
            fromY = from.y;
            fromZ = from.z;

            toX = to.x;
            toY = to.y;
            toZ = to.z;

            var direction = to - from;
            direction.Clamp(new Vector3Int(-1, -1, -1), new Vector3Int(1, 1, 1));
            Direction = direction;

            Length = 0;
            Length = GetLength(From, To);
            
        }

        private static int GetLength(Vector3Int from, Vector3Int to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y) + Math.Abs(from.z - to.z) + 1;
        }

        /// <summary>
        /// Gets all points of the line. Both "From" and "To" are inclusive.
        /// The direction is from "From" to "To";
        /// </summary>
        /// <returns></returns>
        public List<Vector3Int> GetPoints()
        {
            var points = new List<Vector3Int>();

            if (From == To)
            {
                points.Add(From);
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    points.Add(From + Direction * i);
                }
            }

            return points;
        }

        /// <summary>
        /// Gets nth point on the line. (Counted from From)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public Vector3Int GetNthPoint(int n)
        {
            if (n >= Length)
            {
                throw new ArgumentException("n is greater than the length of the line.", nameof(n));
            }

            if (n < 0)
            {
                throw new ArgumentException("n must be greater than or equal to 0", nameof(n));
            }


            return From + Direction * n;
        }

        /// <summary>
        /// Checks if the orthogonal line contains a given point.
        /// </summary>
        /// <remarks>
        /// Index is 0 for From and Length - 1 for To.
        /// </remarks>
        /// <param name="point"></param>
        /// <returns>Index of a given point on the line or -1.</returns>
        [Pure]
        public int Contains(Vector3Int point)
        {
            if (Direction == Vector3Int.zero)
            {
                return point == From ? 0 : -1;
            }

            if (point == From)
            {
                return 0;
            }

            int index;
            int sign;

            if (Direction.x != 0 && point.y == fromY && point.z == fromZ)
            {
                index = point.x - From.x;
                sign = Direction.x;
            }
            else if (Direction.y != 0 && point.x == fromX && point.z == fromZ)
            {
                index = point.y - From.y;
                sign = Direction.y;
            }
            else if (Direction.z != 0 && point.y == fromY && point.x == fromX)
            {
                index = point.z - From.z;
                sign = Direction.z;
            }
            else
            {
                return -1;
            }

            var absoluteIndex = Math.Abs(index);
            if (sign == Math.Sign(index) && absoluteIndex < Length)
            {
                return absoluteIndex;
            }

            return -1;
        }

        #region Operators

        /// <summary>
        ///     Adds given IntVector2 to both endpoints of a given orthogonal line.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static OrthogonalLine operator +(OrthogonalLine line, Vector3Int point)
        {
            return new OrthogonalLine(line.From + point, line.To + point);
        }

        /// <summary>
        ///     Adds given IntVector2 to both endpoints of a given orthogonal line.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static OrthogonalLine operator +(Vector3Int point, OrthogonalLine line)
        {
            return line + point;
        }

        #endregion

        /// <inheritdoc />
        public bool Equals(OrthogonalLine other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return obj is OrthogonalLine line && Equals(line);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode() * 397) ^ To.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"IntLine: {From} -> {To}";
        }
    }
}