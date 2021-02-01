using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity
{
    /// <summary>
    /// Set of room templates that can be stored inside a scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "RoomTemplatesSet", menuName = "Edgar/Room templates set")]
    public class RoomTemplatesSet : ScriptableObject
    {
        public List<GameObject> RoomTemplates = new List<GameObject>();
    }
}