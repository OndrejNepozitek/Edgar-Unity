using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Tilemaps
{
    [Serializable]
    internal enum GridInformationType
    {
        Integer,
        String,
        Float,
        Double,
        UnityObject,
        Color
    }

    [Serializable]
    [AddComponentMenu("Tilemap/Grid Information")]
    public class GridInformation : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionColorKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<Color> m_PositionColorValues = new List<Color>();

        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionDoubleKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<double> m_PositionDoubleValues = new List<double>();

        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionFloatKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<float> m_PositionFloatValues = new List<float>();

        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionIntKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<int> m_PositionIntValues = new List<int>();

        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionObjectKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<Object> m_PositionObjectValues = new List<Object>();

        [SerializeField]
        [HideInInspector]
        private List<GridInformationKey> m_PositionStringKeys = new List<GridInformationKey>();

        [SerializeField]
        [HideInInspector]
        private List<string> m_PositionStringValues = new List<string>();

        internal Dictionary<GridInformationKey, GridInformationValue> PositionProperties { get; } = new Dictionary<GridInformationKey, GridInformationValue>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            var grid = GetComponentInParent<Grid>();
            if (grid == null)
                return;

            m_PositionIntKeys.Clear();
            m_PositionIntValues.Clear();
            m_PositionStringKeys.Clear();
            m_PositionStringValues.Clear();
            m_PositionFloatKeys.Clear();
            m_PositionFloatValues.Clear();
            m_PositionDoubleKeys.Clear();
            m_PositionDoubleValues.Clear();
            m_PositionObjectKeys.Clear();
            m_PositionObjectValues.Clear();
            m_PositionColorKeys.Clear();
            m_PositionColorValues.Clear();

            foreach (var kvp in PositionProperties)
            {
                switch (kvp.Value.type)
                {
                    case GridInformationType.Integer:
                        m_PositionIntKeys.Add(kvp.Key);
                        m_PositionIntValues.Add((int) kvp.Value.data);
                        break;
                    case GridInformationType.String:
                        m_PositionStringKeys.Add(kvp.Key);
                        m_PositionStringValues.Add(kvp.Value.data as string);
                        break;
                    case GridInformationType.Float:
                        m_PositionFloatKeys.Add(kvp.Key);
                        m_PositionFloatValues.Add((float) kvp.Value.data);
                        break;
                    case GridInformationType.Double:
                        m_PositionDoubleKeys.Add(kvp.Key);
                        m_PositionDoubleValues.Add((double) kvp.Value.data);
                        break;
                    case GridInformationType.Color:
                        m_PositionColorKeys.Add(kvp.Key);
                        m_PositionColorValues.Add((Color) kvp.Value.data);
                        break;
                    default:
                        m_PositionObjectKeys.Add(kvp.Key);
                        m_PositionObjectValues.Add(kvp.Value.data as Object);
                        break;
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            PositionProperties.Clear();
            for (var i = 0; i != Math.Min(m_PositionIntKeys.Count, m_PositionIntValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Integer;
                positionValue.data = m_PositionIntValues[i];
                PositionProperties.Add(m_PositionIntKeys[i], positionValue);
            }

            for (var i = 0; i != Math.Min(m_PositionStringKeys.Count, m_PositionStringValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.String;
                positionValue.data = m_PositionStringValues[i];
                PositionProperties.Add(m_PositionStringKeys[i], positionValue);
            }

            for (var i = 0; i != Math.Min(m_PositionFloatKeys.Count, m_PositionFloatValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Float;
                positionValue.data = m_PositionFloatValues[i];
                PositionProperties.Add(m_PositionFloatKeys[i], positionValue);
            }

            for (var i = 0; i != Math.Min(m_PositionDoubleKeys.Count, m_PositionDoubleValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Double;
                positionValue.data = m_PositionDoubleValues[i];
                PositionProperties.Add(m_PositionDoubleKeys[i], positionValue);
            }

            for (var i = 0; i != Math.Min(m_PositionObjectKeys.Count, m_PositionObjectValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.UnityObject;
                positionValue.data = m_PositionObjectValues[i];
                PositionProperties.Add(m_PositionObjectKeys[i], positionValue);
            }

            for (var i = 0; i != Math.Min(m_PositionColorKeys.Count, m_PositionColorValues.Count); i++)
            {
                GridInformationValue positionValue;
                positionValue.type = GridInformationType.Color;
                positionValue.data = m_PositionColorValues[i];
                PositionProperties.Add(m_PositionColorKeys[i], positionValue);
            }
        }

        public bool SetPositionProperty<T>(Vector3Int position, string name, T positionProperty)
        {
            throw new NotImplementedException("Storing this type is not accepted in GridInformation");
        }

        public bool SetPositionProperty(Vector3Int position, string name, int positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Integer, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, string name, string positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.String, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, string name, float positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Float, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, string name, double positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Double, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, string name, Object positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.UnityObject, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int position, string name, Color positionProperty)
        {
            return SetPositionProperty(position, name, GridInformationType.Color, positionProperty);
        }

        private bool SetPositionProperty(Vector3Int position, string name, GridInformationType dataType, object positionProperty)
        {
            var grid = GetComponentInParent<Grid>();
            if (grid != null && positionProperty != null)
            {
                GridInformationKey positionKey;
                positionKey.position = position;
                positionKey.name = name;

                GridInformationValue positionValue;
                positionValue.type = dataType;
                positionValue.data = positionProperty;

                PositionProperties[positionKey] = positionValue;
                return true;
            }

            return false;
        }

        public T GetPositionProperty<T>(Vector3Int position, string name, T defaultValue) where T : Object
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.UnityObject)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return positionValue.data as T;
            }

            return defaultValue;
        }

        public int GetPositionProperty(Vector3Int position, string name, int defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Integer)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (int) positionValue.data;
            }

            return defaultValue;
        }

        public string GetPositionProperty(Vector3Int position, string name, string defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.String)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (string) positionValue.data;
            }

            return defaultValue;
        }

        public float GetPositionProperty(Vector3Int position, string name, float defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Float)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (float) positionValue.data;
            }

            return defaultValue;
        }

        public double GetPositionProperty(Vector3Int position, string name, double defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Double)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (double) positionValue.data;
            }

            return defaultValue;
        }

        public Color GetPositionProperty(Vector3Int position, string name, Color defaultValue)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;

            GridInformationValue positionValue;
            if (PositionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != GridInformationType.Color)
                    throw new InvalidCastException("Value stored in GridInformation is not of the right type");
                return (Color) positionValue.data;
            }

            return defaultValue;
        }

        public bool ErasePositionProperty(Vector3Int position, string name)
        {
            GridInformationKey positionKey;
            positionKey.position = position;
            positionKey.name = name;
            return PositionProperties.Remove(positionKey);
        }

        public virtual void Reset()
        {
            PositionProperties.Clear();
        }

        public Vector3Int[] GetAllPositions(string propertyName)
        {
            return PositionProperties.Keys.ToList().FindAll(x => x.name == propertyName).Select(x => x.position).ToArray();
        }

        internal struct GridInformationValue
        {
            public GridInformationType type;
            public object data;
        }

        [Serializable]
        internal struct GridInformationKey
        {
            public Vector3Int position;
            public string name;
        }
    }
}