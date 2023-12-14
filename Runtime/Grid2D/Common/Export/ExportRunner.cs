using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Grid2D;
using Edgar.Legacy.Utils;
using UnityEngine;

namespace Edgar.Unity.Export
{
    public class ExportRunner
    {
        private readonly Dictionary<RoomBase, string> _roomToIdMapping = new Dictionary<RoomBase, string>();
        private readonly Dictionary<GameObject, string> _roomTemplateToIdMapping = new Dictionary<GameObject, string>();
        private LevelDescriptionGrid2D _levelDescription;

        public string ExportToJson(LevelDescriptionGrid2D levelDescription, int minimumRoomDistance, RepeatModeOverride repeatModeOverride)
        {
            _levelDescription = levelDescription;
            
            var exportDto = GetExportDto();
            exportDto.MinimumRoomDistance = minimumRoomDistance;
            exportDto.RepeatModeOverride = repeatModeOverride;
            
            var jsonString = JsonUtility.ToJson(exportDto, true);
            
            return jsonString;
        }

        private ExportDto GetExportDto()
        {
            var roomTemplates = _levelDescription.GetPrefabToRoomTemplateMapping();
            var roomTemplateGameObjects = roomTemplates.Keys.ToList();

            return new ExportDto()
            {
                RoomTemplates = roomTemplateGameObjects.Select(MapToDto).ToList(),
                LevelGraph = MapToLevelGraphDto(_levelDescription.GetLevelDescription()),
            };
        }

        // Get the set of room templates that is shared by the highest number of rooms
        private List<RoomTemplateGrid2D> GetDefaultRoomTemplates(LevelDescriptionGrid2D<RoomBase> levelDescription, bool isCorridor)
        {
            var rooms = levelDescription.GetGraph().Vertices.ToList();
            var roomTemplatesCountMapping = new Dictionary<List<RoomTemplateGrid2D>, int>();

            foreach (var room in rooms)
            {
                var roomDescription = levelDescription.GetRoomDescription(room);
                if (roomDescription.IsCorridor != isCorridor)
                {
                    continue;
                }

                var roomTemplates = roomDescription.RoomTemplates;

                var foundMatchingSet = false;
                foreach (var countMapping in roomTemplatesCountMapping)
                {
                    var referenceRoomTemplates = countMapping.Key;
                    if (!referenceRoomTemplates.SequenceEqual(roomTemplates)) continue;
                    
                    roomTemplatesCountMapping[referenceRoomTemplates] = countMapping.Value + 1;
                    foundMatchingSet = true;
                    break;
                }

                if (!foundMatchingSet)
                {
                    roomTemplatesCountMapping[roomTemplates] = 1;
                }
            }

            var sorted = roomTemplatesCountMapping.OrderByDescending(x => x.Value).ToList();

            return sorted[0].Key;
        }
        
        private LevelGraphDto MapToLevelGraphDto(LevelDescriptionGrid2D<RoomBase> levelDescription)
        {
            var defaultRoomTemplates = GetDefaultRoomTemplates(levelDescription, false);
            var defaultCorridorRoomTemplates = GetDefaultRoomTemplates(levelDescription, true);

            var prefabToRoomTemplateMapping = _levelDescription.GetPrefabToRoomTemplateMapping().ToTwoWayDictionary();
            
            var rooms = levelDescription
                .GetGraphWithoutCorridors()
                .Vertices
                .Select(x =>
                {
                    var roomDescription = levelDescription.GetRoomDescription(x);
                    var roomTemplates = roomDescription.RoomTemplates;
                    if (defaultRoomTemplates.SequenceEqual(roomTemplates))
                    {
                        roomTemplates = null;
                    }
                    
                    return new RoomDto()
                    {
                        Id = GetRoomId(x),
                        Position = x.Position,
                        DisplayName = x.GetDisplayName(),
                        RoomTemplates = roomTemplates?
                            .Select(roomTemplate => GetRoomTemplateId(prefabToRoomTemplateMapping.GetByValue(roomTemplate)))
                            .ToList()
                    };
                })
                .ToList();

            var connections = levelDescription
                .GetGraphWithoutCorridors()
                .Edges
                .Select(edge => new ConnectionDto()
                {
                    From = GetRoomId(edge.From),
                    To = GetRoomId(edge.To),
                })
                .ToList();
            
            return new LevelGraphDto()
            {
                Rooms = rooms,
                Connections = connections,
                DefaultRoomTemplates = defaultRoomTemplates
                    .Select(x => GetRoomTemplateId(prefabToRoomTemplateMapping.GetByValue(x)))
                    .ToList(),
                DefaultCorridorRoomTemplates = defaultCorridorRoomTemplates
                    .Select(x => GetRoomTemplateId(prefabToRoomTemplateMapping.GetByValue(x)))
                    .ToList(),
            };
        }
        
        private RoomTemplateDto MapToDto(GameObject roomTemplate)
        {
            var doorsComponent = roomTemplate.GetComponent<DoorsGrid2D>();
            var roomTemplateComponent = roomTemplate.GetComponent<RoomTemplateSettingsGrid2D>();
            
            var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate);
            var outlineTilemaps = RoomTemplateUtilsGrid2D.GetTilemapsForOutline(tilemaps);
            var tiles = RoomTemplateLoaderGrid2D.GetUsedTiles(tilemaps).ToList();
            var outlineHandler = roomTemplate.GetComponent<IRoomTemplateOutlineHandlerGrid2D>();
            var outlinePolygon = outlineHandler != null
                ? outlineHandler.GetRoomTemplateOutline()
                : new Polygon2D(RoomTemplateLoaderGrid2D.GetPolygonFromTilemaps(outlineTilemaps));
            
            var outlineTiles = outlinePolygon.GetOutlinePoints().Select(x => new Vector3Int(x.x, x.y,0)).ToList();
            
            return new RoomTemplateDto()
            {
                Name = GetRoomTemplateId(roomTemplate),
                RepeatMode = roomTemplateComponent.RepeatMode,
                Tiles = tiles,
                OutlineTiles = outlineTiles,
                Doors = new DoorsDto()
                {
                    SelectedMode = doorsComponent.SelectedMode,
                    HybridDoorModeData = doorsComponent.HybridDoorModeData,
                    ManualDoorModeData = doorsComponent.ManualDoorModeData,
                    SimpleDoorModeData = doorsComponent.SimpleDoorModeData,
                }
            };
        }
        
        private string GetRoomId(RoomBase room)
        {
            if (_roomToIdMapping.TryGetValue(room, out var id))
            {
                return id;
            }

            id = GetUniqueIdentifier(room.GetDisplayName(), _roomToIdMapping.Values);
            _roomToIdMapping[room] = id;

            return id;
        }
        
        private string GetRoomTemplateId(GameObject roomTemplate)
        {
            if (_roomTemplateToIdMapping.TryGetValue(roomTemplate, out var id))
            {
                return id;
            }

            id = GetUniqueIdentifier(roomTemplate.name, _roomToIdMapping.Values);
            _roomTemplateToIdMapping[roomTemplate] = id;

            return id;
        }
        
        private static string GetUniqueIdentifier(string originalName, IReadOnlyCollection<string> usedIds)
        {
            var identifier = originalName;
            var attemptCounter = 0;
            
            while (usedIds.Contains(identifier))
            {
                attemptCounter++;
                identifier = $"{originalName} {attemptCounter}";
            }

            return identifier;
        }
    }
}