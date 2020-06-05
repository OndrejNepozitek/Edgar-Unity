using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Utils
{
    public class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        private int version;

        protected virtual int OnUpgradeSerializedData(int version) {
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