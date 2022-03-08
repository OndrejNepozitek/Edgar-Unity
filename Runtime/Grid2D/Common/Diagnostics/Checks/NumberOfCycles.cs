using System.Linq;
using System.Text;

namespace Edgar.Unity.Diagnostics
{
    public class NumberOfCycles
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var graph = levelDescription.GetGraph();
            var nonTreeEdges = graph.Edges.Count() - graph.VerticesCount + 1;
            var result = new Result();
            result.NumberOfCycles = nonTreeEdges;

            if (nonTreeEdges >= 2)
            {
                result.IsPotentialProblem = true;
                var sb = new StringBuilder();

                sb.AppendLine($"It seems like the level graph has at least {nonTreeEdges} cycles.");
                sb.AppendLine($"The larger the number of cycles, the harder it is for the generator to produce a level.");
                sb.AppendLine($"Graphs without cycles are the easiest for the algorithm to generate. It is usually recommended to have at most 2 cycles.");
                sb.AppendLine($"If you want to see whether the number of cycles causes is too high for the generator, try removing some of the cycles and see if/how much the performance changes.");
                sb.AppendLine($"Or if you really want to have cycles in your levels, make sure that your room templates have as many available door positions as possible.");

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
            public string Name => "Number of cycles";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }

            public int NumberOfCycles { get; set; }
        }
    }
}