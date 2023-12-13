using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity.Export
{
    #if OndrejNepozitekEdgar
    // This connection class is used to debug exported levels.
    public class ExportConnection : ConnectionBase
    {
        public List<GameObject> RoomTemplates;

        public override List<GameObject> GetRoomTemplates()
        {
            return RoomTemplates;
        }
    }
    #endif
}