using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity.Diagnostics;
using UnityEngine;

namespace Edgar.Unity
{
    public class PipelineRunner<TPayload> where TPayload : class
    {
        /// <summary>
        ///     Runs given pipeline items with a given payload.
        /// </summary>
        /// <param name="pipelineTasks"></param>
        /// <param name="payload"></param>
        public void Run(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload)
        {
            var enumerator = GetEnumerator(pipelineTasks, payload);
            while (enumerator.MoveNext())
            {
                /* empty */
            }
        }

        public IEnumerator GetEnumerator(IEnumerable<IPipelineTask<TPayload>> pipelineTasks, TPayload payload)
        {
            foreach (var pipelineItem in pipelineTasks)
            {
                pipelineItem.Payload = payload;

                IEnumerator enumerator;

                try
                {
                    enumerator = pipelineItem.Process();
                }
                catch (TimeoutException e)
                {
                    var results = Diagnostics.Diagnostics.Run(payload);
                    e.DiagnosticResults = results;
                    DisplayResults(results);

                    throw;
                }

                while (true)
                {
                    try
                    {
                        var hasNext = enumerator.MoveNext();
                        if (!hasNext)
                        {
                            break;
                        }
                    }
                    catch (TimeoutException e)
                    {
                        var results = Diagnostics.Diagnostics.Run(payload);
                        e.DiagnosticResults = results;
                        DisplayResults(results);

                        throw;
                    }

                    yield return null;
                }
            }
        }

        private void DisplayResults(List<IDiagnosticResult> results)
        {
            var originalLogType = Application.GetStackTraceLogType(LogType.Warning);
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);

            Debug.LogWarning($"<size=17><b>--- Timeout diagnostic ---</b></size>");
            Debug.LogWarning($"The generator was not able to produce a level within a given time limit. The reason for that is usually that there is some problem with the configuration of the generator.");

            var problematicResults = results.Where(x => x.IsPotentialProblem).ToList();

            if (problematicResults.Count > 0)
            {
                Debug.LogWarning($"Below you can find an automatic diagnostic of what might be wrong with the configuration.");
                Debug.LogWarning($"If you are not sure what that to do, please create an issue on github together with a screenshot of the diagnostic below.");

                foreach (var result in problematicResults)
                {
                    Debug.LogWarning($"-------- <b>{result.Name}</b> --------");

                    if (result.IsPotentialProblem)
                    {
                        foreach (var line in result.Summary.Trim().Split('\n'))
                        {
                            Debug.LogWarning(line);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"It seems like we were not able to automatically detect any problems with the configuration.");
                Debug.LogWarning($"Please create an issue on github to further investigate this problem.");
            }

            Debug.LogWarning($"-------- <b>End of diagnostic</b> --------");

            Application.SetStackTraceLogType(LogType.Warning, originalLogType);
        }
    }
}