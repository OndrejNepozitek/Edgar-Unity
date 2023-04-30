using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public class WrongPositionGameObjects
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var roomTemplates = levelDescription.GetPrefabToRoomTemplateMapping();
            var wrongPositionGameObjects = new Dictionary<GameObject, List<GameObject>>();

            foreach (var pair in roomTemplates)
            {
                var gameObject = pair.Key;
                var wrongPositions = RoomTemplateDiagnostics.GetWrongPositionGameObjects(gameObject);

                if (wrongPositions.Count != 0)
                {
                    wrongPositionGameObjects.Add(gameObject, wrongPositions);
                }
            }

            var result = new Result
            {
                WrongPositionGameObjects = wrongPositionGameObjects
            };

            if (wrongPositionGameObjects.Count == 0)
            {
                result.IsPotentialProblem = false;
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"When designing room templates, the following game objects should be positioned at (0,0,0): the prefab root, tilemaps root and individual tilemap objects.");
                sb.AppendLine($"The following room templates contain badly positioned game objects:");

                foreach (var pair in wrongPositionGameObjects.OrderBy(x => x.Key))
                {
                    sb.AppendLine($"- room template: {pair.Key.name}, game objects: {string.Join(", ", pair.Value.Select(x => x.name))}");
                }
                
                result.IsPotentialProblem = true;
                result.Summary = sb.ToString();
            }

            return result;
        }

        public class Result : IDiagnosticResult
        {
            public string Name => "Wrong positions of room template game objects";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }

            public Dictionary<GameObject, List<GameObject>> WrongPositionGameObjects { get; set; }
        }
    }
}