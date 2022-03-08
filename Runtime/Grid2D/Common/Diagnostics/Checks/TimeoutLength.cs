using System.Text;

namespace Edgar.Unity.Diagnostics
{
    public class TimeoutLength
    {
        public Result Run(DungeonGeneratorBaseGrid2D dungeonGenerator)
        {
            return Run(dungeonGenerator.GeneratorConfig.Timeout);
        }

        public Result Run(int timeout)
        {
            var result = new Result();
            var limit = 3000;
            result.Timeout = timeout;

            if (timeout < limit)
            {
                result.IsPotentialProblem = true;

                var sb = new StringBuilder();
                sb.AppendLine($"The configured timeout time is relatively short ({timeout} ms).");
                sb.AppendLine($"It is recommended to use at least {limit} ms. Use even longer time for very complex levels.");
                sb.AppendLine($"The generator is non-deterministic, so even if 99% of runs end up being very fast, there might be some longer runs.");
                sb.AppendLine($"Therefore, it is recommended to not be too strict when configuring the timeout time.");

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
            public int Timeout { get; set; }

            public string Name => "Timeout length";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
        }
    }
}