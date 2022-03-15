using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public class DifferentLengthsOfDoors
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var roomTemplates = levelDescription.GetPrefabToRoomTemplateMapping();
            var doorLengths = new Dictionary<int, List<GameObject>>();

            foreach (var pair in roomTemplates)
            {
                var gameObject = pair.Key;
                var roomTemplate = pair.Value;

                foreach (var doorLength in GetDoorLengths(roomTemplate))
                {
                    if (!doorLengths.ContainsKey(doorLength))
                    {
                        doorLengths[doorLength] = new List<GameObject>();
                    }

                    doorLengths[doorLength].Add(gameObject);
                }
            }

            var result = new Result();
            result.DoorLengths = doorLengths;

            if (doorLengths.Count == 1)
            {
                var doorLength = doorLengths.Keys.First();
                result.IsPotentialProblem = false;
                result.Summary = $"All doors have the same length ({doorLength}). This is the recommended setup.";
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("There are room templates with different lengths of doors:");

                foreach (var pair in doorLengths.OrderBy(x => x.Key))
                {
                    var doorLength = pair.Key;
                    var affectedRoomTemplates = pair.Value;
                    var roomTemplatesExample = string.Join(", ", affectedRoomTemplates.Take(3).Select(x => $"\"{x.name}\""));
                    sb.AppendLine($"- Door length {doorLength} (room template{(affectedRoomTemplates.Count > 1 ? "s" : "")} {roomTemplatesExample}{(affectedRoomTemplates.Count > 3 ? "..." : "")})");
                }

                if (doorLengths.Count == 2)
                {
                    sb.Append($"This can be completely fine if you know what you are doing.");
                }
                else
                {
                    sb.Append($"Having more than 2 different lengths of doors looks very suspicious.");
                }

                sb.AppendLine($"If it was not intentional to have doors of different lengths, go through individual room templates and check their door lengths.");
                sb.Append($"While doing so, give more attention to room templates with the manual door mode.");

                result.IsPotentialProblem = true;
                result.Summary = sb.ToString();
            }

            return result;
        }

        private HashSet<int> GetDoorLengths(RoomTemplateGrid2D roomTemplate)
        {
            var doorMode = roomTemplate.Doors;
            var doors = doorMode.GetDoors(roomTemplate.Outline);
            var doorLengths = new HashSet<int>();

            foreach (var door in doors)
            {
                doorLengths.Add(door.Length + 1);
            }

            return doorLengths;
        }

        public class Result : IDiagnosticResult
        {
            public string Name => "Different lengths of doors";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }

            public Dictionary<int, List<GameObject>> DoorLengths { get; set; }
        }
    }
}