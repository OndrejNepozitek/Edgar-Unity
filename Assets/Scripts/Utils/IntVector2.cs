namespace Assets.Scripts.Utils
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Integer vector with 2 elements. Represents a point in a 2D discrete space.
	/// </summary>
	public struct IntVector2 : IComparable<IntVector2>, IEquatable<IntVector2>
	{
		public readonly int X;

		public readonly int Y;

		public IntVector2(int x, int y)
		{
			X = x;
			Y = y;
		}

		#region Distance computations

		/// <summary>
		/// Computes a manhattan distance of two vectors.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int ManhattanDistance(IntVector2 a, IntVector2 b)
		{
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
		}

		/// <summary>
		/// Compute an euclidean distance of two vectors.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double EuclideanDistance(IntVector2 a, IntVector2 b)
		{
			return Math.Sqrt((int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2)));
		}

		/// <summary>
		/// Computes a maximum distance between corresponding components of two vectors.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int MaxDistance(IntVector2 a, IntVector2 b)
		{
			return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
		}

		#endregion

		#region Equality, comparing and hash computation

		/// <inheritdoc />
		/// <summary>
		/// Check if two vectors are equal.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(IntVector2 other)
		{
			return X == other.X && Y == other.Y;
		}

		/// <summary>
		/// Check if two vectors are equal.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return obj is IntVector2 && Equals((IntVector2) obj);
		}

		/// <inheritdoc />
		/// <summary>
		/// Uses comparing operator to compare two vectors.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(IntVector2 other)
		{
			if (other == this)
			{
				return 0;
			}

			return this < other ? -1 : 1;
		}

		/// <summary>
		/// Computes hash code
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}

		#endregion

		#region Utility functions

		/// <summary>
		/// Computes a dot product of two vectors.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int DotProduct(IntVector2 other)
		{
			return X * other.X + Y * other.Y;
		}

		/// <summary>
		/// Computes element-wise product of two vectors.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public IntVector2 ElementWiseProduct(IntVector2 other)
		{
			return new IntVector2(X * other.X, Y * other.Y);
		}

		/// <summary>
		/// Gets all vectors that are adjacent to this one.
		/// That means vector that are different by 1 in exactly one of its components.
		/// </summary>
		/// <returns></returns>
		public List<IntVector2> GetAdjacentVectors()
		{
			var positions = new List<IntVector2>
			{
				new IntVector2(X + 1, Y),
				new IntVector2(X - 1, Y),
				new IntVector2(X, Y + 1),
				new IntVector2(X, Y - 1)
			};

			return positions;
		}

		/// <summary>
		/// Gets all vectors that are adjacent and diagonal to this one.
		/// That means vector that are different by 1 both of its components.
		/// </summary>
		/// <returns></returns>
		public List<IntVector2> GetAdjacentAndDiagonalVectors()
		{
			var positions = GetAdjacentVectors();

			positions.Add(new IntVector2(X + 1, Y + 1));
			positions.Add(new IntVector2(X - 1, Y - 1));
			positions.Add(new IntVector2(X - 1, Y + 1));
			positions.Add(new IntVector2(X + 1, Y - 1));

			return positions;
		}

		#endregion

		#region Operators

		public static IntVector2 operator +(IntVector2 a, IntVector2 b)
		{
			return new IntVector2(a.X + b.X, a.Y + b.Y);
		}

		public static IntVector2 operator -(IntVector2 a, IntVector2 b)
		{
			return new IntVector2(a.X - b.X, a.Y - b.Y);
		}

		public static IntVector2 operator *(int a, IntVector2 b)
		{
			return new IntVector2(a * b.X, a * b.Y);
		}

		public static bool operator ==(IntVector2 a, IntVector2 b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(IntVector2 a, IntVector2 b)
		{

			return !Equals(a, b);
		}

		public static bool operator <=(IntVector2 a, IntVector2 b)
		{

			return a.X <= b.X || (a.X == b.X && a.Y <= b.Y);
		}

		public static bool operator <(IntVector2 a, IntVector2 b)
		{
			return a.X < b.X || (a.X == b.X && a.Y < b.Y);
		}

		public static bool operator >(IntVector2 a, IntVector2 b)
		{
			return !(a <= b);
		}

		public static bool operator >=(IntVector2 a, IntVector2 b)
		{
			return !(a < b);
		}

		#endregion

		#region String representation

		public override string ToString()
		{
			return $"IntVector2 ({X}, {Y})";
		}

		public string ToStringShort()
		{
			return $"[{X}, {Y}]";
		}

		#endregion

	}
}