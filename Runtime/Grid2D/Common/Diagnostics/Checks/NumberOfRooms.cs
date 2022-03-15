using System.Text;

namespace Edgar.Unity.Diagnostics
{
    public class NumberOfRooms
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var graph = levelDescription.GetGraph();
            var vertices = graph.VerticesCount;
            var result = new Result();
            result.NumberOfRooms = vertices;

            if (vertices > 20)
            {
                result.IsPotentialProblem = true;
                var sb = new StringBuilder();
                sb.AppendLine($"The level graph has quite a lot of rooms ({vertices}).");
                sb.AppendLine($"The higher the number of rooms, the harder it is for the generator to produce a level.");
                sb.AppendLine($"If you want to have a lot of rooms in the level, it is best to limit the number of cycles.");
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
            public string Name => "Number of rooms";

            public int NumberOfRooms { get; set; }

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
        }
    }
}