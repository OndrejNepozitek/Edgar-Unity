using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ProceduralLevelGenerator.Scripts.Generators.Common.LevelGraph
{
    /// <summary>
    ///     Represents a group of rooms that share the same room templates.
    /// </summary>
    [Serializable]
    public class RoomsGroup : ISerializationCallbackReceiver
    {
        /// <summary>
        ///     GUID of the group.
        /// </summary>
        [HideInInspector]
        public Guid Guid = Guid.NewGuid();

        /// <summary>
        ///     Room templates assigned to the group.
        /// </summary>
        public List<GameObject> IndividualRoomTemplates = new List<GameObject>();

        /// <summary>
        ///     Name of the group.
        /// </summary>
        public string Name = "New group";

        [HideInInspector]
        public List<RoomTemplatesSet> RoomTemplateSets = new List<RoomTemplatesSet>();

        #region Ugly GUID serialization logic

        [HideInInspector]
        [SerializeField]
        private byte[] serializedGuid;

        public void OnBeforeSerialize()
        {
            if (Guid != Guid.Empty)
            {
                serializedGuid = Guid.ToByteArray();
            }
        }

        public void OnAfterDeserialize()
        {
            if (serializedGuid != null && serializedGuid.Length == 16)
            {
                Guid = new Guid(serializedGuid);
            }
        }

        #endregion
    }
}