using System.Collections.Generic;

namespace Edgar.Unity.Diagnostics
{
    public static class Diagnostics
    {
        public static List<IDiagnosticResult> Run<TPayload>(TPayload payload)
        {
            var results = new List<IDiagnosticResult>();

            if (payload is IGraphBasedGeneratorPayload graphBasedPayload)
            {
                results.AddRange(Run(graphBasedPayload.LevelDescription));
            }

            if (payload is DungeonGeneratorPayload dungeonGeneratorPayload)
            {
                results.Add(new TimeoutLength().Run(dungeonGeneratorPayload.DungeonGenerator));
            }

            return results;
        }

        public static List<IDiagnosticResult> Run(LevelDescription levelDescription)
        {
            var results = new List<IDiagnosticResult>();

            results.Add(new DifferentLengthsOfDoors().Run(levelDescription));
            results.Add(new NumberOfCycles().Run(levelDescription));
            results.Add(new NumberOfRooms().Run(levelDescription));

            return results;
        }
    }
}
