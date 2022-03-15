using System.Text;

namespace Edgar.Unity.Diagnostics
{
    public class MinimumRoomDistance
    {
        public Result Run(DungeonGeneratorBaseGrid2D dungeonGenerator)
        {
            return Run(dungeonGenerator.GeneratorConfig.MinimumRoomDistance);
        }

        public Result Run(int minimumRoomDistance)
        {
            var result = new Result();
            result.MinimumRoomDistance = minimumRoomDistance;

            if (result.MinimumRoomDistance > 1)
            {
                result.IsPotentialProblem = true;

                var sb = new StringBuilder();
                sb.AppendLine($"The minimum room distance ({result.MinimumRoomDistance}) is higher than the default value (1).");
                sb.AppendLine($"Each increase of the value makes it harder for the generator to produce a valid layout.");
                sb.AppendLine($"Setting the value to 2 is usually okay, but with higher values you also have to make sure that your corridors are long enough to satisfy the minimum distance constraint.");
                sb.AppendLine($"You can ignore this warning if you know what you are doing.");

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
            public int MinimumRoomDistance { get; set; }

            public string Name => "Minimum room distance";

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; set; }
        }
    }
}