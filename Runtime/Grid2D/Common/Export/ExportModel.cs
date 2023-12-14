using System;
using System.Collections.Generic;
using Edgar.GraphBasedGenerator.Common;
using UnityEngine;

namespace Edgar.Unity.Export
{
    [Serializable]
    public class ExportDto
    {
        public LevelGraphDto LevelGraph;
        public List<RoomTemplateDto> RoomTemplates;
        public RepeatModeOverride RepeatModeOverride;
        public int MinimumRoomDistance = 1;
    }
    
    [Serializable]
    public class LevelGraphDto
    {
        public List<RoomDto> Rooms;
        public List<ConnectionDto> Connections;
        public List<string> DefaultRoomTemplates;
        public List<string> DefaultCorridorRoomTemplates;
    }
    
    [Serializable]
    public class RoomDto
    {
        public string Id;
        public Vector2 Position;
        public string DisplayName;
        public List<string> RoomTemplates;
    }
    
    [Serializable]
    public class RoomTemplateDto
    {
        public string Name;
        public RoomTemplateRepeatMode RepeatMode = RoomTemplateRepeatMode.AllowRepeat;
        public List<Vector3Int> Tiles;
        public List<Vector3Int> OutlineTiles;
        public DoorsDto Doors;
    }
    
    [Serializable]
    public class DoorsDto
    {
        public DoorsGrid2D.DoorMode SelectedMode;
        public HybridDoorModeDataGrid2D HybridDoorModeData;
        public ManualDoorModeDataGrid2D ManualDoorModeData;
        public SimpleDoorModeDataGrid2D SimpleDoorModeData;
    }
    
    [Serializable]
    public class ConnectionDto
    {
        public string From;
        public string To;
        public List<string> RoomTemplates;
    }
}