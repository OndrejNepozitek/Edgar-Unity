using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edgar.Legacy.Utils.GraphAnalysis;

namespace Edgar.Unity.Diagnostics
{
    public class OddCycles
    {
        public Result Run(LevelDescriptionBase levelDescription)
        {
            var graph = levelDescription.GetGraph();
            var graphCyclesAlgo = new GraphCyclesGetter<RoomBase>();
            var cycles = graphCyclesAlgo.GetCycles(graph);

            var result = new Result
            {
                CycleLengths = cycles.Select(x => x.Count).OrderBy(x => x).ToList()
            };
            var hasOddCycle = result.CycleLengths.Any(x => x % 2 == 1);

            if (hasOddCycle)
            {
                result.IsPotentialProblem = true;
                var sb = new StringBuilder();

                sb.AppendLine($"It seems like the level graph has a cycle with an odd number of rooms");
                sb.AppendLine($"We detected the following cycle lengths: {string.Join(", ", result.CycleLengths)}.");
                sb.AppendLine($"Odd-length cycles are harder to lay out than even-length cycles - you often need good room templates with many doors.");
                sb.AppendLine($"Extra hard are cycles consisting of 3 rooms.");
                sb.AppendLine($"Consider adding/removing a room to/from an odd-length cycle to make it even-length.");
                
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
            public string Name => "Cycles with odd number of rooms";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
            
            public List<int> CycleLengths { get; set; }
        }
    }
}