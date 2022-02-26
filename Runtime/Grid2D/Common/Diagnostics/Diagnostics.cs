using System.Collections.Generic;
using System.Linq;
using Edgar.GraphBasedGenerator.Grid2D;
using UnityEngine;

namespace Edgar.Unity.Diagnostics
{
    public static class Diagnostics
    {
        public static List<IDiagnosticResult> Run<TPayload>(TPayload payload)
        {
            var results = new List<IDiagnosticResult>();

            if (payload is DungeonGeneratorPayloadGrid2D dungeonGeneratorPayload)
            {
                results.AddRange(Run(dungeonGeneratorPayload.LevelDescription));
                results.Add(new TimeoutLength().Run(dungeonGeneratorPayload.DungeonGenerator));
                results.Add(new MinimumRoomDistance().Run(dungeonGeneratorPayload.DungeonGenerator));
            }

            return results;
        }

        public static List<IDiagnosticResult> Run(LevelDescriptionGrid2D levelDescription)
        {
            var results = new List<IDiagnosticResult>();

            results.Add(new DifferentLengthsOfDoors().Run(levelDescription));
            results.Add(new WrongManualDoors().Run(levelDescription));
            results.Add(new NumberOfCycles().Run(levelDescription));
            results.Add(new NumberOfRooms().Run(levelDescription));

            return results;
        }

        public static void DisplayPerformanceResults(List<IDiagnosticResult> results, bool isPreemptive = false)
        {
            var originalLogType = Application.GetStackTraceLogType(LogType.Warning);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);

            if (isPreemptive)
            {
                Debug.LogWarning($"<size=17><b>--- Performance diagnostics ---</b></size>");
                Debug.LogWarning($"This is an automatic diagnostic procedure meant to analyze potential problems with the configuration of the generator.");
                Debug.LogWarning($"You can see this text because you enabled the \"{nameof(DungeonGeneratorBaseGrid2D.EnableDiagnostics)}\" checkbox.");
                Debug.LogWarning($"If the performance of the generator is good, you may ignore all the suggestions below.");
                Debug.LogWarning($"---");
            }
            else
            {
                Debug.LogWarning($"<size=17><b>--- Timeout diagnostics ---</b></size>");
                Debug.LogWarning($"The generator was not able to produce a level within a given time limit. The reason for that is usually that there is some problem with the configuration of the generator.");
            }

            var problematicResults = results.Where(x => x.IsPotentialProblem).ToList();

            if (problematicResults.Count > 0)
            {
                Debug.LogWarning($"Below you can find an automatic diagnostic of what might be wrong with the configuration of the generator.");
                Debug.LogWarning($"If you are not sure what that to do, please create an issue on github together with a screenshot of the diagnostic below.");

                foreach (var result in problematicResults)
                {
                    if (result.IsPotentialProblem)
                    {
                        PrintResult(result);
                    }
                }
            }
            else
            {
                Debug.LogWarning($"It seems like we were not able to automatically detect any problems with the configuration.");
                Debug.LogWarning($"Please create an issue on github to further investigate the performance of the generator.");
            }

            Debug.LogWarning($"-------- <b>End of diagnostic</b> --------");

            Application.SetStackTraceLogType(LogType.Warning, originalLogType);
        }

        public static void DisplayNoSuitableShapeResults(List<IDiagnosticResult> results, RoomBase room, List<RoomTemplateGrid2D> roomTemplates)
        {
            var wrongManualDoors = results.SingleOrDefault(x => x is WrongManualDoors.Result && x.IsPotentialProblem);

            var originalLogType = Application.GetStackTraceLogType(LogType.Warning);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);

            Debug.LogWarning($"<size=17><b>--- Error diagnostic ---</b></size>");
            Debug.LogWarning($"The generator was not able to produce a level due to an error with one or more room templates.");

            if (wrongManualDoors != null)
            {
                Debug.LogWarning("--");
                Debug.LogWarning($"<b>There is a high chance that this error caused by an incorrect configuration of the manual door mode. See the \"{wrongManualDoors.Name}\" diagnostic section below.</b>");
                Debug.LogWarning($"<b>If you do not think that your manual doors are incorrect, continue reading.</b>");
                Debug.LogWarning("--");
            }

            Debug.LogWarning($"When trying to find a suitable shape for the \"{room.GetDisplayName()}\" room, there was no available room template that could be connected to already laid-out neighbors.");
            Debug.LogWarning($"When that happened, neighbors were assigned the following room templates: {string.Join(",", roomTemplates.Select(x => $"\"{x.Name}\""))}.");
            Debug.LogWarning($"To fix this issues, make sure that for every possible choice of neighboring room templates,");
            Debug.LogWarning($"there exists a room template for the room \"{room.GetDisplayName()}\" that can be connected to at least one of the neighbors.");

            if (wrongManualDoors != null)
            {
                PrintResult(wrongManualDoors);
            }

            Debug.LogWarning($"-------- <b>End of diagnostic</b> --------");

            Application.SetStackTraceLogType(LogType.Warning, originalLogType);
        }

        private static void PrintResult(IDiagnosticResult result)
        {
            Debug.LogWarning($"-------- <b>{result.Name}</b> --------");

            foreach (var line in result.Summary.Trim().Split('\n'))
            {
                Debug.LogWarning(line);
            }
        }
    }
}