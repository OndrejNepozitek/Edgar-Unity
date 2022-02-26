using UnityEngine;

namespace Edgar.Unity
{
    public abstract class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private int version = 1;

        protected virtual int OnUpgradeSerializedData(int version)
        {
            return 1;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            version = OnUpgradeSerializedData(version);
        }
    }
}