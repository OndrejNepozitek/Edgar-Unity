using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public class NotEnoughDoors
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var roomTemplates = new List<RoomTemplateGrid2D>();
            var levelDescriptionGrid2D = levelDescription.GetLevelDescription();
            foreach (var room in levelDescription.GetGraph().Vertices)
            {
                var roomDescription = levelDescriptionGrid2D.GetRoomDescription(room);
                if (!roomDescription.IsCorridor)
                {
                    roomTemplates.AddRange(roomDescription.RoomTemplates);
                }
            }
            roomTemplates = roomTemplates.Distinct().ToList();

            var result = new Result
            {
                Summary = ""
            };

            var summaries = new List<string>()
            {
                DoorsOnAllSides(roomTemplates, result),
            };
            
            var summariesWithoutNulls = summaries.Where(x => x != null).ToList();
            if (summariesWithoutNulls.Count == 0)
            {
                return result;
            }

            var sb = new StringBuilder();
            sb.AppendLine("For the performance of the generator, it is important to add many door positions to individual room templates.");
            sb.AppendLine("It is even more critical if you want to generate levels with cycles.");
            sb.AppendLine("A good starting point is to use the Simple door mode that provides many door positions.");
            sb.AppendLine("");
            sb.AppendLine("We detected potential issues related to doors below:");
            sb.AppendLine("");

            result.IsPotentialProblem = true;

            result.Summary = sb.ToString();
            result.Summary += string.Join("\n", summariesWithoutNulls);
            
            return result;
        }

        private string DoorsOnAllSides(List<RoomTemplateGrid2D> roomTemplates, Result result)
        {
            var hasAllDirectionsDoors = false;
            foreach (var roomTemplate in roomTemplates)
            {
                var doors = roomTemplate.Doors.GetDoors(roomTemplate.Outline);
                var distinctDirections = doors
                    .Select(x => x.Line.GetDirection())
                    .Distinct()
                    .Count();
                
                if (distinctDirections >= 4)
                {
                    hasAllDirectionsDoors = true;
                    break;
                }
            }

            if (hasAllDirectionsDoors)
            {
                return null;
            }

            result.MissingDoorsOnAllSides = true;
            
            var sb = new StringBuilder();
            sb.AppendLine("There are no room templates that have doors on all 4 sides.");
            sb.AppendLine("Such room templates are useful for the generator because they are often very versatile and can be used for rooms with many neighbors.");
            sb.AppendLine("They are also very useful if you want to have cycles in your level graph.");

            return sb.ToString();
        }

        public class Result : IDiagnosticResult
        {
            public string Name => "Not enough doors";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
            
            public bool MissingDoorsOnAllSides { get; set; }
        }
    }
}