using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public class WrongManualDoors
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var result = new Result();
            var mapping = levelDescription.GetPrefabToRoomTemplateMapping();
            var problematicRoomTemplates = new Dictionary<GameObject, int>();

            foreach (var pair in mapping)
            {
                var roomTemplate = pair.Value;
                var gameObject = pair.Key;

                var individualResult = RoomTemplateDiagnostics.CheckWrongManualDoors(roomTemplate.Outline, roomTemplate.Doors, out var differentLengthsCount);
                if (individualResult.HasErrors)
                {
                    problematicRoomTemplates.Add(gameObject, differentLengthsCount);
                }
            }

            if (problematicRoomTemplates.Count != 0)
            {
                result.IsPotentialProblem = true;
                result.ProblematicRoomTemplates = problematicRoomTemplates;
                var sb = new StringBuilder();

                sb.AppendLine($"There are room templates with a high probability of having an incorrect setup of manual doors.");
                sb.AppendLine($"The problematic room templates are:");

                foreach (var pair in problematicRoomTemplates)
                {
                    sb.AppendLine($"- \"{pair.Key.name}\" with {pair.Value} different lengths of doors");
                }

                sb.AppendLine($"Please go through these room templates and check that their setup is correct.");

                result.Summary = sb.ToString();
            }
            else
            {
                result.IsPotentialProblem = false;
            }

            return result;
        }

        public class Result : IDiagnosticResult
        {
            public string Name => "Wrong manual doors";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }

            public Dictionary<GameObject, int> ProblematicRoomTemplates { get; set; }
        }
    }
}