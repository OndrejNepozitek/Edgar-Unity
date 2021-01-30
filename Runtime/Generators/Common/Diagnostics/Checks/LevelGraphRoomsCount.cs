namespace Edgar.Unity.Diagnostics
{
    public class LevelGraphRoomsCount
    {
        public Result Run(LevelDescription levelDescription)
        {
            var graph = levelDescription.GetGraph();
            var roomsCount = graph.VerticesCount;

            return null;
        }

        public class Result : IDiagnosticResult
        {
            public int RoomsCount { get; set; }

            public string Name { get; }

            public string Summary { get; set; }

            public bool IsPotentialProblem { get; }

            private Result()
            {

            }
        }
    }
}