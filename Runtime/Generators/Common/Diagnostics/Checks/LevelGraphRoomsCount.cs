namespace Edgar.Unity
{
    public class LevelGraphRoomsCount
    {
        public Result Run(LevelDescription levelDescription)
        {
            var graph = levelDescription.GetGraph();
            var roomsCount = graph.VerticesCount;

            return null;
        }

        public class Result : IDiagnosticsResult
        {
            public int RoomsCount { get; set; }

            public string Summary { get; set; }
        }
    }
}