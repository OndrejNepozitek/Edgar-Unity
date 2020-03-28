using System.Collections.Generic;
using GeneralAlgorithms.DataStructures.Common;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ProceduralLevelGenerator.Unity.Utils
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this IntVector2 vector)
        {
            return new Vector3(vector.X, vector.Y);
        }

        public static IntVector2 ToCustomIntVector2(this Vector2Int vector)
        {
            return new IntVector2(vector.x, vector.y);
        }

        public static IntVector2 ToCustomIntVector2(this Vector3Int vector)
        {
            return new IntVector2(vector.x, vector.y);
        }

        public static Vector3Int ToUnityIntVector3(this IntVector2 vector)
        {
            return new Vector3Int(vector.X, vector.Y, 0);
        }

        public static Vector3Int RoundToUnityIntVector3(this Vector3 vector)
        {
            return new Vector3Int((int) vector.x, (int) vector.y, 0);
        }

        // TODO: not nice
        public static Vector3Int RotateAroundCenter(this Vector3Int vector, int degrees)
        {
            return vector.ToCustomIntVector2().RotateAroundCenter(degrees).ToUnityIntVector3();
        }

        // TODO: not nice
        public static Vector3Int Transform(this Vector3Int vector, Transformation transformation)
        {
            return vector.ToCustomIntVector2().Transform(transformation).ToUnityIntVector3();
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Gets visible children of `SerializedProperty` at 1 level depth.
        /// </summary>
        /// <param name="serializedProperty">Parent `SerializedProperty`.</param>
        /// <returns>Collection of `SerializedProperty` children.</returns>
        public static IEnumerable<SerializedProperty> GetVisibleChildren(this SerializedProperty serializedProperty, bool includeNested)
        {
            var currentProperty = serializedProperty.Copy();
            var nextSiblingProperty = serializedProperty.Copy();
            nextSiblingProperty.NextVisible(false);
 
            if (currentProperty.NextVisible(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                        break;
 
                    yield return currentProperty;
                }
                while (currentProperty.NextVisible(includeNested));
            }
        }
#endif
    }
}