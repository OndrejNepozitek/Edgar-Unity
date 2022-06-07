using System;
using UnityEngine;

namespace Edgar.Unity
{
    [Serializable]
    public class SerializableVector3Int
    {
        public int x;
        public int y;
        public int z;

        public SerializableVector3Int(int rX, int rY, int rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{x}, {y}, {z}]";
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector3Int(SerializableVector3Int rValue)
        {
            return new Vector3Int(rValue.x, rValue.y, rValue.z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator SerializableVector3Int(Vector3Int rValue)
        {
            return new SerializableVector3Int(rValue.x, rValue.y, rValue.z);
        }
    }
}