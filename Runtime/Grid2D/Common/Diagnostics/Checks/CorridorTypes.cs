using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edgar.Geometry;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public class CorridorTypes
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var hasCorridors = levelDescription.GetGraph().VerticesCount != levelDescription.GetGraphWithCorridors().VerticesCount;
            if (!hasCorridors)
            {
                return new Result();
            }

            var corridorRoomTemplates = new List<RoomTemplateGrid2D>();

            var levelDescriptionGrid2D = levelDescription.GetLevelDescription();
            foreach (var room in levelDescription.GetGraphWithCorridors().Vertices)
            {
                var roomDescription = levelDescriptionGrid2D.GetRoomDescription(room);
                if (roomDescription.IsCorridor)
                {
                    corridorRoomTemplates.AddRange(roomDescription.RoomTemplates);
                }
            }

            corridorRoomTemplates = corridorRoomTemplates.Distinct().ToList();
            var corridorTypesMapping = corridorRoomTemplates.ToDictionary(x => x, GetCorridorType);
            var corridorTypes = corridorTypesMapping.Values.ToList();
            
            var result = new Result
            {
                CorridorTypes = corridorTypes
            };
            
            var sb = new StringBuilder();

            // If there is an undefined corridor
            if (corridorTypes.Contains(CorridorType.Undefined))
            {
                result.IsPotentialProblem = true;
                
                var undefinedRoomTemplateNames = corridorTypesMapping
                    .Where(x => x.Value == CorridorType.Undefined)
                    .Select(x => x.Key.Name);
                
                sb.AppendLine($"Usually, it is expected that corridors are passages that have exactly one door on each of its two ends.");
                sb.AppendLine($"It looks like there are corridor room templates that do not satisfy this condition.");
                sb.AppendLine($"Affected room templates: {string.Join(", ", undefinedRoomTemplateNames)}.");
                sb.AppendLine($"If you know what you are doing, you can keep the current setup.");
            }
            // If there is missing Horizontal or Vertical corridor
            else if (corridorTypes.Distinct().Count() == 1)
            {
                result.IsPotentialProblem = true;
                
                var corridorType = corridorTypes.First();
                var missingCorridorType = corridorType == CorridorType.Horizontal ? CorridorType.Vertical : CorridorType.Horizontal;
                var corridorTypeName = corridorType.ToString().ToLower();
                var missingCorridorTypeName = missingCorridorType.ToString().ToLower();
                
                sb.AppendLine($"Both vertical and horizontal corridors are usually needed in a level.");
                sb.AppendLine($"However, it looks like your current setup only contains {corridorTypeName} corridor and no {missingCorridorTypeName} corridor.");
                sb.AppendLine($"You should add a {missingCorridorTypeName} corridor room template to the level.");
            }

            result.Summary = sb.ToString();
            
            return result;
        }

        private CorridorType GetCorridorType(RoomTemplateGrid2D roomTemplate)
        {
            var doorMode = roomTemplate.Doors;
            var doors = doorMode.GetDoors(roomTemplate.Outline);
            var doorOrientations = doors.Select(x => x.Line.GetDirection()).ToList();

            if (doors.Count != 2)
            {
                return CorridorType.Undefined;
            }

            if (doorOrientations.Contains(OrthogonalLineGrid2D.Direction.Bottom) && doorOrientations.Contains(OrthogonalLineGrid2D.Direction.Top))
            {
                return CorridorType.Horizontal;
            }
            
            if (doorOrientations.Contains(OrthogonalLineGrid2D.Direction.Left) && doorOrientations.Contains(OrthogonalLineGrid2D.Direction.Right))
            {
                return CorridorType.Vertical;
            }
            
            return CorridorType.Undefined;
        }

        public enum CorridorType
        {
            Undefined, Horizontal, Vertical
        }

        public class Result : IDiagnosticResult
        {
            public string Name => "Corridor types";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
            
            public List<CorridorType> CorridorTypes { get; set; }
        }
    }
}