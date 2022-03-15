using System.Collections.Generic;
using Edgar.Geometry;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edgar.Unity
{
    public static class Extensions
    {
        public static Vector3 ToVector3(this EdgarVector2Int vector)
        {
            return new Vector3(vector.X, vector.Y);
        }

        public static EdgarVector2Int ToCustomIntVector2(this Vector2Int vector)
        {
            return new EdgarVector2Int(vector.x, vector.y);
        }

        public static EdgarVector2Int ToCustomIntVector2(this Vector3Int vector)
        {
            return new EdgarVector2Int(vector.x, vector.y);
        }

        public static Vector3Int ToUnityIntVector3(this EdgarVector2Int vector)
        {
            return new Vector3Int(vector.X, vector.Y, 0);
        }

        public static Vector3Int RoundToUnityIntVector3(this Vector3 vector)
        {
            return new Vector3Int((int) vector.x, (int) vector.y, (int) vector.z);
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
                } while (currentProperty.NextVisible(includeNested));
            }
        }
        #endif
    }
}