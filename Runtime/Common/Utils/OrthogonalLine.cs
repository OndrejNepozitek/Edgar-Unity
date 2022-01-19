using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    ///     Structure representing an orthogonal line in a integer grid.
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
        ///     Returns number of points.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Construct an orthogonal line from given endpoints.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <exception cref="ArgumentException">Thrown when given points do not form an orthogonal line.</exception>
        public OrthogonalLine(Vector3Int from, Vector3Int to)
        {
            if (from.x != to.x && from.y != to.y)
            {
                throw new ArgumentException("The line is not orthogonal");
            }

            if (from.z != to.z)
            {
                throw new ArgumentException("z values must be equal");
            }

            fromX = from.x;
            fromY = from.y;
            fromZ = from.z;

            toX = to.x;
            toY = to.y;
            toZ = to.z;

            Length = 0;
            Length = GetLength(From, To);
        }

        private static int GetLength(Vector3Int from, Vector3Int to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y) + 1;
        }

        /// <summary>
        ///     Returns a direction of the line.
        /// </summary>
        /// <returns></returns>
        private Direction GetDirection()
        {
            if (From == To)
            {
                return Direction.Undefined;
            }

            return GetDirection(From, To);
        }

        /// <summary>
        ///     Gets a direction of an orthogonal lined formed by given points.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <exception cref="ArgumentException">Thrown when given points do not form an orthogonal line</exception>
        /// <returns></returns>
        private static Direction GetDirection(Vector3Int from, Vector3Int to)
        {
            if (from == to)
            {
                return Direction.Undefined;
            }

            if (from.x == to.x)
            {
                return from.y > to.y ? Direction.Bottom : Direction.Top;
            }

            if (from.y == to.y)
            {
                return from.x > to.x ? Direction.Left : Direction.Right;
            }

            throw new ArgumentException("Given points do not form an orthogonal line");
        }

        /// <summary>
        ///     Gets all points of the line. Both "From" and "To" are inclusive.
        ///     The direction is from "From" to "To";
        /// </summary>
        /// <returns></returns>
        public List<Vector3Int> GetPoints()
        {
            var points = new List<Vector3Int>();

            switch (GetDirection())
            {
                case var _ when From == To:
                    points.Add(From);
                    break;

                case Direction.Top:
                    for (var i = From.y; i <= To.y; i++)
                        points.Add(new Vector3Int(From.x, i, From.z));
                    break;
                case Direction.Bottom:
                    for (var i = From.y; i >= To.y; i--)
                        points.Add(new Vector3Int(From.x, i, From.z));
                    break;
                case Direction.Right:
                    for (var i = From.x; i <= To.x; i++)
                        points.Add(new Vector3Int(i, From.y, From.z));
                    break;
                case Direction.Left:
                    for (var i = From.x; i >= To.x; i--)
                        points.Add(new Vector3Int(i, From.x, From.z));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return points;
        }

        /// <summary>
        ///     Gets nth point on the line. (Counted from From)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [Pure]
        public Vector3Int GetNthPoint(int n)
        {
            if (n > Length)
                throw new ArgumentException("n is greater than the length of the line.", nameof(n));

            var direction = GetDirection();

            switch (direction)
            {
                case Direction.Top:
                    return From + new Vector3Int(0, n, 0);
                case Direction.Right:
                    return From + new Vector3Int(n, 0, 0);
                case Direction.Bottom:
                    return From - new Vector3Int(0, n, 0);
                case Direction.Left:
                    return From - new Vector3Int(n, 0, 0);
                case Direction.Undefined:
                {
                    if (n > 0)
                        throw new ArgumentException();

                    return From;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Checks if the orthogonal line contains a given point.
        /// </summary>
        /// <remarks>
        ///     Index is 0 for From and Count + 1 for To.
        /// </remarks>
        /// <param name="point"></param>
        /// <returns>Index of a given point on the line or -1.</returns>
        [Pure]
        public int Contains(Vector3Int point)
        {
            var direction = GetDirection();

            switch (direction)
            {
                case Direction.Top:
                {
                    if (point.x == From.x && point.y <= To.y && point.y >= From.y)
                    {
                        return point.y - From.y;
                    }

                    break;
                }
                case Direction.Right:
                {
                    if (point.y == From.y && point.x <= To.x && point.x >= From.x)
                    {
                        return point.x - From.x;
                    }

                    break;
                }
                case Direction.Bottom:
                {
                    if (point.x == From.x && point.y >= To.y && point.y <= From.y)
                    {
                        return From.y - point.y;
                    }

                    break;
                }
                case Direction.Left:
                {
                    if (point.y == From.y && point.x >= To.x && point.x <= From.x)
                    {
                        return From.x - point.x;
                    }

                    break;
                }
                case Direction.Undefined:
                {
                    if (point == From)
                    {
                        return 0;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return -1;
        }

        /// <summary>
        ///     Gets a direction vector of the line.
        /// </summary>
        /// <remarks>
        ///     That is a vector that satisfies that From + Length * direction_vector = To.
        /// </remarks>
        /// <returns></returns>
        [Pure]
        public Vector3Int GetDirectionVector()
        {
            switch (GetDirection())
            {
                case Direction.Top:
                    return new Vector3Int(0, 1, 0);
                case Direction.Right:
                    return new Vector3Int(1, 0, 0);
                case Direction.Bottom:
                    return new Vector3Int(0, -1, 0);
                case Direction.Left:
                    return new Vector3Int(-1, 0, 0);
                case Direction.Undefined:
                    throw new InvalidOperationException("Degenerated lines without a direction set do not have a direction vector.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            return $"IntLine: {From} -> {To} ({GetDirection()})";
        }

        /// <summary>
        ///     Enum that holds a direction of an orthogonal line.
        /// </summary>
        public enum Direction
        {
            Top,
            Right,
            Bottom,
            Left,
            Undefined
        }
    }
}